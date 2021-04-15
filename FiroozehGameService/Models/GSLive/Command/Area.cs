// <copyright file="Area.cs" company="Firoozeh Technology LTD">
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
using Newtonsoft.Json;

namespace FiroozehGameService.Models.GSLive.Command
{
    [Serializable]
    internal class Area
    {
        [JsonProperty("4")] public string Cert;
        [JsonProperty("5")] public byte ChannelId;
        [JsonProperty("3")] public string ConnectToken;
        [JsonProperty("0")] public string Ip;
        [JsonProperty("2")] public int Port;
        [JsonProperty("1")] public string Protocol;

        public override string ToString()
        {
            return "Area{" +
                   "EndPoint='" + Ip + '\'' +
                   ", Protocol='" + Protocol + '\'' +
                   ", Port='" + Port + '\'' +
                   ", ConnectToken='" + ConnectToken + '\'' +
                   ", Cert='" + Cert + '\'' +
                   ", ChannelId='" + ChannelId + '\'' +
                   '}';
        }
    }
}