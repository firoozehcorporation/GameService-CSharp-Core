// <copyright file="GProtocolClient.cs" company="Firoozeh Technology LTD">
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
using FiroozehGameService.Core.Socket.PacketHelper;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.EventArgs;
using FiroozehGameService.Models.GSLive.Command;
using GClient = GProtocol.GProtocolClient;
using Packet = FiroozehGameService.Models.GSLive.RT.Packet;

namespace FiroozehGameService.Core.Socket
{
    internal abstract class GProtocolClient
    {
        internal abstract void Init();

        internal abstract void CreateInstance();

        internal abstract void StopReceiving(bool isGraceful);

        internal abstract void Send(Packet packet, GProtocolSendType type, bool canSendBigSize = false,
            bool isCritical = false, bool isEvent = false);

        internal abstract int GetRtt();

        internal abstract long GetPacketLost();

        internal abstract bool IsConnected();


        protected void OnDataReceived(SocketDataReceived arg)
        {
            DataReceived?.Invoke(this, arg);
        }

        protected void OnClosed(ErrorArg errorArg)
        {
            Error?.Invoke(this, errorArg);
        }

        #region GProtocolClient

        protected GClient Client;
        protected Area Area;

        protected readonly ISerializer PacketSerializable = new PacketSerializer();
        protected readonly IDeserializer PacketDeserializer = new PacketDeserializer();

        protected internal event EventHandler<SocketDataReceived> DataReceived;
        protected internal event EventHandler<ErrorArg> Error;

        #endregion
    }
}