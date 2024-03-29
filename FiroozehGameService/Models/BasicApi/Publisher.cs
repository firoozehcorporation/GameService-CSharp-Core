﻿// <copyright file="Publisher.cs" company="Firoozeh Technology LTD">
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
    ///     Represents Publisher Data Model In Game Service Basic API
    /// </summary>
    [Serializable]
    public class Publisher
    {
        /// <summary>
        ///     Gets the Publisher Cover URL.
        /// </summary>
        /// <value>the Publisher Cover URL</value>
        [JsonProperty("logo")] public string Image;

        /// <summary>
        ///     Gets the Publisher Name.
        /// </summary>
        /// <value>the Publisher Name</value>
        [JsonProperty("name")] public string Name;


        internal Publisher()
        {
        }
    }
}