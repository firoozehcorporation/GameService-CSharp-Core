// <copyright file="Notification.cs" company="Firoozeh Technology LTD">
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
using FiroozehGameService.Models.Enums.Command;
using Newtonsoft.Json;


/**
* @author Alireza Ghodrati
*/


namespace FiroozehGameService.Models.Command
{
    /// <summary>
    /// Represents Notification Model In Game Service Command
    /// </summary>
    [Serializable]
    public class Notification
    {
        /// <summary>
        /// Gets the Notification Title
        /// </summary>
        /// <value>the Notification Title</value>
        [JsonProperty("1")]
        public string Title { get; internal set; }

        
        /// <summary>
        /// Gets the Notification Description
        /// </summary>
        /// <value>the Notification Description</value>
        [JsonProperty("2")]
        public string Description { get; internal set; }

        /// <summary>
        /// Gets the Notification Action Type
        /// </summary>
        /// <value>the Notification Action Type</value>
        [JsonProperty("4")]
        public TapActionType TapActionType = TapActionType.CloseNotification;

        
        /// <summary>
        /// Gets the Notification Action Data
        /// </summary>
        /// <value>the Notification Action Data</value>
        [JsonProperty("5")]
        public NotificationActionData ActionData { get; internal set; }
        
        
        /// <summary>
        /// Gets the Notification Json Data
        /// </summary>
        /// <value>the Notification Json Data</value>
        [JsonProperty("6")]
        public string JsonData { get; internal set; }

        
        public override string ToString()
        {
            return "Title : " + Title +
                   ", Description : " + Description +
                   ", TapActionType : " + TapActionType +
                   ", ActionData : " + ActionData +
                   ", JsonData : " + JsonData;
        }
    }
}