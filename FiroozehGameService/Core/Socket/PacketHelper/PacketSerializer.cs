// <copyright file="PacketSerializer.cs" company="Firoozeh Technology LTD">
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
using System.Collections.Generic;
using System.Text;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.GSLive;
using FiroozehGameService.Models.GSLive.Command;
using FiroozehGameService.Utils;
using Newtonsoft.Json;

namespace FiroozehGameService.Core.Socket.PacketHelper
{
    internal class PacketSerializer : ISerializer
    {
        public byte[] Serialize(APacket packet, string key, bool isCommand)
        {
            try
            {
                return packet.Serialize(key, isCommand);
            }
            catch (Exception e)
            {
                DebugUtil.LogError<PacketDeserializer>(DebugLocation.Internal, "Serialize", e.Message);
                return null;
            }
        }

        public byte[] Serialize(List<Packet> packets, string key, bool isCommand)
        {
            foreach (var packet in packets) packet.Encrypt(key, isCommand);

            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(packets, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            }));
        }
    }
}