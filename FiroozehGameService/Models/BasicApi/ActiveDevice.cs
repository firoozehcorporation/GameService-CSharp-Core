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
        ///     Gets the Device Model
        /// </summary>
        /// <value>the Device Model</value>
        [JsonProperty("DeviceModel")] public string DeviceModel;


        /// <summary>
        ///     Gets the Device Name
        /// </summary>
        /// <value>the Device Name</value>
        [JsonProperty("DeviceName")] public string DeviceName;
        
        
        /// <summary>
        ///     Gets the Device Graphics Name
        /// </summary>
        /// <value>the Device Graphics Name</value>
        [JsonProperty("GraphicsDeviceName")] public string GraphicsDeviceName;



        /// <summary>
        ///     Gets the Device Operating System
        /// </summary>
        /// <value>the Device Operating System</value>
        [JsonProperty("OperatingSystem")] public string OperatingSystem;
        
        
        /// <summary>
        ///     returns true if the Active Device For Current Player
        /// </summary>
        /// <value>returns true if the Active Device For Current Player</value>
        [JsonProperty("is_current")] public bool IsCurrentDevice;


        /// <summary>
        ///     Gets the Last Login Time
        /// </summary>
        /// <value>the Last Login Time</value>
        [JsonProperty("last_login")] public DateTimeOffset LastLoginTime;
        
        
        /// <summary>
        ///     Gets the First Login Time
        /// </summary>
        /// <value>the First Login Time</value>
        [JsonProperty("first_login")] public DateTimeOffset FirstLoginTime;
        


        public override string ToString()
        {
            return "ActiveDevice{" +
                   "DeviceID='" + DeviceId + '\'' +
                   ", DeviceName='" + DeviceName + '\'' +
                   ", DeviceModel='" + DeviceModel + '\'' +
                   ", OperatingSystem='" + OperatingSystem + '\'' +
                   ", GraphicsDeviceName='" + GraphicsDeviceName + '\'' +
                   ", FirstLoginTime=" + FirstLoginTime +
                   ", LastLoginTime='" + LastLoginTime + '\'' +
                   '}';
        }
    }
}