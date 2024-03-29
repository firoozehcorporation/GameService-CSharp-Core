// <copyright file="Packet.cs" company="Firoozeh Technology LTD">
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
using FiroozehGameService.Utils.Encryptor;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.GSLive.Command
{
    [Serializable]
    internal class Packet : APacket
    {
        [JsonProperty("1")] public int Action;
        [JsonProperty("2")] public string Data;
        [JsonProperty("3")] public string Message;
        [JsonProperty("0")] public string Token;

        public Packet(string token, int action, string data = null, string message = null)
        {
            Token = token;
            Action = action;
            Data = data;
            Message = message;
        }

        public override string ToString()
        {
            return "Packet{" +
                   "Hash='" + Token + '\'' +
                   ", Action=" + Action +
                   ", Data='" + Data + '\'' +
                   ", Message='" + Message + '\'' +
                   '}';
        }


        internal override byte[] Serialize(string key, bool isCommand)
        {
            if (this.IsEncryptionEnabled(isCommand, true)) this.EncryptPacket(key);
            return ConvertToBytes(JsonConvert.SerializeObject(this, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            }));
        }

        internal override void Encrypt(string key, bool isCommand)
        {
            if (this.IsEncryptionEnabled(isCommand, true))
                this.EncryptPacket(key);
        }

        internal override void Deserialize(byte[] buffer)
        {
        }

        internal override int BufferSize(short prefixLen)
        {
            return 0;
        }
    }
}