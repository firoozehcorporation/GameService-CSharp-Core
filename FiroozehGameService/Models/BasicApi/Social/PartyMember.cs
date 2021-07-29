// <copyright file="PartyMember.cs" company="Firoozeh Technology LTD">
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
using FiroozehGameService.Models.GSLive;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.BasicApi.Social
{
    /// <summary>
    ///     Represents Party Data Model In Game Service Social Basic API
    /// </summary>
    [Serializable]
    public class PartyMember
    {
        /// <summary>
        ///     Gets the Party Member Data.
        /// </summary>
        /// <value>the Party Member Data</value>
        [JsonProperty("member")] public Member Member;


        /// <summary>
        ///     Gets the Party Member Role.
        /// </summary>
        /// <value>the Party Member Role</value>
        [JsonProperty("role")] public string Role;


        /// <summary>
        ///     Gets the Party Member Variables
        /// </summary>
        /// <value>the Party Member Variables</value>
        [JsonProperty("variables")] public Dictionary<string, string> Variables;


        internal PartyMember()
        {
        }
    }
}