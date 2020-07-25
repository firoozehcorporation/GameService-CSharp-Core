// <copyright file="Message.cs" company="Firoozeh Technology LTD">
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

/**
* @author Alireza Ghodrati
*/


namespace FiroozehGameService.Models.GSLive.RT
{
    /// <summary>
    ///     Represents Event Data Model In GameService RealTime MultiPlayer System
    /// </summary>
    [Serializable]
    public class EventData
    {
        /// <summary>
        ///     Gets the Event Caller Buffer
        /// </summary>
        /// <value>the Event Caller Buffer</value>
        public byte[] Caller;
        
        
        
        /// <summary>
        ///     Gets the Event Data Buffer
        /// </summary>
        /// <value>the Event Data Buffer</value>
        public byte[] Data;
        
        
        
        /// <summary>
        /// the Receiver Id (Current Client Member ID)
        /// </summary>
        public string ReceiverId;
        
        
        
        /// <summary>
        /// the Sender Id (Owner Member ID)
        /// </summary>
        public string SenderId;
    }
}