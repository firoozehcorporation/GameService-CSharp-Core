// <copyright file="Event.cs" company="Firoozeh Technology LTD">
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

using System;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.GSLive;
using Newtonsoft.Json;

/**
* @author Alireza Ghodrati
*/
namespace FiroozehGameService.Models.BasicApi.Social
{
    /// <summary>
    ///     Represents Event Data Model In Game Service Basic API
    /// </summary>
    [Serializable]
    public class Event
    {
        /// <summary>
        ///     Gets the Event Action.
        /// </summary>
        /// <value>the Event Action</value>
        [JsonProperty("action")] public EventAction Action;


        /// <summary>
        ///     Gets the Event Extra Data
        ///     NOTE : Extra Data Is NULLABLE
        ///     USAGE : Available When Action is <see cref="EventAction.ChangeMemberRole" />
        /// </summary>
        /// <value>the Event Extra Data</value>
        [JsonProperty("extra")] public string Extra;


        /// <summary>
        ///     Gets the Member who Creates this Event.
        /// </summary>
        /// <value>the Event ID</value>
        [JsonProperty("who")] public Member FromMember;

        
        /// <summary>
        ///     Gets the Game That Creates this Event.
        /// </summary>
        /// <value>the Game</value>
        [JsonProperty("game")] public Game FromGame;


        /// <summary>
        ///     Gets the Party Data
        ///     NOTE : Party Data Only Available in Party Actions
        /// </summary>
        /// <value>the Party Data</value>
        [JsonProperty("party")] public Member FromParty;

        /// <summary>
        ///     Gets the Event ID.
        /// </summary>
        /// <value>the Event ID</value>
        [JsonProperty("_id")] public string Id;


        /// <summary>
        ///     Gets the Event Time
        /// </summary>
        /// <value>the Event Time</value>
        [JsonProperty("at")] public DateTimeOffset Time;


        public override string ToString()
        {
            return "Achievement{" +
                   "ID='" + Id + '\'' +
                   ", Action='" + Action + '\'' +
                   ", Member='" + FromMember + '\'' +
                   ", Party=" + FromParty +
                   ", Extra='" + Extra + '\'' +
                   ", At=" + Time +
                   '}';
        }
    }
}