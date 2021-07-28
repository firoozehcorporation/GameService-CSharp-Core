// <copyright file="PushEvent.cs" company="Firoozeh Technology LTD">
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
using FiroozehGameService.Models.Enums.GSLive;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.GSLive
{
    /// <summary>
    ///     Represents Push Event Data Model In GameService MultiPlayer System (GSLive)
    /// </summary>
    [Serializable]
    public class PushEvent
    {
        /// <summary>
        ///     Gets the Event Buffer Type.
        /// </summary>
        /// <value>the Event Buffer Type</value>
        [JsonProperty("4")] public PushEventBufferType BufferType;

        /// <summary>
        ///     Gets the Event Data.
        /// </summary>
        /// <value>the Event Data</value>
        [JsonProperty("2")] public string Data;

        /// <summary>
        ///     Gets the Event Receiver Id Or Tag
        /// </summary>
        /// <value>the Event Receiver Id Or Tag</value>
        [JsonProperty("1")] public string MemberIdOrTag;

        /// <summary>
        ///     Gets the Event Send Type.
        /// </summary>
        /// <value>the Event Send Type</value>
        [JsonProperty("0")] public PushEventSendType SendType;


        internal PushEvent()
        {
        }
    }
}