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
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.Enums.GSLive;
using FiroozehGameService.Utils;

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
            internal AutoMatchOption()
            {
            }

            /// <summary>
            ///     Specifies the AutoMatch Options
            /// </summary>
            /// <param name="role">(Not NULL)Specifies the Room Role</param>
            /// <param name="minPlayer">Specifies the Room min player limit (MIN=2)</param>
            /// <param name="maxPlayer">Specifies the Room max player limit</param>
            /// <param name="isPersist">Specifies the Room Persistence</param>
            /// <param name="extra">Specifies the Extra Data To Send to Other Clients</param>
            public AutoMatchOption(string role, int minPlayer = 2, int maxPlayer = 2, bool isPersist = false,
                string extra = null)
            {
                if (maxPlayer < minPlayer)
                    throw new GameServiceException("MaxPlayer Cant Smaller Than MinPlayer")
                        .LogException<AutoMatchOption>(DebugLocation.Internal, "Constructor");
                if (string.IsNullOrEmpty(role))
                    throw new GameServiceException("Role Cant Be EmptyOrNull").LogException<AutoMatchOption>(
                        DebugLocation.Internal, "Constructor");

                MinPlayer = minPlayer;
                MaxPlayer = maxPlayer;
                Role = role;
                Extra = extra;
                IsPersist = isPersist;
            }

            internal int MinPlayer { get; }
            internal int MaxPlayer { get; }
            internal string Role { get; }
            internal string Extra { get; }
            internal bool IsPersist { get; }
            internal GSLiveType GsLiveType { set; get; }
        }

        /// <inheritdoc />
        /// <summary>
        ///     Represents GSLive CreateRoomOption
        /// </summary>
        public class CreateRoomOption : AutoMatchOption
        {
            internal CreateRoomOption()
            {
            }

            /// <summary>
            ///     Specifies the CreateRoom Options
            /// </summary>
            /// <param name="roomName">(Not NULL)Specifies the Room Name</param>
            /// <param name="role">(Not NULL)Specifies the Room Role</param>
            /// <param name="minPlayer">Specifies the Room min player limit (MIN=2)</param>
            /// <param name="maxPlayer">Specifies the Room max player limit</param>
            /// <param name="isPrivate">Specifies the Room Privacy</param>
            /// <param name="isPersist">Specifies the Room Persistence</param>
            /// <param name="extra">Specifies the Extra Data To Send to Other Clients</param>
            /// <param name="roomPassword">Specifies the Room Password If the Room is Private</param>
            public CreateRoomOption(string roomName, string role, int minPlayer = 2, int maxPlayer = 2,
                bool isPrivate = false, bool isPersist = false, string extra = null, string roomPassword = null)
                : base(role, minPlayer, maxPlayer, isPersist, extra)
            {
                if (string.IsNullOrEmpty(roomName))
                    throw new GameServiceException("RoomName Cant Be EmptyOrNull").LogException<CreateRoomOption>(
                        DebugLocation.Internal, "Constructor");
                if (!isPrivate && !string.IsNullOrEmpty(roomPassword))
                    throw new GameServiceException("RoomPassword Can Only Set When Room is Private")
                        .LogException<CreateRoomOption>(DebugLocation.Internal, "Constructor");
                if (isPrivate && string.IsNullOrEmpty(roomPassword))
                    throw new GameServiceException("RoomPassword Cant Be EmptyOrNull When Room is Private")
                        .LogException<CreateRoomOption>(DebugLocation.Internal, "Constructor");
                if (isPrivate && (roomPassword.Length < 4 || roomPassword.Length > 15))
                    throw new GameServiceException("RoomPassword Length Must Be Between 4 and 15")
                        .LogException<CreateRoomOption>(DebugLocation.Internal, "Constructor");

                RoomName = roomName;
                RoomPassword = roomPassword;
                IsPrivate = isPrivate;
            }

            internal string RoomName { get; }

            internal string RoomPassword { get; }

            internal bool IsPrivate { get; }
        }
    }
}