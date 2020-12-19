// <copyright file="Game.cs" company="Firoozeh Technology LTD">
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
using Newtonsoft.Json;

namespace FiroozehGameService.Models.BasicApi
{
    /// <summary>
    ///     Represents Game Data Model In Game Service Basic API
    /// </summary>
    [Serializable]
    public class Game
    {
        /// <summary>
        ///     Gets the Game Name.
        /// </summary>
        /// <value>the Game Name</value>
        [JsonProperty("name")] public string Name;


        /// <summary>
        ///     Gets the Game Cover URL.
        /// </summary>
        /// <value>the Game Cover URL</value>
        [JsonProperty("logo")] public string Image;
        
        
        /// <summary>
        ///     Gets the Game Publisher.
        /// </summary>
        /// <value>the Game Publisher</value>
        [JsonProperty("publisher")] public Publisher Publisher;
        
        
        /// <summary>
        ///     Gets the Game Online Players.
        /// </summary>
        /// <value>the Game Online Players</value>
        [JsonProperty("onlines")] public int OnlinePlayers;
    }
}