// <copyright file="GsTcpClient.cs" company="Firoozeh Technology LTD">
// Copyright (C) 2019 Firoozeh Technology LTD. All Rights Reserved.
//
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//
//  http://www.apache.org/licenses/LICENSE-2.0
//
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//    limitations under the License.
// </copyright>


/**
* @author Alireza Ghodrati
*/

using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FiroozehGameService.Core.Socket.ClientHelper;
using FiroozehGameService.Handlers;
using FiroozehGameService.Models.BasicApi;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.Enums.GSLive;
using FiroozehGameService.Models.EventArgs;
using FiroozehGameService.Models.GSLive;
using FiroozehGameService.Models.GSLive.Command;
using FiroozehGameService.Utils;
using Timer = System.Timers.Timer;

namespace FiroozehGameService.Core.Socket
{
    internal class GsTcpClient : GTcpClient
    {
        private TcpClient _client;
        private NetworkStream _clientStream;


        public GsTcpClient(Area area = null)
        {
            Area = area;

            if (area == null) CommandEventHandlers.CommandClientConnected += OnTcpClientConnected;
            else TurnBasedEventHandlers.TurnBasedClientConnected += OnTcpClientConnected;
        }

        private void OnTcpClientConnected(object sender, GTcpConnection connection)
        {
            if (Type != (GSLiveType) sender || connection.IsWs) return;

            _client = connection.TcpClient;
            _clientStream = _client.GetStream();
            _clientStream.WriteTimeout = TcpTimeout;
            _clientStream.ReadTimeout = TcpTimeout;
            OperationCancellationToken = new CancellationTokenSource();
            IsAvailable = true;

            DebugUtil.LogNormal<GsTcpClient>(
                Type == GSLiveType.TurnBased ? DebugLocation.TurnBased : DebugLocation.Command, "OnTcpClientConnected",
                "GsTcpClient -> Init Done");

            if (Area == null) CommandEventHandlers.GsCommandClientConnected?.Invoke(null, null);
            else TurnBasedEventHandlers.GsTurnBasedClientConnected?.Invoke(null, null);
        }


        internal override void Init(CommandInfo info, string cipher)
        {
            try
            {
                CommandInfo = info;
                Key = cipher;
                Type = CommandInfo == null ? GSLiveType.TurnBased : GSLiveType.Command;

                Suspend();

                var ip = CommandInfo == null ? Area.Ip : CommandInfo.Ip;
                var port = CommandInfo?.Port ?? Area.Port;
                var timeOutWait = CommandInfo == null ? TurnTimeOutWait : CommandTimeOutWait;

                DebugUtil.LogNormal<GsTcpClient>(
                    Type == GSLiveType.TurnBased ? DebugLocation.TurnBased : DebugLocation.Command, "Init",
                    "GsTcpClient -> Init Started...");


                new TcpClientWithTimeout(ip, port, timeOutWait).Connect(Type);
            }
            catch (Exception e)
            {
                e.LogException<GsTcpClient>(
                    Type == GSLiveType.TurnBased ? DebugLocation.TurnBased : DebugLocation.Command, "Init");
            }
        }


        internal override void StartReceiving()
        {
            RecvThread = new Thread(async () => await Receiving())
            {
                Priority = ThreadPriority.Highest,
                IsBackground = true
            };

            RecvThread.Start();
        }

        internal override void StartSending()
        {
            SendQueueTimer = new Timer
            {
                Interval = SendQueueInterval
            };

            SendQueueTimer.Elapsed += async (sender, args) =>
            {
                bool dontHaveTempData;
                lock (SendTempQueueLock)
                {
                    dontHaveTempData = SendTempQueue.Count == 0;
                }

                if (IsSendingQueue || SendQueue.Count == 0 && dontHaveTempData || !IsConnected()) return;

                await Sending();
            };

            SendQueueTimer.Start();
        }


        private async Task Sending()
        {
            try
            {
                IsSendingQueue = true;

                lock (SendTempQueueLock)
                {
                    SendQueue.InsertRange(0, SendTempQueue);
                    SendTempQueue.Clear();
                }

                await SendAsync(PacketSerializer.Serialize(SendQueue, Key, Type == GSLiveType.Command));
            }
            catch (Exception e)
            {
                e.LogException<GsTcpClient>(
                    Type == GSLiveType.TurnBased ? DebugLocation.TurnBased : DebugLocation.Command,
                    "Sending");

                lock (SendTempQueueLock)
                {
                    SendTempQueue.InsertRange(0, SendQueue);
                }
            }
            finally
            {
                IsSendingQueue = false;
                SendQueue.Clear();
            }
        }

        private async Task Receiving()
        {
            DebugUtil.LogNormal<GsTcpClient>(
                Type == GSLiveType.TurnBased ? DebugLocation.TurnBased : DebugLocation.Command, "Receiving",
                "GsTcpClient -> Start Receiving...");

            while (IsConnected())
                try
                {
                    BufferReceivedBytes += await _clientStream.ReadAsync(Buffer, BufferOffset,
                        Buffer.Length - BufferOffset, OperationCancellationToken.Token);

                    if (!IsConnected()) break;

                    DataBuilder.Append(Encoding.UTF8.GetString(Buffer, BufferOffset, BufferReceivedBytes));
                    var packets = PacketValidator.ValidateDataAndReturn(DataBuilder);

                    foreach (var packet in packets)
                        OnDataReceived(new SocketDataReceived
                        {
                            Packet = PacketDeserializer.Deserialize(packet, Key, Type == GSLiveType.Command)
                        });

                    BufferReceivedBytes = 0;
                }
                catch (Exception e)
                {
                    if (!(e is OperationCanceledException || e is ObjectDisposedException ||
                          e is ArgumentOutOfRangeException))
                    {
                        e.LogException<GsTcpClient>(
                            Type == GSLiveType.TurnBased ? DebugLocation.TurnBased : DebugLocation.Command,
                            "Receiving");

                        OnClosed(new ErrorArg {Error = e.ToString()});
                    }

                    break;
                }

            DebugUtil.LogNormal<GsTcpClient>(
                Type == GSLiveType.TurnBased ? DebugLocation.TurnBased : DebugLocation.Command, "Receiving",
                "GsTcpClient -> Receiving Done!");
        }


        internal override void Send(Packet packet)
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    var buffer = PacketSerializer.Serialize(packet, Key, Type == GSLiveType.Command);

                    if (_clientStream == null) return;

                    _clientStream.Write(buffer, 0, buffer.Length);
                    _clientStream.Flush();
                }
                catch (Exception e)
                {
                    if (!(e is OperationCanceledException || e is ObjectDisposedException ||
                          e is ArgumentOutOfRangeException))
                    {
                        e.LogException<GsTcpClient>(
                            Type == GSLiveType.TurnBased ? DebugLocation.TurnBased : DebugLocation.Command,
                            "Send");

                        OnClosed(new ErrorArg {Error = e.ToString()});
                    }
                }
            }, OperationCancellationToken.Token);
        }


        protected override async Task SendAsync(byte[] payload)
        {
            if (_clientStream != null)
            {
                await _clientStream.WriteAsync(payload, 0, payload.Length, OperationCancellationToken.Token);
                await _clientStream.FlushAsync(OperationCancellationToken.Token);
            }
        }

        internal override void AddToSendQueue(Packet packet)
        {
            if (IsSendingQueue)
                lock (SendTempQueueLock)
                {
                    SendTempQueue?.Add(packet);
                }
            else SendQueue?.Add(packet);
        }


        internal override async Task SendAsync(Packet packet)
        {
            try
            {
                var buffer = PacketSerializer.Serialize(packet, Key, Type == GSLiveType.Command);
                if (_clientStream != null)
                {
                    await _clientStream.WriteAsync(buffer, 0, buffer.Length, OperationCancellationToken.Token);
                    await _clientStream.FlushAsync(OperationCancellationToken.Token);
                }
            }
            catch (Exception e)
            {
                if (!(e is OperationCanceledException || e is ObjectDisposedException ||
                      e is ArgumentOutOfRangeException))
                {
                    e.LogException<GsTcpClient>(
                        Type == GSLiveType.TurnBased ? DebugLocation.TurnBased : DebugLocation.Command, "SendAsync");

                    OnClosed(new ErrorArg {Error = e.ToString()});
                }
            }
        }

        internal override void StopReceiving(bool isGraceful)
        {
            try
            {
                IsAvailable = false;
                IsSendingQueue = false;

                DataBuilder?.Clear();
                SendQueue?.Clear();

                lock (SendTempQueueLock)
                {
                    SendTempQueue?.Clear();
                }

                SendQueueTimer?.Stop();
                SendQueueTimer?.Close();

                OperationCancellationToken?.Cancel(false);
                OperationCancellationToken?.Dispose();

                _client?.Close();
            }
            catch (Exception)
            {
                // ignored
            }
            finally
            {
                Key = null;
                _client = null;
                _clientStream = null;
                OperationCancellationToken = null;
                DataReceived = null;
                RecvThread = null;
                SendQueueTimer = null;

                try
                {
                    GC.SuppressFinalize(this);
                }
                catch (Exception)
                {
                    // ignored
                }

                DebugUtil.LogNormal<GsTcpClient>(
                    Type == GSLiveType.TurnBased ? DebugLocation.TurnBased : DebugLocation.Command, "StopReceiving",
                    "GsTcpClient -> StopReceiving Done");
            }
        }

        protected override void Suspend()
        {
            try
            {
                IsAvailable = false;
                IsSendingQueue = false;

                DataBuilder?.Clear();

                SendQueueTimer?.Stop();
                SendQueueTimer?.Close();

                OperationCancellationToken?.Cancel(false);
                OperationCancellationToken?.Dispose();

                _client?.Close();
            }
            catch (Exception)
            {
                // ignored
            }
            finally
            {
                _client = null;
                _clientStream = null;
                OperationCancellationToken = null;
                RecvThread = null;
                SendQueueTimer = null;

                try
                {
                    GC.SuppressFinalize(this);
                }
                catch (Exception)
                {
                    // ignored
                }

                DebugUtil.LogNormal<GsTcpClient>(
                    Type == GSLiveType.TurnBased ? DebugLocation.TurnBased : DebugLocation.Command, "Suspend",
                    "GsTcpClient -> Suspend Done");
            }
        }

        internal override bool IsConnected()
        {
            return IsAvailable;
        }
    }
}