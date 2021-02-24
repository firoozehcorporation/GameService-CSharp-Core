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
    internal class GsTcpClient : GsSocketClient
    {
        private const short CommandTimeOutWait = 700;
        private const short TurnTimeOutWait = 500;

        private TcpClient _client;
        private NetworkStream _clientStream;


        public GsTcpClient(Area area = null)
        {
            Area = area;
            KeepAliveUtil = new KeepAliveUtil(area == null ? CommandConst.KeepAliveTime : TurnBasedConst.KeepAliveTime);
            CoreEventHandlers.OnTcpClientConnected += OnTcpClientConnected;
            KeepAliveUtil.Caller += KeepAliveCaller;
        }

        private async void KeepAliveCaller(object sender, byte[] payload)
        {
            if (Type == GSLiveType.Command) return;

            await SendAsync(payload);
        }

        private void OnTcpClientConnected(object sender, TcpClient client)
        {
            if (Type != (GSLiveType) sender) return;

            _client = client;
            _clientStream = _client.GetStream();
            OperationCancellationToken = new CancellationTokenSource();
            IsAvailable = true;

            DebugUtil.LogNormal<GsTcpClient>(
                Type == GSLiveType.TurnBased ? DebugLocation.TurnBased : DebugLocation.Command, "OnTcpClientConnected",
                "GsTcpClient -> Init Done");

            CoreEventHandlers.OnGsTcpClientConnected?.Invoke(sender, client);
        }


        internal override async Task Init(CommandInfo info)
        {
            try
            {
                CommandInfo = info;
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

            while (IsAvailable)
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
                            Packet = PacketDeserializer.Deserialize(packet)
                        });
                    BufferReceivedBytes = 0;
                }
                catch (Exception e)
                {
                    e.LogException<GsTcpClient>(
                        Type == GSLiveType.TurnBased ? DebugLocation.TurnBased : DebugLocation.Command,
                        "StartReceiving");

                    if (!(e is OperationCanceledException || e is ObjectDisposedException ||
                          e is ArgumentOutOfRangeException))
                        OnClosed(new ErrorArg {Error = e.ToString()});
                    break;
                }
        }


        internal override void Send(Packet packet)
        {
            Task.Run(() =>
            {
                var buffer = PacketSerializer.Serialize(packet);
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
                var buffer = PacketSerializer.Serialize(packet);
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
                IsAvailable = false;
                OperationCancellationToken?.Cancel(false);
                OperationCancellationToken?.Dispose();

                _client?.GetStream().Close();
                _client?.Close();
                _client = null;

                OperationCancellationToken = null;

                DebugUtil.LogNormal<GsTcpClient>(
                    Type == GSLiveType.TurnBased ? DebugLocation.TurnBased : DebugLocation.Command, "StopReceiving",
                    "GsTcpClient -> StopReceiving Done");
            }
            catch (Exception e)
            {
                e.LogException<GsTcpClient>(
                    Type == GSLiveType.TurnBased ? DebugLocation.TurnBased : DebugLocation.Command, "StopReceiving");
            }
        }
    }
}