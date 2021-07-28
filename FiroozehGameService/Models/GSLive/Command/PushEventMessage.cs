// <copyright file="PushEventMessage.cs" company="Firoozeh Technology LTD">
// Copyright (C) 2021 Firoozeh Technology LTD. All Rights Reserved.
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
using FiroozehGameService.Models.Enums.GSLive;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.GSLive.Command
{
    [Serializable]
    internal class PushEventMessage
    {
        [JsonProperty("4")] internal PushEventBufferType BufferType;
        [JsonProperty("2")] internal string Data;
        [JsonProperty("3")] internal int NextSecs;
        [JsonProperty("1")] internal string ReceiverId;
        [JsonProperty("0")] internal PushEventSendType SendType;


        internal PushEventMessage()
        {
        }

        internal PushEventMessage(PushEventSendType sendType, string receiverId = null, string data = null,
            int nextSecs = 0,
            PushEventBufferType bufferType = PushEventBufferType.NoBuffering)
        {
            SendType = sendType;
            ReceiverId = receiverId;
            Data = data;
            NextSecs = nextSecs;
            BufferType = bufferType;
        }
    }
}