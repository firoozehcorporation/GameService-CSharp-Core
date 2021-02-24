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
using GProtocol.Public;
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
                Client?.Connect(Convert.FromBase64String(Area.ConnectToken));
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
                Client = new Client();

                Client.OnStateChanged += ClientOnOnStateChanged;
                Client.OnMessageReceived += ClientOnOnMessageReceived;

                DebugUtil.LogNormal<GsUdpClient>(DebugLocation.RealTime, "CreateInstance", "GsUdpClient Created");
            }
            else
            {
                DebugUtil.LogError<GsUdpClient>(DebugLocation.RealTime, "CreateInstance", "Token Is NULL");
            }
        }


        private void ClientOnOnMessageReceived(byte[] payload, int payloadsize)
        {
            OnDataReceived(new SocketDataReceived
            {
                Packet = PacketDeserializer.Deserialize(payload, 0, payloadsize),
                Time = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
            });
        }


        private void ClientOnOnStateChanged(ClientState state)
        {
            switch (state)
            {
                case ClientState.Connected:
                    IsAvailable = true;
                    CoreEventHandlers.GProtocolConnected?.Invoke(null, null);
                    break;
                case ClientState.InvalidConnectToken:
                case ClientState.ConnectionTimedOut:
                case ClientState.ChallengeResponseTimedOut:
                case ClientState.ConnectionRequestTimedOut:
                case ClientState.ConnectionDenied:
                    IsAvailable = false;
                    Client?.Disconnect();
                    Client = null;

                    OnClosed(new ErrorArg {Error = state.ToString()});
                    break;
                case ClientState.ConnectTokenExpired:
                case ClientState.Disconnected:
                case ClientState.SendingConnectionRequest:
                case ClientState.SendingChallengeResponse:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }


        internal override void Send(Packet packet, GProtocolSendType type, bool canSendBigSize = false)
        {
            if (Client?.State == ClientState.Connected)
            {
                packet.SendType = type;
                var buffer = PacketSerializable.Serialize(packet);

                if (!canSendBigSize && !PacketUtil.CheckPacketSize(buffer))
                    throw new GameServiceException("this Packet Is Too Big!,Max Packet Size is " +
                                                   RealTimeConst.MaxPacketSize + " bytes.")
                        .LogException<GsUdpClient>(DebugLocation.RealTime, "Send");

                Client?.Send(buffer, buffer.Length);
            }
            else
            {
                DebugUtil.LogError<GsUdpClient>(DebugLocation.RealTime, "Send", "Client not Connected!");
            }
        }

        internal override void StopReceiving()
        {
            try
            {
                Client?.Disconnect();
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