// <copyright file="MessageReceiveEvent.cs" company="Firoozeh Technology LTD">
// Copyright (C) 2021 Firoozeh Technology LTD. All Rights Reserved.
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

using FiroozehGameService.Models.Enums.GSLive;

namespace FiroozehGameService.Models.GSLive.TB
{
    /// <summary>
    ///     Represents MessageReceiveEvent When Message Received in GSLiveTurnBased
    /// </summary>
    public class MessageReceiveEvent
    {
        /// <summary>
        ///     Gets the Type of Received Message
        /// </summary>
        /// <value>Type of Received Message</value>
        public MessageType MessageType { get; internal set; }

        /// <summary>
        ///     Gets the Message Data
        /// </summary>
        /// <value>the Message Data</value>
        public string Data { get; internal set; }

        /// <summary>
        ///     Gets the Message Sender Member Id
        /// </summary>
        /// <value>Message Sender Member Id</value>
        public string SenderMemberId { get; internal set; }

        /// <summary>
        ///     Gets the Message Receiver Member Id
        /// </summary>
        /// <value>Message Receiver Member Id</value>
        public string ReceiverMemberId { get; internal set; }
    }
}