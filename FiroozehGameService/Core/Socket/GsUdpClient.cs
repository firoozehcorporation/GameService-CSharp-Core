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
            CreateInstance();
        }

        public bool IsAvailable { get; private set; }


        internal override void Init()
        {
            try
            {
                if (Client == null) CreateInstance();
                Client?.Connect(Convert.FromBase64String(Area.ConnectToken));
                LogUtil.Log(this, "GsUdpClient Init");
            }
            catch (Exception e)
            {
                LogUtil.Log(this, "GsUdpClient Err : " + e);
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

                LogUtil.Log(this, "GsUdpClient Created");
                DebugUtil.LogNormal<GsUdpClient>(DebugLocation.RealTime,"CreateInstance","GsUdpClient Created");
            }
            else
            {
                LogUtil.LogError(this, "Token Is NULL");
                DebugUtil.LogError<GsUdpClient>(DebugLocation.RealTime,"CreateInstance","Token Is NULL");
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
            LogUtil.Log(this, "Client_OnStateChanged : " + state);
            DebugUtil.LogNormal<GsUdpClient>(DebugLocation.RealTime,"ClientOnOnStateChanged",state.ToString());


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
                case ClientState.ConnectTokenExpired: break;
                case ClientState.Disconnected: break;
            }
        }


        internal override void Send(Packet packet, GProtocolSendType type, bool canSendBigSize = false)
        {
            if (Client?.State == ClientState.Connected)
            {
                packet.SendType = type;
                if (packet.Action == RealTimeConst.ActionPing)
                    packet.ClientSendTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                var buffer = PacketSerializable.Serialize(packet);

                if (!canSendBigSize)
                    if (!PacketUtil.CheckPacketSize(buffer))
                    {
                        LogUtil.LogError(this,
                            "this Packet Is Too Big!,Max Packet Size is " + RealTimeConst.MaxPacketSize + " bytes.");
                        throw new GameServiceException("this Packet Is Too Big!,Max Packet Size is " +
                                                       RealTimeConst.MaxPacketSize + " bytes.")
                            .LogException<GsUdpClient>(DebugLocation.RealTime,"Send");
                    }
                
                LogUtil.Log(this, "RealTime Send Payload Len : " + buffer.Length);

                Client?.Send(buffer, buffer.Length);
            }
            else
            {
                LogUtil.LogError(this, "Client not Connected!");
                DebugUtil.LogError<GsUdpClient>(DebugLocation.RealTime,"Send","Client not Connected!");
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
                e.LogException<GsUdpClient>(DebugLocation.RealTime,"StopReceiving");
            }

            Client = null;
            IsAvailable = false;
        }
    }
}