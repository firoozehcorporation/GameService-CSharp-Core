// <copyright file="RealTimeEventHandlers.cs" company="Firoozeh Technology LTD">
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
using FiroozehGameService.Core.GSLive;
using FiroozehGameService.Models.Enums.GSLive;
using FiroozehGameService.Models.GSLive;
using FiroozehGameService.Models.GSLive.Command;
using FiroozehGameService.Models.GSLive.RT;

/**
* @author Alireza Ghodrati
*/


namespace FiroozehGameService.Handlers
{
    /// <inheritdoc />
    /// <summary>
    ///     Represents RealTimeEventHandlers In MultiPlayer System
    /// </summary>
    public class RealTimeEventHandlers : CommandEventHandler
    {
        /// <summary>
        ///     Calls When SomeOne Join To Current Room
        ///     It Maybe Current Player or Another
        ///     This Event Handler Called By Following Functions :
        ///     <see cref="GSLiveRT.JoinRoom" />
        ///     <see cref="GSLiveRT.CreateRoom" />
        ///     <see cref="GSLiveRT.AutoMatch" />
        /// </summary>
        public static EventHandler<JoinEvent> JoinedRoom;

        /// <summary>
        ///     Calls When Current Player Try To Reconnect to Server
        /// </summary>
        public static EventHandler<ReconnectStatus> Reconnected;

        
        /// <summary>
        ///     Calls When SomeOne Left the Current Room
        ///     This Event Handler Called By Following Function :
        ///     <see cref="GSLiveRT.LeaveRoom" />
        /// </summary>
        public static EventHandler<Member> LeftRoom;


        /// <summary>
        ///     Calls When SomeOne Send Message In Current Room
        ///     This Event Handler Called By Following Functions :
        ///     <see cref="GSLiveRT.SendPublicMessage" />
        ///     <see cref="GSLiveRT.SendPrivateMessage" />
        /// </summary>
        public static EventHandler<MessageReceiveEvent> NewMessageReceived;


        /// <summary>
        ///     Returns Current Room Members Detail  When Call The Following Function :
        ///     <see cref="GSLiveRT.GetRoomMembersDetail" />
        /// </summary>
        public static EventHandler<List<Member>> RoomMembersDetailReceived;
        
        
        /// <summary>
        ///     Returns Room Info When Call The Following Function :
        ///     <see cref="GSLiveRT.GetCurrentRoomInfo" />
        /// </summary>
        public static EventHandler<RoomData> CurrentRoomInfoReceived;
    }
}