// <copyright file="PushEventType" company="Firoozeh Technology LTD">
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


namespace FiroozehGameService.Models.Enums
{
    /// <summary>
    ///     Represents EventType Class in GameService
    /// </summary>
    public enum EventAction
    {
        /// <summary>
        ///     Accept Friend Request Action
        /// </summary>
        AcceptFriendRequest = 1,

        /// <summary>
        ///     Accept Friend Request Action
        /// </summary>
        AcceptJoinPartyRequest = 2,

        /// <summary>
        ///     kick Member Action
        /// </summary>
        KickPartyMember = 3,

        /// <summary>
        ///     Change Member Role Action
        /// </summary>
        ChangePartyMemberRole = 4,

        /// <summary>
        ///     Add To Party By Friend Action
        /// </summary>
        AddToPartyByFriend = 5
    }
}