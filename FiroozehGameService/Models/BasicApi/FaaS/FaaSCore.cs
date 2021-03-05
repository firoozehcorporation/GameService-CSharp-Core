// <copyright file="FaaSCore.cs" company="Firoozeh Technology LTD">
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

namespace FiroozehGameService.Models.BasicApi.FaaS
{
    /// <summary>
    ///     Represents FaaSCore Data Model In Game Service Basic API
    /// </summary>
    [Serializable]
    public class FaaSCore
    {
        /// <summary>
        ///     Gets the Function ID.
        /// </summary>
        /// <value>the Function ID</value>
        [JsonProperty("function_id")]
        public string FunctionId { get; internal set; }
    }
}