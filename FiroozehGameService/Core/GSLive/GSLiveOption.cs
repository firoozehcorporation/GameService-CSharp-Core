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
    ///     Represents GSLive GSLiveOption
    /// </summary>
    public class GSLiveOption
    {
        /// <summary>
        ///     Represents GSLive AutoMatchOption
        /// </summary>
        public class AutoMatchOption
        {
            /// <summary>
            ///     Specifies the AutoMatch Options
            /// </summary>
            /// <param name="role">(Not NULL)Specifies the Room Role</param>
            /// <param name="minPlayer">Specifies the Room min player limit (MIN=2)</param>
            /// <param name="maxPlayer">Specifies the Room max player limit (MAX=8)</param>
            /// <param name="isPersist">Specifies the Room Persistence</param>
            public AutoMatchOption(string role, int minPlayer = 2, int maxPlayer = 2, bool isPersist = false)
            {
                if (minPlayer < 2 || minPlayer > 8) throw new GameServiceException("Invalid MinPlayer Value");
                if (maxPlayer < 2 || maxPlayer > 8) throw new GameServiceException("Invalid MaxPlayer Value");
                if (maxPlayer < minPlayer) throw new GameServiceException("MaxPlayer Cant Smaller Than MinPlayer");
                if (string.IsNullOrEmpty(role)) throw new GameServiceException("Role Cant Be EmptyOrNull");

                MinPlayer = minPlayer;
                MaxPlayer = maxPlayer;
                Role = role;
                IsPersist = isPersist;
            }

            internal int MinPlayer { get; }
            internal int MaxPlayer { get; }
            internal string Role { get; }
            internal bool IsPersist { get; }
            internal GSLiveType GsLiveType { set; get; }
        }

        /// <inheritdoc />
        /// <summary>
        ///     Represents GSLive CreateRoomOption
        /// </summary>
        public class CreateRoomOption : AutoMatchOption
        {
            /// <summary>
            ///     Specifies the CreateRoom Options
            /// </summary>
            /// <param name="roomName">(Not NULL)Specifies the Room Name</param>
            /// <param name="role">(Not NULL)Specifies the Room Role</param>
            /// <param name="minPlayer">Specifies the Room min player limit (MIN=2)</param>
            /// <param name="maxPlayer">Specifies the Room max player limit (MAX=8)</param>
            /// <param name="isPrivate">Specifies the Room Privacy</param>
            /// <param name="isPersist">Specifies the Room Persistence</param>
            public CreateRoomOption(string roomName, string role, int minPlayer = 2, int maxPlayer = 2,
                bool isPrivate = false, bool isPersist = false)
                : base(role, minPlayer, maxPlayer, isPersist)
            {
                if (string.IsNullOrEmpty(roomName)) throw new GameServiceException("RoomName Cant Be EmptyOrNull");
                RoomName = roomName;
                IsPrivate = isPrivate;
            }

            internal string RoomName { get; }
            internal bool IsPrivate { get; }
        }
    }
}