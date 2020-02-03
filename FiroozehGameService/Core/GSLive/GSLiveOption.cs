// <copyright file="GSLiveOption.cs" company="Firoozeh Technology LTD">
// Copyright (C) 2019 Firoozeh Technology LTD. All Rights Reserved.
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

using FiroozehGameService.Models;
using FiroozehGameService.Models.Enums.GSLive;
namespace FiroozehGameService.Core.GSLive
{
    /// <summary>
    /// Represents GSLive GSLiveOption
    /// </summary>
    public class GSLiveOption
    {
        /// <summary>
        /// Represents GSLive AutoMatchOption
        /// </summary>
        public class AutoMatchOption
        {
            internal int MinPlayer { get; }
            internal int MaxPlayer { get; }
            internal string Role { get; }
            internal bool IsPersist { get; }
            internal GSLiveType GsLiveType { set; get; }

            public AutoMatchOption(string role , int minPlayer, int maxPlayer, bool isPersist = false)
            {
                MinPlayer = minPlayer < 2 || minPlayer > 8 ? throw new GameServiceException("Invalid MinPlayer Value") : MinPlayer = minPlayer;
                MaxPlayer = maxPlayer < 2 || maxPlayer > 8 ? throw new GameServiceException("Invalid MaxPlayer Value") : MinPlayer = minPlayer;
                Role =  string.IsNullOrEmpty(role) ? throw new GameServiceException("Role Cant Be EmptyOrNull") : Role = role;
                IsPersist = isPersist;
                
                if (maxPlayer < minPlayer) throw new GameServiceException("MaxPlayer Cant Smaller Than MinPlayer");
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Represents GSLive CreateRoomOption
        /// </summary>
        public class CreateRoomOption : AutoMatchOption
        {
            internal string RoomName { get; }   
            internal bool IsPrivate { get; }

            public CreateRoomOption(string roomName,string role, int minPlayer, int maxPlayer , bool isPrivate = false ,bool isPersist = false)
                : base(role, minPlayer, maxPlayer, isPersist)
            {
                RoomName =  string.IsNullOrEmpty(roomName) ? throw new GameServiceException("RoomName Cant Be EmptyOrNull") : RoomName = roomName;
                IsPrivate = isPrivate;
            }
        }


    }
}