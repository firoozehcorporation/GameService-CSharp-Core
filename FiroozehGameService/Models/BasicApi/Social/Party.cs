// <copyright file="Party.cs" company="Firoozeh Technology LTD">
// Copyright (C) 2020 Firoozeh Technology LTD. All Rights Reserved.
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

namespace FiroozehGameService.Models.BasicApi.Social
{
    /// <summary>
    ///     Represents Party Data Model In Game Service Social Basic API
    /// </summary>
    [Serializable]
    public class Party
    {
        /// <summary>
        ///     Gets the Party Create Time
        /// </summary>
        /// <value>the Party Create Time</value>
        [JsonProperty("created_at")] public DateTimeOffset CreateTime;


        /// <summary>
        ///     Gets the Party Description.
        /// </summary>
        /// <value>the Party Description</value>
        [JsonProperty("desc")] public string Description;

        /// <summary>
        ///     Gets the Party ID
        /// </summary>
        /// <value>the Party ID</value>
        [JsonProperty("_id")] public string Id;

        /// <summary>
        ///     Gets the Party Logo URL.
        /// </summary>
        /// <value>the Party Logo URL</value>
        [JsonProperty("logo")] public string Logo;


        /// <summary>
        ///     Gets the Max Member Count.
        /// </summary>
        /// <value>the Max Member Count</value>
        [JsonProperty("max")] public int MaxMember;


        /// <summary>
        ///     Gets the Member Count.
        /// </summary>
        /// <value>the Member Count</value>
        [JsonProperty("members_count")] public int MemberCount;


        /// <summary>
        ///     Gets the Party Name.
        /// </summary>
        /// <value>the Party Name</value>
        [JsonProperty("name")] public string Name;


        /// <summary>
        ///     Gets the Party Variables
        ///     NOTE : (NULLABLE) Only Available if Member is Admin or Creator
        /// </summary>
        /// <value>the Party Variables</value>
        [JsonProperty("variables")] public Dictionary<string, string> Variables;

        internal Party()
        {
        }
    }
}