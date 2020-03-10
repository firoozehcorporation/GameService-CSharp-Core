// <copyright file="NotificationActionData.cs" company="Firoozeh Technology LTD">
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
using FiroozehGameService.Models.Enums.GSLive.Command;
using Newtonsoft.Json;

/**
* @author Alireza Ghodrati
*/


namespace FiroozehGameService.Models.GSLive.Command
{
    /// <summary>
    /// Represents NotificationActionData Model In Game Service Command
    /// </summary>
    [Serializable]
    public class NotificationActionData
    {
        /// <summary>
        /// Gets the PackageName Data
        /// this Data may be Null if Not Set in Developer Panel
        /// </summary>
        /// <value>the PackageName Data</value>
        [JsonProperty("1")]
        public string PackageName { get; internal set; }
        
        
        /// <summary>
        /// Gets the MarketType Data
        /// this Data may be Null if Not Set in Developer Panel
        /// </summary>
        /// <value>the MarketType Data</value>
        [JsonProperty("2")]
        public MarketType MarketType { get; internal set; }
        
        
        /// <summary>
        /// Gets the LinkAddress Data
        /// this Data may be Null if Not Set in Developer Panel
        /// </summary>
        /// <value>the LinkAddress Data</value>
        [JsonProperty("3")]
        public string LinkAddress { get; internal set; }
        
        
        /// <summary>
        /// Gets the EmailAddress Data
        /// this Data may be Null if Not Set in Developer Panel
        /// </summary>
        /// <value>the EmailAddress Data</value>
        [JsonProperty("4")]
        public string EmailAddress { get; internal set; }
        
        
        /// <summary>
        /// Gets the EmailTitle Data
        /// this Data may be Null if Not Set in Developer Panel
        /// </summary>
        /// <value>the EmailTitle Data</value>
        [JsonProperty("5")]
        public string EmailTitle { get; internal set; }
        
        
        /// <summary>
        /// Gets the EmailBody Data
        /// this Data may be Null if Not Set in Developer Panel
        /// </summary>
        /// <value>the EmailBody Data</value>
        [JsonProperty("6")]
        public string EmailBody { get; internal set; }
        
        /// <summary>
        /// Gets the TelegramChannel Data
        /// this Data may be Null if Not Set in Developer Panel
        /// </summary>
        /// <value>the TelegramChannel Data</value>
        [JsonProperty("7")]
        public string TelegramChannel { get; internal set; }
        
        /// <summary>
        /// Gets the IntentAction Data
        /// this Data may be Null if Not Set in Developer Panel
        /// </summary>
        /// <value>the IntentAction Data</value>
        [JsonProperty("8")]
        public string IntentAction { get; internal set; }

        public override string ToString()
        {
            return "PackageName : " + PackageName +
                   ", MarketType : " + MarketType +
                   ", LinkAddress : " + LinkAddress +
                   ", EmailAddress : " + EmailAddress +
                   ", EmailTitle : " + EmailTitle +
                   ", EmailBody :" + EmailBody +
                   ", TelegramChannel : " + TelegramChannel +
                   ", IntentAction : " + IntentAction;
        }
    }
}