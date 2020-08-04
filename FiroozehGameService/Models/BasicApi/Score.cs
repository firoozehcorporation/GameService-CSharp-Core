// <copyright file="Score.cs" company="Firoozeh Technology LTD">
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
using FiroozehGameService.Models.GSLive;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.BasicApi
{
    /// <summary>
    ///     Represents Score Data Model In Game Service Basic API
    /// </summary>
    [Serializable]
    public class Score
    {
        /// <summary>
        ///     Gets the Member Submit this Score.
        /// </summary>
        /// <value>the Member Submit this Score</value>
        [JsonProperty("member")] public Member Submitter;

        /// <summary>
        ///     Gets the Tries Count of This Score.
        /// </summary>
        /// <value>the Tries Count of This Score</value>
        [JsonProperty("tries")] public int Tries;

        /// <summary>
        ///     Gets the Value of This Score.
        /// </summary>
        /// <value>the Value of This Score</value>
        [JsonProperty("value")] public int Value;
        
        
        /// <summary>
        ///     Gets the Rank of This Score.
        /// </summary>
        /// <value>the Rank of This Score</value>
        [JsonProperty("rank")] public int Rank;
    }
}