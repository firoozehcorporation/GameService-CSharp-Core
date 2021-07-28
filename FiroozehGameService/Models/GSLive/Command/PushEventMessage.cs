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
using Newtonsoft.Json;

namespace FiroozehGameService.Models.GSLive.Command
{
    [Serializable]
    internal class PushEventMessage
    {
        [JsonProperty("2")] internal string Data;
        [JsonProperty("4")] internal bool IsBuffering;
        [JsonProperty("0")] internal bool IsMember;
        [JsonProperty("3")] internal int NextSecs;
        [JsonProperty("1")] internal string ReceiverId;


        internal PushEventMessage()
        {
        }

        internal PushEventMessage(bool isMember, string receiverId = null, string data = null, int nextSecs = 0,
            bool isBuffering = false)
        {
            IsMember = isMember;
            ReceiverId = receiverId;
            Data = data;
            NextSecs = nextSecs;
            IsBuffering = isBuffering;
        }
    }
}