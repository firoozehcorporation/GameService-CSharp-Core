// <copyright file="TurnBasedEventHandlers.cs" company="Firoozeh Technology LTD">
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
using FiroozehGameService.Models;
using FiroozehGameService.Models.Enums.GSLive;
using FiroozehGameService.Models.GSLive;
using FiroozehGameService.Models.GSLive.Command;
using FiroozehGameService.Models.GSLive.TB;

/**
* @author Alireza Ghodrati
*/


namespace FiroozehGameService.Handlers
{
    /// <inheritdoc />
    /// <summary>
    ///     Represents TurnBasedEventHandlers In MultiPlayer System
    /// </summary>
    public class TurnBasedEventHandlers : CommandEventHandlers
    {
        internal static EventHandler<string> TurnBasedAuthorized;
        internal static EventHandler<GTcpConnection> TurnBasedClientConnected;
        internal static EventHandler<GameServiceException> GsTurnBasedClientError;
        internal static EventHandler<Packet> TurnBasedMirror;

        internal static EventHandler TurnBasedPing;

        internal static EventHandler LeftDispose;
        internal static EventHandler GsTurnBasedClientConnected;


        /// <summary>
        ///     Calls When SomeOne Join To Current Room
        ///     It Maybe Current Player or Another
        ///     This Event Handler Called By Following Functions :
        ///     <see cref="GsLiveTurnBased.JoinRoom" />
        ///     <see cref="GsLiveTurnBased.CreateRoom" />
        ///     <see cref="GsLiveTurnBased.AutoMatch" />
        /// </summary>
        public static EventHandler<JoinEvent> JoinedRoom;


        /// <summary>
        ///     Calls When Current Player Reconnect to Server
        /// </summary>
        public static EventHandler<ReconnectStatus> Reconnected;


        /// <summary>
        ///     Calls When SomeOne Left the Current Room
        ///     This Event Handler Called By Following Function :
        ///     <see cref="GsLiveTurnBased.LeaveRoom" />
        /// </summary>
        public static EventHandler<Member> LeftRoom;


        /// <summary>
        ///     Returns New Turn With Data From Another Players When Call The Following Function :
        ///     <see cref="GsLiveTurnBased.TakeTurn" />
        /// </summary>
        public static EventHandler<Turn> TakeTurn;


        /// <summary>
        ///     Returns New Turn From Another Players When Call The Following Function :
        ///     <see cref="GsLiveTurnBased.ChooseNext" />
        /// </summary>
        public static EventHandler<Member> ChoosedNext;


        /// <summary>
        ///     Returns NewOrUpdate Property From Another Players When Call The Following Functions :
        ///     <see cref="GsLiveTurnBased.SetOrUpdateProperty" />
        ///     <see cref="GsLiveTurnBased.RemoveProperty" />
        /// </summary>
        public static EventHandler<PropertyPayload> PropertyUpdated;


        /// <summary>
        ///     Returns Properties From Another Players When Call The Following Function :
        ///     <see cref="GsLiveTurnBased.GetMemberProperties" />
        /// </summary>
        public static EventHandler<List<PropertyData>> MemberPropertiesReceived;


        /// <summary>
        ///     Returns Room Info When Call The Following Function :
        ///     <see cref="GsLiveTurnBased.GetCurrentRoomInfo" />
        /// </summary>
        public static EventHandler<RoomData> CurrentRoomInfoReceived;


        /// <summary>
        ///     Calls When SomeOne Announced To Finish Game
        ///     This Event Handler Called By Following Function :
        ///     <see cref="GsLiveTurnBased.Vote" />
        /// </summary>
        public static EventHandler<Vote> VoteReceived;


        /// <summary>
        ///     Calls When Accept Vote Received From Other Players
        ///     This Event Handler Called By Following Function :
        ///     <see cref="GsLiveTurnBased.AcceptVote" />
        /// </summary>
        public static EventHandler<AcceptVote> AcceptVoteReceived;

        /// <summary>
        ///     Returns Current Room Members Detail  When Call The Following Function :
        ///     <see cref="GsLiveTurnBased.GetRoomMembersDetail" />
        /// </summary>
        public static EventHandler<List<Member>> RoomMembersDetailReceived;


        /// <summary>
        ///     Returns Current Turn Member Received When Call The Following Function :
        ///     <see cref="GsLiveTurnBased.GetCurrentTurnMember" />
        /// </summary>
        public static EventHandler<Member> CurrentTurnMemberReceived;
    }
}