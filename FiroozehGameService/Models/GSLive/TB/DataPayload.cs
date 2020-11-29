// <copyright file="DataPayload.cs" company="Firoozeh Technology LTD">
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
using Newtonsoft.Json;

namespace FiroozehGameService.Models.GSLive.TB
{
    [Serializable]
    internal class DataPayload : Payload
    {
        [JsonProperty("0")] public int Action;
        [JsonProperty("2")] public string Data;
        [JsonProperty("1")] public string Id;
        [JsonProperty("5")] public bool IsPrivate;
        [JsonProperty("3")] public string NextId;
        [JsonProperty("4")] public Dictionary<string, Outcome> Outcomes;


        public override string ToString()
        {
            return "DataPayload{" +
                   "Action=" + Action +
                   ", ID='" + Id + '\'' +
                   ", Data='" + Data + '\'' +
                   ", Next='" + NextId + '\'' +
                   ", Outcomes=" + Outcomes +
                   ", Private=" + IsPrivate +
                   '}';
        }
    }
}