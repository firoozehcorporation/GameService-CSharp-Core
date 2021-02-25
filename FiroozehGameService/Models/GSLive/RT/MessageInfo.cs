// <copyright file="MessageInfo.cs" company="Firoozeh Technology LTD">
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


using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.Enums.GSLive.RT;

/**
* @author Alireza Ghodrati
*/


namespace FiroozehGameService.Models.GSLive.RT
{
    /// <summary>
    ///     Represents MessageInfo When Message Received in GSLiveRealTime
    /// </summary>
    public class MessageInfo
    {
        /// <summary>
        ///     Gets the Type of Received Message
        /// </summary>
        /// <value>Type of Received Message</value>
        public MessageType MessageType { get; internal set; }


        /// <summary>
        ///     Gets the Type of Protocol Send Message
        ///     if is Reliable , the Packet Loss is Minimized.
        /// </summary>
        /// <value>the Type of Protocol Send Message </value>
        public GProtocolSendType SendType { get; internal set; }


        /// <summary>
        ///     Gets Server RTT Time
        ///     you can use it to calculate Ping Time
        /// </summary>
        /// <value>Server RTT Time</value>
        public short RoundTripTime { get; internal set; }


        /// <summary>
        ///     Gets Client Packet Receive Time in Unix
        /// </summary>
        /// <value>Client Packet Receive Time</value>
        public long ClientReceiveTime { get; internal set; }
    }
}