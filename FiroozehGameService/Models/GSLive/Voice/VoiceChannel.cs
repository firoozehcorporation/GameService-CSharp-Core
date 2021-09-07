// <copyright file="VoiceChannel.cs" company="Firoozeh Technology LTD">
// Copyright (C) 2021 Firoozeh Technology LTD. All Rights Reserved.
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

namespace FiroozehGameService.Models.GSLive.Voice
{
    /// <summary>
    ///     Represents VoiceChannel Data Model In GameService Voice System
    /// </summary>
    [Serializable]
    internal class VoiceChannel
    {
        /// <summary>
        ///     Gets the Voice Channel Created time
        /// </summary>
        /// <value>the Voice Channel Created time</value>
        [JsonProperty("5")] internal DateTimeOffset ChannelCreatedAt;

        /// <summary>
        ///     Gets the Voice Channel CreatorMemberId
        /// </summary>
        /// <value>the Voice Channel CreatorMemberId</value>
        [JsonProperty("4")] internal string ChannelCreatorMemberId;

        /// <summary>
        ///     Gets the Voice Channel Description
        /// </summary>
        /// <value>the Voice Channel Description</value>
        [JsonProperty("3")] internal string ChannelDescription;

        /// <summary>
        ///     Gets the Voice Channel Game Id
        /// </summary>
        /// <value>the Voice Channel Game Id</value>
        [JsonProperty("6")] internal DateTimeOffset ChannelGameId;

        /// <summary>
        ///     Gets the Voice Channel Id
        /// </summary>
        /// <value>the Voice Channel Id</value>
        [JsonProperty("0")] internal string ChannelId;

        /// <summary>
        ///     Gets the Voice Channel Key
        /// </summary>
        /// <value>the Voice Channel Key</value>
        [JsonProperty("1")] internal string ChannelKey;

        /// <summary>
        ///     Gets the Voice Channel Name
        /// </summary>
        /// <value>the Voice Channel Name</value>
        [JsonProperty("2")] internal string ChannelName;
    }
}