// <copyright file="Room.cs" company="Firoozeh Technology LTD">
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
using FiroozehGameService.Models.Enums.GSLive;
using Newtonsoft.Json;


/**
* @author Alireza Ghodrati
*/


namespace FiroozehGameService.Models.GSLive
{
    /// <summary>
    ///     Represents Room Data Model In GameService MultiPlayer System (GSLive)
    /// </summary>
    [Serializable]
    public class Room
    {
        /// <summary>
        ///     Gets the Room Creator Id.
        /// </summary>
        /// <value>the Room Creator Id</value>
        [JsonProperty("creator")] public string Creator;


        /// <summary>
        ///     Gets the Game Id
        /// </summary>
        /// <value>the the Game Id</value>
        [JsonProperty("game")] public string GameId;

        /// <summary>
        ///     Gets the Room Type
        /// </summary>
        /// <value>the Room Type</value>
        [JsonProperty("syncMode")] public GSLiveType GsLiveType;

        /// <summary>
        ///     Gets the Room id.
        ///     You Can Use it In MultiPlayer Functions that Needs Room id
        /// </summary>
        /// <value>the Room id</value>
        [JsonProperty("_id")] public string Id;


        /// <summary>
        ///     Gets the Room Persistant Value.
        ///     it Set in GSLiveOption <see cref="FiroozehGameService.Core.GSLive.GSLiveOption" />
        /// </summary>
        /// <value>the Room Persistant Value</value>
        [JsonProperty("persist")] public bool IsPersist;


        /// <summary>
        ///     Gets the Room Privacy Value.
        ///     it Set in GSLiveOption <see cref="FiroozehGameService.Core.GSLive.GSLiveOption" />
        /// </summary>
        /// <value>the Room Privacy Value</value>
        [JsonProperty("private")] public bool IsPrivate;


        /// <summary>
        ///     Gets the Room Logo URL.
        /// </summary>
        /// <value>the Room Logo URL</value>
        [JsonProperty("logo")] public string Logo;


        /// <summary>
        ///     Gets the Room Minimum Member Value To Accept.
        ///     it Set in GSLiveOption <see cref="FiroozehGameService.Core.GSLive.GSLiveOption" />
        /// </summary>
        /// <value>the Room Maximum Member Value To Accept</value>
        [JsonProperty("max")] public int Max;


        /// <summary>
        ///     Gets the Room Minimum Member Value To Accept.
        ///     it Set in GSLiveOption <see cref="FiroozehGameService.Core.GSLive.GSLiveOption" />
        /// </summary>
        /// <value>the Room Minimum Member Value To Accept</value>
        [JsonProperty("min")] public int Min;


        /// <summary>
        ///     Gets the Room Name.
        ///     it Set in GSLiveOption <see cref="FiroozehGameService.Core.GSLive.GSLiveOption" />
        /// </summary>
        /// <value>the Room Name</value>
        [JsonProperty("name")] public string Name;

        /// <summary>
        ///     Gets the Room Players Member Id.
        /// </summary>
        /// <value>the Room Players Member Id</value>
        [JsonProperty("members")] public string[] PlayersId;


        /// <summary>
        ///     Gets the Room Role Value.
        ///     it Set in GSLiveOption <see cref="FiroozehGameService.Core.GSLive.GSLiveOption" />
        /// </summary>
        /// <value>the Room Role Value</value>
        [JsonProperty("role")] public string Role;


        /// <summary>
        ///     Gets the Room Status Value.
        /// </summary>
        /// <value>the Room Status Value</value>
        [JsonProperty("status")] public int Status;


        internal Room()
        {
        }
    }
}