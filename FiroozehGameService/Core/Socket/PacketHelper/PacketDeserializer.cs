// <copyright file="PacketDeserializer.cs" company="Firoozeh Technology LTD">
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
using System.Linq;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.GSLive;
using FiroozehGameService.Models.GSLive.RT;
using FiroozehGameService.Utils;
using Newtonsoft.Json;

namespace FiroozehGameService.Core.Socket.PacketHelper
{
    internal class PacketDeserializer : IDeserializer
    {
        public APacket Deserialize(byte[] buffer, int offset, int receivedBytes)
        {
            try
            {
                var seg = new ArraySegment<byte>(buffer, offset, receivedBytes);
                return new Packet(seg.ToArray());
            }
            catch (Exception e)
            {
                DebugUtil.LogError<PacketDeserializer>(DebugLocation.Internal, "DeserializeTypeOne", e.Message);
                return null;
            }
        }

        public APacket Deserialize(string buffer, string key = null)
        {
            try
            {
                var packet = JsonConvert.DeserializeObject<Models.GSLive.Command.Packet>(buffer);
                packet.DecryptPacket(key);

                return packet;
            }
            catch (Exception e)
            {
                DebugUtil.LogError<PacketDeserializer>(DebugLocation.Internal, "DeserializeTypeTwo", e.Message);
                return null;
            }
        }
    }
}