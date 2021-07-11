// <copyright file="AuthPayload.cs" company="Firoozeh Technology LTD">
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
    internal class AuthPayload : Payload
    {
        [JsonProperty("0")] internal string GameId;
        [JsonProperty("3")] internal string RealTimeConnectionType;
        [JsonProperty("1")] internal string Token;
        [JsonProperty("2")] internal string TurnBasedConnectionType;

        internal AuthPayload(string gameId, string token, string turnBasedConnectionType = null,
            string realTimeConnectionType = null)
        {
            GameId = gameId;
            Token = token;
            TurnBasedConnectionType = turnBasedConnectionType;
            RealTimeConnectionType = realTimeConnectionType;
        }
    }
}