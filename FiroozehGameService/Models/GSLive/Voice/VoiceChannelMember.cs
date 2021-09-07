// <copyright file="VoiceChannelMember.cs" company="Firoozeh Technology LTD">
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
    ///     Represents VoiceChannelMember Data Model In GameService Voice System
    /// </summary>
    [Serializable]
    internal class VoiceChannelMember
    {
        /// <summary>
        ///     Gets the Member Voice Deafen Status
        /// </summary>
        /// <value>the  Member Voice Deafen Status</value>
        [JsonProperty("2")] internal bool IsDeafen;


        /// <summary>
        ///     Gets the Member Kicked Status
        /// </summary>
        /// <value>the  Member Kicked Status</value>
        [JsonProperty("3")] internal bool IsKickedPermanently;

        /// <summary>
        ///     Gets the Member Voice Mute Status
        /// </summary>
        /// <value>the Member Voice Mute Status</value>
        [JsonProperty("1")] internal bool IsMuted;

        /// <summary>
        ///     Gets the Member Data
        /// </summary>
        /// <value>the Member Data</value>
        [JsonProperty("0")] internal Member Member;
    }
}