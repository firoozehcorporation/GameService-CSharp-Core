// <copyright file="TableItemHelper.cs" company="Firoozeh Technology LTD">
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

namespace FiroozehGameService.Models.BasicApi.DBaaS
{
    /// <summary>
    ///     Represents TableItemHelper Data Model In Game Service Basic API
    /// </summary>
    [Serializable]
    public class TableItemHelper
    {
        /// <summary>
        ///     Gets the TableItem Object ID.
        /// </summary>
        /// <value>the TableItem Object ID</value>
        [JsonProperty("id")] public string Id;


        /// <summary>
        ///     Gets the TableItem Owner User ID.
        /// </summary>
        /// <value>the TableItem Owner ID</value>
        [JsonProperty("owner")] public string OwnerId;

        internal TableItemHelper()
        {
        }


        /// <summary>
        ///     To Prevent Serialize Id Property
        /// </summary>
        public bool ShouldSerializeId()
        {
            return false;
        }


        /// <summary>
        ///     To Prevent Serialize OwnerId Property
        /// </summary>
        public bool ShouldSerializeOwnerId()
        {
            return false;
        }
    }
}