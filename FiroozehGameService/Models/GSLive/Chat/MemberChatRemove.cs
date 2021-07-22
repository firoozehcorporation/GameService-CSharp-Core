// <copyright file="MemberChatRemove.cs" company="Firoozeh Technology LTD">
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

using System;

namespace FiroozehGameService.Models.GSLive.Chat
{
    /// <summary>
    ///     Represents Member Chat Remove Data Model In GameService Chat System
    /// </summary>
    [Serializable]
    public class MemberChatRemove
    {
        /// <summary>
        ///     Gets the Channel Name That removed All Chats Chats Belong To Current Member id in it
        /// </summary>
        /// <value>the Channel Name</value>
        public string ChannelName;

        /// <summary>
        ///     Gets Member id That is Removed All Chats Belong To it
        /// </summary>
        /// <value>The Member id</value>
        public string MemberId;
    }
}