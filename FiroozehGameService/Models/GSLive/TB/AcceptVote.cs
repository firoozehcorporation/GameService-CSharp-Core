// <copyright file="AcceptVote.cs" company="Firoozeh Technology LTD">
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

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

/**
* @author Alireza Ghodrati
*/

namespace FiroozehGameService.Models.GSLive.TB
{
    /// <summary>
    ///     Represents AcceptVote Data Model In GameService TurnBased MultiPlayer System
    /// </summary>
    [Serializable]
    public class AcceptVote
    {
        /// <summary>
        ///     Gets the Accept Counts from Other Players.
        /// </summary>
        /// <value>the Accept Counts from Other Players</value>
        [JsonProperty("1")] public int AcceptCounts;


        /// <summary>
        ///     Gets the Game Result(Outcomes).
        ///     (Type : Dictionary(MemberID,Outcome))
        /// </summary>
        /// <value>the Game Result(Outcomes)</value>
        [JsonProperty("2")] public Dictionary<string, Outcome> Result;
    }
}