// <copyright file="FriendData.cs" company="Firoozeh Technology LTD">
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
using FiroozehGameService.Models.GSLive;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.BasicApi.Social
{
    /// <summary>
    ///     Represents Friend Data Model In Game Service Social Basic API
    /// </summary>
    [Serializable]
    public class FriendData
    {
        /// <summary>
        ///     Gets the Friend Request Accept Time
        /// </summary>
        /// <value>the Friend Request Accept Time</value>
        [JsonProperty("accepted_at")] public DateTimeOffset AcceptedTime;

        /// <summary>
        ///     Gets the Friend Member Data.
        ///     The Friend Member or Requester
        /// </summary>
        /// <value>the Friend Member Data</value>
        [JsonProperty("member")] public Member Member;


        /// <summary>
        ///     Gets the Friend Request Time
        /// </summary>
        /// <value>the Friend Request Time</value>
        [JsonProperty("requested_at")] public DateTimeOffset RequestedTime;

        internal FriendData()
        {
        }
    }
}