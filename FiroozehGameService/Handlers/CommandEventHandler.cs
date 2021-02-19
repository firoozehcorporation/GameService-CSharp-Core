// <copyright file="CommandEventHandler.cs" company="Firoozeh Technology LTD">
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


using System;
using System.Collections.Generic;
using FiroozehGameService.Core.Providers.GSLive;
using FiroozehGameService.Models.Enums.GSLive.Command;
using FiroozehGameService.Models.GSLive;
using FiroozehGameService.Models.GSLive.Command;


/**
* @author Alireza Ghodrati
*/


namespace FiroozehGameService.Handlers
{
    /// <inheritdoc />
    /// <summary>
    ///     Represents CommandEventHandler In MultiPlayer System
    /// </summary>
    public class CommandEventHandler : CoreEventHandlers
    {
        /// <summary>
        ///     Returns Available Rooms When Call The Following Functions :
        ///     <see cref="GsLiveRealTime.GetAvailableRooms" />
        ///     <see cref="GsLiveTurnBased.GetAvailableRooms" />
        /// </summary>
        public static EventHandler<List<Room>> AvailableRoomsReceived;


        /// <summary>
        ///     Returns New Auto Match Event When New User Added To List
        /// </summary>
        public static EventHandler<AutoMatchEvent> AutoMatchUpdated;


        /// <summary>
        ///     Returns Current AutoMatch Canceled Status
        ///     <see cref="GsLiveRealTime.CancelAutoMatch" />
        ///     <see cref="GsLiveTurnBased.CancelAutoMatch" />
        /// </summary>
        public static EventHandler<AutoMatchCancel> AutoMatchCanceled;


        /// <summary>
        ///     Returns Available Invite , When Call The Following Functions :
        ///     <see cref="GsLiveRealTime.GetInviteInbox" />
        ///     <see cref="GsLiveTurnBased.GetInviteInbox" />
        /// </summary>
        public static EventHandler<List<Invite>> InviteInboxReceived;


        /// <summary>
        ///     Calls When New Invite Received
        /// </summary>
        public static EventHandler<Invite> NewInviteReceived;


        /// <summary>
        ///     Returns List of Member , When Call The Following Functions :
        ///     <see cref="GsLiveRealTime.FindMember" />
        ///     <see cref="GsLiveTurnBased.FindMember" />
        /// </summary>
        public static EventHandler<List<Member>> FindMemberReceived;


        /// <summary>
        ///     Calls When Current Invitation Sent Successfully
        ///     <see cref="GsLiveRealTime.InviteUser" />
        ///     <see cref="GsLiveTurnBased.InviteUser" />
        /// </summary>
        public static EventHandler InvitationSent;
    }
}