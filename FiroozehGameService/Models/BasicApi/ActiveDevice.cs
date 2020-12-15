// <copyright file="ActiveDevice.cs" company="Firoozeh Technology LTD">
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
    ///     Represents ActiveDevice Data Model In Game Service Basic API
    /// </summary>
    [Serializable]
    public class ActiveDevice
    {
        /// <summary>
        ///     Gets the DeviceID
        /// </summary>
        /// <value>the DeviceID</value>
        [JsonProperty("DeviceID")] public string DeviceId;


        /// <summary>
        ///     Gets the Device Type
        /// </summary>
        /// <value>the Device Type</value>
        [JsonProperty("Device")] public string DeviceType;

        
        
        /// <summary>
        ///     Gets the First Login Time
        /// </summary>
        /// <value>the First Login Time</value>
        [JsonProperty("first_login")] public DateTimeOffset FirstLoginTime;
        
        
        /// <summary>
        ///     Gets the Last Login Time
        /// </summary>
        /// <value>the Last Login Time</value>
        [JsonProperty("last_login")] public DateTimeOffset LastLoginTime;



        public override string ToString()
        {
            return "LeaderBoard{" +
                   "DeviceID='" + DeviceId + '\'' +
                   ", DeviceType='" + DeviceType + '\'' +
                   ", FirstLoginTime=" + FirstLoginTime +
                   ", LastLoginTime='" + LastLoginTime + '\'' +
                    '}';
        }
    }
}