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
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.Enums.GSLive;
using FiroozehGameService.Models.EventArgs;
using FiroozehGameService.Models.GSLive.Command;
using FiroozehGameService.Utils;

namespace FiroozehGameService.Core.Socket
{
    internal class GsTcpClient : GTcpClient
    {
        private const short CommandTimeOutWait = 700;
        private const short TurnTimeOutWait = 500;

        private TcpClient _client;
        private NetworkStream _clientStream;


        public GsTcpClient(Area area = null)
        {
            Area = area;

            if (area == null)
            {
                CommandEventHandlers.CommandClientConnected += OnTcpClientConnected;
            }
            else
            {
                KeepAliveUtil = new KeepAliveUtil(TurnBasedConst.KeepAliveTime);

                TurnBasedEventHandlers.TurnBasedClientConnected += OnTcpClientConnected;
                KeepAliveUtil.Caller += KeepAliveCaller;
            }
        }

        private async void KeepAliveCaller(object sender, byte[] payload)
        {
            await SendAsync(payload);
        }

        private void OnTcpClientConnected(object sender, TcpClient client)
        {
            if (Type != (GSLiveType) sender) return;

            _client = client;
            _clientStream = _client.GetStream();
            OperationCancellationToken = new CancellationTokenSource();

            DebugUtil.LogNormal<GsTcpClient>(
                Type == GSLiveType.TurnBased ? DebugLocation.TurnBased : DebugLocation.Command, "OnTcpClientConnected",
                "GsTcpClient -> Init Done");

            if (Area == null) CommandEventHandlers.GsCommandClientConnected?.Invoke(null, null);
            else TurnBasedEventHandlers.GsTurnBasedClientConnected?.Invoke(null, null);
        }


        internal override async Task Init(CommandInfo info, string cipher)
        {
            try
            {
                CommandInfo = info;
                Key = cipher;
                Type = CommandInfo == null ? GSLiveType.TurnBased : GSLiveType.Command;


                var ip = CommandInfo == null ? Area.Ip : CommandInfo.Ip;
                var port = CommandInfo?.Port ?? Area.Port;
                var timeOutWait = CommandInfo == null ? TurnTimeOutWait : CommandTimeOutWait;

                DebugUtil.LogNormal<GsTcpClient>(
                    Type == GSLiveType.TurnBased ? DebugLocation.TurnBased : DebugLocation.Command, "Init",
                    "GsTcpClient -> Init Started...");


                await new TcpClientWithTimeout(ip, port, timeOutWait).Connect(Type);
            }
            catch (Exception e)
            {
                e.LogException<GsTcpClient>(
                    Type == GSLiveType.TurnBased ? DebugLocation.TurnBased : DebugLocation.Command, "Init");
            }
        }


        internal override async Task StartReceiving()
        {
            DebugUtil.LogNormal<GsTcpClient>(
                Type == GSLiveType.TurnBased ? DebugLocation.TurnBased : DebugLocation.Command, "StartReceiving",
                "GsTcpClient -> StartReceiving");

            KeepAliveUtil?.Start();

            while (IsConnected())
                try
                {
                    BufferReceivedBytes += await _clientStream.ReadAsync(
                        Buffer,
                        BufferOffset,
                        Buffer.Length - BufferOffset,
                        OperationCancellationToken.Token);

                    DataBuilder.Append(Encoding.UTF8.GetString(Buffer, BufferOffset, BufferReceivedBytes));
                    var packets = PacketValidator.ValidateDataAndReturn(DataBuilder);
                    foreach (var packet in packets)
                        OnDataReceived(new SocketDataReceived
                        {
                            Packet = PacketDeserializer.Deserialize(packet, Key, IsEncryptionEnabled)
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
                            "StartReceiving");

                        OnClosed(new ErrorArg {Error = e.ToString()});
                    }

                    break;
                }
        }


        internal override void Send(Packet packet)
        {
            Task.Run(() =>
            {
                var buffer = PacketSerializer.Serialize(packet, Key, IsEncryptionEnabled);
                _clientStream?.Write(buffer, 0, buffer.Length);
            }, OperationCancellationToken.Token);
        }


        protected override async Task SendAsync(byte[] payload)
        {
            try
            {
                if (_clientStream != null)
                {
                    await _clientStream.WriteAsync(payload, 0, payload.Length);
                    await _clientStream.FlushAsync();
                }
            }
            catch (Exception e)
            {
                e.LogException<GsTcpClient>(
                    Type == GSLiveType.TurnBased ? DebugLocation.TurnBased : DebugLocation.Command, "SendAsync");
                OnClosed(new ErrorArg {Error = e.ToString()});
            }
        }


        internal override async Task SendAsync(Packet packet)
        {
            try
            {
                var buffer = PacketSerializer.Serialize(packet, Key, IsEncryptionEnabled);
                if (_clientStream != null)
                {
                    await _clientStream.WriteAsync(buffer, 0, buffer.Length);
                    await _clientStream.FlushAsync();
                }
            }
            catch (Exception e)
            {
                e.LogException<GsTcpClient>(
                    Type == GSLiveType.TurnBased ? DebugLocation.TurnBased : DebugLocation.Command, "SendAsync");
                OnClosed(new ErrorArg {Error = e.ToString()});
            }
        }

        internal override void StopReceiving()
        {
            try
            {
                KeepAliveUtil?.Dispose();
                DataBuilder?.Clear();

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
                KeepAliveUtil = null;
                Key = null;
                _client = null;
                _clientStream = null;
                OperationCancellationToken = null;
                DataReceived = null;
                IsEncryptionEnabled = false;

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

        internal override bool IsConnected()
        {
            var part1 = _client?.Client?.Poll(1000, SelectMode.SelectRead);
            var part2 = _client?.Client?.Available == 0;
            return part1 == false || part2 == false;
        }

        internal override void SetEncryptionStatus(bool isEnabled)
        {
            IsEncryptionEnabled = isEnabled;
        }
    }
}