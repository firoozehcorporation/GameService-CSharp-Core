// <copyright file="Message.cs" company="Firoozeh Technology LTD">
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
    internal class Message
    {
        [JsonProperty("3")] internal string Data;
        [JsonProperty("5")] internal string Id;
        [JsonProperty("0")] internal bool IsPrivate;
        [JsonProperty("6")] internal string Property;
        [JsonProperty("1")] internal string ReceiverId;
        [JsonProperty("2")] internal string SenderId;
        [JsonProperty("4")] internal long Time;

        internal Message(bool isPrivate, string receiverId, string data, string property = null)
        {
            IsPrivate = isPrivate;
            ReceiverId = receiverId;
            Data = data;
            Property = property;
        }


        public override string ToString()
        {
            return "Message{" +
                   "isPrivate=" + IsPrivate +
                   ", ReceiverID='" + ReceiverId + '\'' +
                   ", Sender='" + SenderId + '\'' +
                   ", Data='" + Data + '\'' +
                   ", Time=" + Time +
                   '}';
        }
    }
}