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
using FiroozehGameService.Core.GSLive;
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
    /// Represents TurnBasedEventHandlers In MultiPlayer System
    /// </summary>
    public class TurnBasedEventHandlers : CommandEventHandler
    {
        
        /// <summary>
        /// Calls When SomeOne Join To Current Room
        /// It Maybe Current Player or Another
        /// This Event Handler Called By Following Functions :
        /// <see cref="GSLiveTB.JoinRoom"/>
        /// <see cref="GSLiveTB.CreateRoom"/>
        /// <see cref="GSLiveTB.AutoMatch"/>
        /// </summary>
        public static EventHandler<JoinEvent> JoinedRoom;
        
        
        /// <summary>
        /// Calls When SomeOne Left the Current Room
        /// This Event Handler Called By Following Function :
        /// <see cref="GSLiveTB.LeaveRoom"/>
        /// </summary>
        public static EventHandler<Member> LeftRoom;
        
        
        /// <summary>
        /// Returns New Turn With Data From Another Players When Call The Following Function :
        /// <see cref="GSLiveTB.TakeTurn"/>
        /// </summary>
        public static EventHandler<Turn> TakeTurn;
        
        
        /// <summary>
        /// Returns New Turn From Another Players When Call The Following Function :
        /// <see cref="GSLiveTB.ChooseNext"/>
        /// </summary>
        public static EventHandler<Member> ChoosedNext;
        
        
        /// <summary>
        /// Calls When SomeOne Announced To Finish Game
        /// This Event Handler Called By Following Function :
        /// <see cref="GSLiveTB.Finish"/>
        /// </summary>
        public static EventHandler<Finish> Finished;
        
        
        /// <summary>
        /// Calls When The Game Is Finished
        /// This Event Handler Called By Following Function :
        /// <see cref="GSLiveTB.Complete"/>
        /// </summary>
        public static EventHandler<Complete> Completed;
        
        /// <summary>
        /// Returns Current Room Members Detail  When Call The Following Function :
        /// <see cref="GSLiveTB.GetRoomMembersDetail"/>
        /// </summary>
        public static EventHandler<List<Member>> RoomMembersDetailReceived;
        
        
        /// <summary>
        /// Returns Current Turn Member Received When Call The Following Function :
        /// <see cref="GSLiveTB.GetCurrentTurnMember"/>
        /// </summary>
        public static EventHandler<Member> CurrentTurnMemberReceived;
    }
}