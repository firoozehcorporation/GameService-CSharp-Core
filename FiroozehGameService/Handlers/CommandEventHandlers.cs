// <copyright file="CommandEventHandlers.cs" company="Firoozeh Technology LTD">
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
using FiroozehGameService.Models;
using FiroozehGameService.Models.Enums.GSLive.Command;
using FiroozehGameService.Models.GSLive;
using FiroozehGameService.Models.GSLive.Command;
using FiroozehGameService.Models.GSLive.Providers;


/**
* @author Alireza Ghodrati
*/


namespace FiroozehGameService.Handlers
{
    /// <inheritdoc />
    /// <summary>
    ///     Represents CommandEventHandlers In MultiPlayer System
    /// </summary>
    public class CommandEventHandlers : CoreEventHandlers
    {
        internal static EventHandler<string> CommandAuthorized;
        internal static EventHandler<GTcpConnection> CommandClientConnected;
        internal static EventHandler<GameServiceException> GsCommandClientError;
        internal static EventHandler CommandPing;
        internal static EventHandler<Packet> CommandMirror;

        internal static EventHandler GsCommandClientConnected;


        /// <summary>
        ///     Returns Available Rooms When Call The Following Functions :
        ///     <see cref="GsLiveRealTimeProvider.GetAvailableRooms" />
        ///     <see cref="GsLiveTurnBasedProvider.GetAvailableRooms" />
        /// </summary>
        public static EventHandler<List<Room>> AvailableRoomsReceived;


        /// <summary>
        ///     Returns New Auto Match Event When New User Added To List
        /// </summary>
        public static EventHandler<AutoMatchEvent> AutoMatchUpdated;


        /// <summary>
        ///     Returns Current AutoMatch Canceled Status
        ///     <see cref="GsLiveRealTimeProvider.CancelAutoMatch" />
        ///     <see cref="GsLiveTurnBasedProvider.CancelAutoMatch" />
        /// </summary>
        public static EventHandler<AutoMatchCancel> AutoMatchCanceled;


        /// <summary>
        ///     Returns Available Invite , When Call The Following Functions :
        ///     <see cref="GsLiveRealTimeProvider.GetInviteInbox" />
        ///     <see cref="GsLiveTurnBasedProvider.GetInviteInbox" />
        /// </summary>
        public static EventHandler<List<Invite>> InviteInboxReceived;


        /// <summary>
        ///     Calls When New Invite Received
        /// </summary>
        public static EventHandler<Invite> NewInviteReceived;


        /// <summary>
        ///     Returns List of Member , When Call The Following Functions :
        ///     <see cref="GsLiveRealTimeProvider.FindMember" />
        ///     <see cref="GsLiveTurnBasedProvider.FindMember" />
        /// </summary>
        public static EventHandler<List<Member>> FindMemberReceived;


        /// <summary>
        ///     Calls When Current Invitation Sent Successfully
        ///     <see cref="GsLiveRealTimeProvider.InviteUser" />
        ///     <see cref="GsLiveTurnBasedProvider.InviteUser" />
        /// </summary>
        public static EventHandler InvitationSent;


        /// <summary>
        ///     Calls When Current Push Event Received Successfully
        ///     <see
        ///         cref="GsLiveEventProvider.PushEventById(string,string,FiroozehGameService.Models.Enums.GSLive.PushEventBufferType)" />
        ///     <see
        ///         cref="GsLiveEventProvider.PushEventByTag(string,string,FiroozehGameService.Models.Enums.GSLive.PushEventBufferType)" />
        /// </summary>
        public static EventHandler<PushEvent> PushEventReceived;


        /// <summary>
        ///     Calls When Current Buffered Push Events Received Successfully
        ///     <see
        ///         cref="GsLiveEventProvider.GetBufferedPushEvents()" />
        /// </summary>
        public static EventHandler<List<PushEvent>> BufferedPushEventsReceived;


        /// <summary>
        ///     Returns Rooms info When Call The Following Functions :
        ///     <see cref="GsLiveRealTimeProvider.GetRoomsInfo" />
        ///     <see cref="GsLiveTurnBasedProvider.GetRoomsInfo" />
        /// </summary>
        public static EventHandler<RoomsInfo> RoomsInfoReceived;
    }
}