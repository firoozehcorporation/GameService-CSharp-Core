// <copyright file="Invite.cs" company="Firoozeh Technology LTD">
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
using Newtonsoft.Json;

/**
* @author Alireza Ghodrati
*/


namespace FiroozehGameService.Models.GSLive
{
    /// <summary>
    ///     Represents Invite Data Model In GameService MultiPlayer System (GSLive)
    /// </summary>
    [Serializable]
    public class Invite
    {
        /// <summary>
        ///     Gets The Game Which You Are Invited.
        /// </summary>
        /// <value>The Game Where You Are Invited</value>
        [JsonProperty("game")] public Game Game;

        /// <summary>
        ///     Gets the Invite id.
        ///     You Can Use It in Accept Invite
        /// </summary>
        /// <value>the Invite id</value>
        [JsonProperty("_id")] public string Id;


        /// <summary>
        ///     Gets the Invited User Id.
        /// </summary>
        /// <value>the Invited User Id</value>
        [JsonProperty("invited")] public string Invited;


        /// <summary>
        ///     Gets the Inviter Member.
        /// </summary>
        /// <value>the Inviter Member</value>
        [JsonProperty("inviter")] public Member Inviter;


        /// <summary>
        ///     Gets The Room Where You Are Invited.
        /// </summary>
        /// <value>The Room Where You Are Invited</value>
        [JsonProperty("room")] public Room Room;
    }
}