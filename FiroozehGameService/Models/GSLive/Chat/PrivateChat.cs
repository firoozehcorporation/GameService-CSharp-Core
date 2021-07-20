// <copyright file="PrivateChat.cs" company="Firoozeh Technology LTD">
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
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.GSLive.Chat
{
    /// <summary>
    ///     Represents Private Chat Data Model In GameService Chat System
    /// </summary>
    [Serializable]
    public class PrivateChat
    {
        /// <summary>
        ///     Gets the Chats From Current Member Contact
        /// </summary>
        /// <value>the Chats From Current Member Contact</value>
        [JsonProperty("1")] public List<Chat> Chats;

        /// <summary>
        ///     Gets the Chats Member Contact
        /// </summary>
        /// <value>the the Chats Member Contact</value>
        [JsonProperty("0")] public Member MemberContact;
    }
}