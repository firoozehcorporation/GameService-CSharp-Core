// <copyright file="GsUdpClient.cs" company="Firoozeh Technology LTD">
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
using FiroozehGameService.Handlers;
using FiroozehGameService.Models;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.EventArgs;
using FiroozehGameService.Models.GSLive.Command;
using FiroozehGameService.Utils;
using GProtocol.Models;
using GProtocol.Utils;
using GClient = GProtocol.GProtocolClient;
using Packet = FiroozehGameService.Models.GSLive.RT.Packet;

namespace FiroozehGameService.Core.Socket
{
    internal class GsUdpClient : GProtocolClient
    {
        public GsUdpClient(Area endpoint)
        {
            Area = endpoint;
            IsAvailable = false;
            CreateInstance();
        }

        internal static bool IsAvailable { get; private set; }


        internal override void Init()
        {
            try
            {
                if (Client == null) CreateInstance();
                Client?.Connect(Area.Ip, (ushort) Area.Port);
            }
            catch (Exception e)
            {
                e.LogException<GsUdpClient>(DebugLocation.RealTime, "Init");
            }
        }

        internal sealed override void CreateInstance()
        {
            if (Area?.ConnectToken != null)
            {
                Client = new GClient(GameService.Configuration.PlatformType);

                Client.OnConnect += OnConnect;
                Client.OnDisconnect += OnDisconnect;
                Client.OnTimeout += OnTimeout;
                Client.OnReceive += OnReceive;

                DebugUtil.LogNormal<GsUdpClient>(DebugLocation.RealTime, "CreateInstance", "GsUdpClient Created");
            }
            else
            {
                DebugUtil.LogError<GsUdpClient>(DebugLocation.RealTime, "CreateInstance", "Token Is NULL");
            }
        }

        private static void OnConnect(object sender, EventArgs e)
        {
            IsAvailable = true;
            CoreEventHandlers.GProtocolConnected?.Invoke(null, null);
        }


        private void OnTimeout(object sender, EventArgs args)
        {
            try
            {
                IsAvailable = false;
                Client?.Disconnect(0);
                Client?.Dispose();
                Client = null;

                OnClosed(new ErrorArg {Error = "Client Timeout"});
            }
            catch (Exception e)
            {
                e.LogException<GsUdpClient>(DebugLocation.RealTime, "OnTimeout");
            }
        }

        private static void OnDisconnect(object sender, EventArgs e)
        {
            DebugUtil.LogNormal<GsUdpClient>(DebugLocation.RealTime, "OnDisconnect", "GsUdpClient Disconnected");
        }

        private void OnReceive(object sender, ReceiveData data)
        {
            var buffer = data.Packet.Payload;
            OnDataReceived(new SocketDataReceived
            {
                Packet = PacketDeserializer.Deserialize(buffer, 0, buffer.Length),
                Time = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
            });
        }


        internal override void Send(Packet packet, GProtocolSendType type, bool canSendBigSize = false,
            bool isCritical = false,bool isEvent = false)
        {
            try
            {
                if (Client?.GetStatus() == PeerState.Connected)
                {
                    packet.SendType = type;
                    var buffer = PacketSerializable.Serialize(packet);

                    if (!canSendBigSize && !PacketUtil.CheckPacketSize(buffer))
                        throw new GameServiceException("this Packet Is Too Big!,Max Packet Size is " +
                                                       RealTimeConst.MaxPacketSize + " bytes.")
                            .LogException<GsUdpClient>(DebugLocation.RealTime, "Send");

                    switch (type)
                    {
                        case GProtocolSendType.UnReliable:
                            Client?.SendUnReliable(buffer);
                            break;
                        case GProtocolSendType.Reliable:
                            if (isCritical) Client?.SendCommand(buffer);
                            else Client?.SendReliable(buffer,isEvent);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(type), type, null);
                    }
                }
                else
                {
                    DebugUtil.LogError<GsUdpClient>(DebugLocation.RealTime, "Send", "Client not Connected!");
                }
            }
            catch (Exception e)
            {
                e.LogException<GsUdpClient>(DebugLocation.RealTime, "Send");
            }
        }

        internal override int GetRtt()
        {
            try
            {
                if (Client == null) return -1;
                return (int) Client.GetRtt();
            }
            catch (Exception)
            {
                return -1;
            }
        }

        internal override long GetPacketLost()
        {
            try
            {
                if (Client == null) return -1;
                return (long) Client.GetPacketLost();
            }
            catch (Exception)
            {
                return -1;
            }
        }

        internal override void StopReceiving()
        {
            try
            {
                Client?.Disconnect(0);
                Client?.Dispose();
            }
            catch (Exception e)
            {
                e.LogException<GsUdpClient>(DebugLocation.RealTime, "StopReceiving");
            }

            Client = null;
            IsAvailable = false;
        }
    }
}