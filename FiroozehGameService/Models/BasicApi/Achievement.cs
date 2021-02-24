// <copyright file="Achievement.cs" company="Firoozeh Technology LTD">
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
namespace FiroozehGameService.Models.BasicApi
{
    /// <summary>
    ///     Represents Achievement Data Model In Game Service Basic API
    /// </summary>
    [Serializable]
    public class Achievement
    {
        /// <summary>
        ///     Gets the Achievement Description.
        /// </summary>
        /// <value>the Achievement Description</value>
        [JsonProperty("desc")] public string Description;


        /// <summary>
        ///     Gets the Game id.
        /// </summary>
        /// <value>the Game id</value>
        [JsonProperty("game")] public string Game;

        /// <summary>
        ///     Gets the Achievement Cover URL.
        /// </summary>
        /// <value>the Achievement Cover URL</value>
        [JsonProperty("image")] public string Image;


        /// <summary>
        ///     Gets the Status of Earned Achievement
        ///     if the status is True You Earned this Achievement Before
        /// </summary>
        /// <value>the Status of Earned Achievement</value>
        [JsonProperty("earned")] public bool IsUnlocked;


        /// <summary>
        ///     Gets the Achievement Key.
        ///     You Can Use It To Unlock Achievement
        /// </summary>
        /// <value>the Achievement Key</value>
        [JsonProperty("key")] public string Key;

        /// <summary>
        ///     Gets the Achievement Name.
        /// </summary>
        /// <value>the Achievement Name</value>
        [JsonProperty("name")] public string Name;


        /// <summary>
        ///     Gets the Achievement Point.
        /// </summary>
        /// <value>the Achievement Point</value>
        [JsonProperty("point")] public int Point;


        /// <summary>
        ///     Gets the Achievement status.
        ///     if the status is True You Can Unlock Achievement
        /// </summary>
        /// <value>the Achievement status</value>
        [JsonProperty("status")] public bool Status;


        public override string ToString()
        {
            return "Achievement{" +
                   "name='" + Name + '\'' +
                   ", key='" + Key + '\'' +
                   ", Description='" + Description + '\'' +
                   ", point=" + Point +
                   ", image='" + Image + '\'' +
                   ", status=" + Status +
                   ", game='" + Game + '\'' +
                   ", IsUnlocked=" + IsUnlocked +
                   '}';
        }
    }
}