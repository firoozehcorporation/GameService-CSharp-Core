// <copyright file="GsWebSocketClient.cs" company="Firoozeh Technology LTD">
// Copyright (C) 2021 Firoozeh Technology LTD. All Rights Reserved.
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
using System.Threading;
using System.Threading.Tasks;
using FiroozehGameService.Core.Socket.ClientHelper;
using FiroozehGameService.Handlers;
using FiroozehGameService.Models.BasicApi;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.Enums.GSLive;
using FiroozehGameService.Models.EventArgs;
using FiroozehGameService.Models.GSLive.Command;
using FiroozehGameService.Utils;
using WebSocketSharp;
using Timer = System.Timers.Timer;

namespace FiroozehGameService.Core.Socket
{
    internal class GsWebSocketClient : GTcpClient
    {
        private WebSocket _client;

        public GsWebSocketClient(Area area = null)
        {
            Area = area;
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

                OperationCancellationToken = new CancellationTokenSource();

                DebugUtil.LogNormal<GsWebSocketClient>(
                    Type == GSLiveType.TurnBased ? DebugLocation.TurnBased : DebugLocation.Command, "Init",
                    "GsWebSocketClient -> Init Started...");

                _client = new WebSocket("ws://" + ip + ':' + port);
                _client.OnOpen += OnOpen;
                _client.OnClose += OnClose;
                _client.OnError += OnError;
                _client.OnMessage += OnMessage;

                new WsClientWithTimeout(ip, port, timeOutWait).Connect(Type, _client);
            }
            catch (Exception e)
            {
                e.LogException<GsWebSocketClient>(
                    Type == GSLiveType.TurnBased ? DebugLocation.TurnBased : DebugLocation.Command, "Init");
            }
        }

        private void OnMessage(object sender, MessageEventArgs message)
        {
            if (!message.IsText) return;

            OnDataReceived(new SocketDataReceived
            {
                Packet = PacketDeserializer.Deserialize(message.Data, Key, Type == GSLiveType.Command)
            });
        }

        private void OnError(object sender, ErrorEventArgs error)
        {
            error.Exception.LogException<GsWebSocketClient>(
                Type == GSLiveType.TurnBased ? DebugLocation.TurnBased : DebugLocation.Command, "OnError");
        }

        private void OnClose(object sender, CloseEventArgs close)
        {
            if (!IsAvailable || close.WasClean) return;

            DebugUtil.LogError<GsWebSocketClient>(
                Type == GSLiveType.TurnBased ? DebugLocation.TurnBased : DebugLocation.Command, "OnTcpClientConnected",
                "GsWebSocketClient -> WS Closed , Reason : " + close.Reason + ", Code : " + close.Code);

            OnClosed(new ErrorArg {Error = close.Reason});
        }

        private void OnOpen(object sender, EventArgs e)
        {
            IsAvailable = true;

            DebugUtil.LogNormal<GsWebSocketClient>(
                Type == GSLiveType.TurnBased ? DebugLocation.TurnBased : DebugLocation.Command, "OnTcpClientConnected",
                "GsWebSocketClient -> WS Opened");

            if (Area == null) CommandEventHandlers.GsCommandClientConnected?.Invoke(null, null);
            else TurnBasedEventHandlers.GsTurnBasedClientConnected?.Invoke(null, null);
        }


        private async Task Sending()
        {
            try
            {
                if (SendQueue.Count == 0 || SendTempQueue.Count == 0 || !IsConnected()) return;

                IsSendingQueue = true;

                SendQueue.AddRange(SendTempQueue);
                SendTempQueue.Clear();

                await SendAsync(PacketSerializer.Serialize(SendQueue, Key, Type == GSLiveType.Command));

                SendQueue.Clear();
            }
            catch (Exception e)
            {
                e.LogException<GsWebSocketClient>(
                    Type == GSLiveType.TurnBased ? DebugLocation.TurnBased : DebugLocation.Command,
                    "Sending");
            }
            finally
            {
                IsSendingQueue = false;
            }
        }

        internal override void Send(Packet packet)
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    var buffer = PacketSerializer.Serialize(packet, Key, Type == GSLiveType.Command);

                    if (_client == null) return;

                    _client.Send(buffer);
                }
                catch (Exception e)
                {
                    if (!(e is OperationCanceledException || e is ObjectDisposedException ||
                          e is ArgumentOutOfRangeException))
                    {
                        e.LogException<GsWebSocketClient>(
                            Type == GSLiveType.TurnBased ? DebugLocation.TurnBased : DebugLocation.Command,
                            "Send");

                        OnClosed(new ErrorArg {Error = e.ToString()});
                    }
                }
            }, OperationCancellationToken.Token);
        }

        internal override Task SendAsync(Packet packet)
        {
            try
            {
                var buffer = PacketSerializer.Serialize(packet, Key, Type == GSLiveType.Command);
                _client?.SendAsync(buffer, null);
            }
            catch (Exception e)
            {
                if (e is OperationCanceledException || e is ObjectDisposedException ||
                    e is ArgumentOutOfRangeException) return Task.CompletedTask;

                e.LogException<GsWebSocketClient>(
                    Type == GSLiveType.TurnBased ? DebugLocation.TurnBased : DebugLocation.Command,
                    "SendAsync");

                OnClosed(new ErrorArg {Error = e.ToString()});
            }

            return Task.CompletedTask;
        }

        protected override Task SendAsync(byte[] payload)
        {
            try
            {
                _client?.SendAsync(payload, null);
            }
            catch (Exception e)
            {
                if (e is OperationCanceledException || e is ObjectDisposedException ||
                    e is ArgumentOutOfRangeException) return Task.CompletedTask;
                e.LogException<GsWebSocketClient>(
                    Type == GSLiveType.TurnBased ? DebugLocation.TurnBased : DebugLocation.Command, "SendAsync");

                OnClosed(new ErrorArg {Error = e.ToString()});
            }

            return Task.CompletedTask;
        }

        internal override void AddToSendQueue(Packet packet)
        {
            if (IsSendingQueue) SendTempQueue?.Add(packet);
            else SendQueue?.Add(packet);
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

                DebugUtil.LogNormal<GsWebSocketClient>(
                    Type == GSLiveType.TurnBased ? DebugLocation.TurnBased : DebugLocation.Command, "StopReceiving",
                    "GsWebSocketClient -> Suspend Done");
            }
        }

        internal override void StartReceiving()
        {
            SendQueueTimer = new Timer
            {
                Interval = SendQueueInterval
            };

            SendQueueTimer.Elapsed += async (sender, args) => { await Sending(); };
            SendQueueTimer.Start();
        }

        internal override void StopReceiving(bool isGraceful)
        {
            try
            {
                IsAvailable = false;
                IsSendingQueue = false;

                DataBuilder?.Clear();
                SendQueue?.Clear();
                SendTempQueue?.Clear();

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

                DebugUtil.LogNormal<GsWebSocketClient>(
                    Type == GSLiveType.TurnBased ? DebugLocation.TurnBased : DebugLocation.Command, "StopReceiving",
                    "GsWebSocketClient -> StopReceiving Done");
            }
        }

        internal override bool IsConnected()
        {
            return _client != null && IsAvailable;
        }
    }
}