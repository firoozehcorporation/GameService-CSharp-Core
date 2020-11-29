// <copyright file="AutoMatch.cs" company="Firoozeh Technology LTD">
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
    internal class AutoMatch
    {
        [JsonProperty("accept")] public bool Accept;
        [JsonProperty("max")] public int Max;

        [JsonProperty("min")] public int Min;
        [JsonProperty("role")] public string Role;

        public override string ToString()
        {
            return "AutoMatch{" +
                   "max=" + Max +
                   ", min=" + Min +
                   ", role='" + Role + '\'' +
                   ", accept=" + Accept +
                   '}';
        }
    }
}