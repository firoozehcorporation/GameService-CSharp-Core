// <copyright file="IGsLiveProvider.cs" company="Firoozeh Technology LTD">
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


using FiroozehGameService.Core.GSLive;
using FiroozehGameService.Models.Enums;

namespace FiroozehGameService.Models.GSLive.Providers
{
    /// <summary>
    ///     Represents Game Service Multiplayer Provider (GSLive)
    /// </summary>
    public abstract class GsLiveRealTimeProvider
    {
        /// <summary>
        ///     Create Room With Option Like : Name , Min , Max , Role , IsPrivate
        /// </summary>
        /// <param name="option">(NOTNULL)Create Room Option</param>
        public abstract void CreateRoom(GSLiveOption.CreateRoomOption option);


        /// <summary>
        ///     Create AutoMatch With Option Like :  Min , Max , Role
        /// </summary>
        /// <param name="option">(NOTNULL)AutoMatch Option</param>
        public abstract void AutoMatch(GSLiveOption.AutoMatchOption option);


        /// <summary>
        ///     Cancel Current AutoMatch
        /// </summary>
        public abstract void CancelAutoMatch();


        /// <summary>
        ///     Join In Room With RoomID
        /// </summary>
        /// <param name="roomId">(NOTNULL)Room's id You Want To Join</param>
        /// <param name="extra">Specifies the Extra Data To Send to Other Clients</param>
        /// <param name="password">(NULLABLE)Specifies the Password if Room is Private</param>
        public abstract void JoinRoom(string roomId, string extra = null, string password = null);


        /// <summary>
        ///     Leave The Current Room
        /// </summary>
        public abstract void LeaveRoom();


        /// <summary>
        ///     Get Available Rooms According To Room's Role
        /// </summary>
        /// <param name="role">(NOTNULL)Room's Role </param>
        public abstract void GetAvailableRooms(string role);


        /// <summary>
        ///     Send A Data To All Players in Room.
        /// </summary>
        /// <param name="data">(NOTNULL) Data To BroadCast </param>
        /// <param name="sendType">Send Type </param>
        public abstract void SendPublicMessage(byte[] data, GProtocolSendType sendType);


        /// <summary>
        ///     Send A Data To Specific Player in Room.
        /// </summary>
        /// <param name="receiverId">(NOTNULL) (Type : MemberID)Player's ID</param>
        /// <param name="data">(NOTNULL) Data for Send</param>
        public abstract void SendPrivateMessage(string receiverId, byte[] data);


        /// <summary>
        ///     Get Room Members Details
        /// </summary>
        public abstract void GetRoomMembersDetail();


        /// <summary>
        ///     Get Current Room Info
        /// </summary>
        public abstract void GetCurrentRoomInfo();


        /// <summary>
        ///     Get Your Invite Inbox
        /// </summary>
        public abstract void GetInviteInbox();


        /// <summary>
        ///     Invite a Specific Player To Specific Room
        /// </summary>
        /// <param name="roomId">(NOTNULL) (Type : RoomID)Room's ID</param>
        /// <param name="userId">(NOTNULL) (Type : UserID)User's ID</param>
        public abstract void InviteUser(string roomId, string userId);


        /// <summary>
        ///     Accept a Specific Invite With Invite ID
        ///     Note: After accepting the invitation, you will be automatically entered into the game room
        /// </summary>
        /// <param name="inviteId">(NOTNULL) (Type : InviteID) Invite's ID</param>
        /// <param name="extra">Specifies the Extra Data To Send to Other Clients</param>
        public abstract void AcceptInvite(string inviteId, string extra = null);


        /// <summary>
        ///     Find All Member With Specific Query
        /// </summary>
        /// <param name="query">(NOTNULL) Query </param>
        /// <param name="limit">(Max = 15) The Result Limits</param>
        public abstract void FindMember(string query, int limit = 10);


        /// <summary>
        ///     Get Current RoundTripTime(RTT)
        ///     NOTE : You Must Join To RealTime Servers To Get Valid Data, Otherwise Return -1
        /// </summary>
        public abstract int GetRoundTripTime();


        /// <summary>
        ///     Get Current PacketLost
        ///     NOTE : You Must Join To RealTime Servers To Get Valid Data, Otherwise Return -1
        /// </summary>
        public abstract long GetPacketLost();


        internal abstract void SendEvent(byte[] caller, byte[] data, GProtocolSendType sendType);

        internal abstract void SendObserver(byte[] caller, byte[] data);
    }
}