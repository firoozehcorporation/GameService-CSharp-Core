// <copyright file="GsLiveRealTime.cs" company="Firoozeh Technology LTD">
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


using System;
using System.Text;
using System.Threading.Tasks;
using FiroozehGameService.Core.GSLive;
using FiroozehGameService.Handlers.Command.RequestHandlers;
using FiroozehGameService.Handlers.RealTime.RequestHandlers;
using FiroozehGameService.Models;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.Enums.GSLive;
using FiroozehGameService.Models.GSLive.Command;
using FiroozehGameService.Models.GSLive.Providers;
using FiroozehGameService.Models.GSLive.RT;
using FiroozehGameService.Utils;

namespace FiroozehGameService.Core.Providers.GSLive
{
    internal class GsLiveRealTime : GsLiveRealTimeProvider
    {
        public override async Task CreateRoom(GSLiveOption.CreateRoomOption option)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "CreateRoom");
            if (option == null)
                throw new GameServiceException("option Cant Be Null").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "CreateRoom");
            option.GsLiveType = GSLiveType.RealTime;
            await GameService.GSLive.GetGsHandler().CommandHandler.RequestAsync(CreateRoomHandler.Signature, option);
        }

        public override async Task AutoMatch(GSLiveOption.AutoMatchOption option)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "AutoMatch");
            if (option == null)
                throw new GameServiceException("option Cant Be Null").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "AutoMatch");
            option.GsLiveType = GSLiveType.RealTime;
            await GameService.GSLive.GetGsHandler().CommandHandler.RequestAsync(AutoMatchHandler.Signature, option);
        }

        public override async Task CancelAutoMatch()
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "CancelAutoMatch");
            await GameService.GSLive.GetGsHandler().CommandHandler.RequestAsync(CancelAutoMatchHandler.Signature);
        }


        public override async Task JoinRoom(string roomId, string extra = null, string password = null)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "JoinRoom");
            if (string.IsNullOrEmpty(roomId))
                throw new GameServiceException("roomId Cant Be EmptyOrNull").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "JoinRoom");
            await GameService.GSLive.GetGsHandler().CommandHandler.RequestAsync(JoinRoomHandler.Signature,
                new RoomDetail {Id = roomId, Extra = extra, RoomPassword = password});
        }

        public override void LeaveRoom()
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "LeaveRoom");
            if (GameService.GSLive.GetGsHandler().RealTimeHandler == null)
                throw new GameServiceException("You Must Create or Join Room First").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "LeaveRoom");

            GameService.GSLive.GetGsHandler().RealTimeHandler?.Request(LeaveRoomHandler.Signature,
                GProtocolSendType.Reliable,
                isCritical: true);
            GameService.GSLive.GetGsHandler().RealTimeHandler?.Dispose();
        }


        public override async Task GetAvailableRooms(string role)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "GetAvailableRooms");
            if (string.IsNullOrEmpty(role))
                throw new GameServiceException("role Cant Be EmptyOrNull").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "GetAvailableRooms");

            await GameService.GSLive.GetGsHandler().CommandHandler.RequestAsync(GetRoomsHandler.Signature,
                new RoomDetail {Role = role, GsLiveType = (int) GSLiveType.RealTime});
        }


        [Obsolete("This Method is Deprecated,Use SendPublicMessage(byte[] data, GProtocolSendType sendType) Instead")]
        public override void SendPublicMessage(string data, GProtocolSendType sendType)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "SendPublicMessage");
            if (string.IsNullOrEmpty(data))
                throw new GameServiceException("data Cant Be EmptyOrNull").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "SendPublicMessage");
            if (GameService.GSLive.GetGsHandler().RealTimeHandler == null)
                throw new GameServiceException("You Must Create or Join Room First").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "SendPublicMessage");

            GameService.GSLive.GetGsHandler().RealTimeHandler.Request(SendPublicMessageHandler.Signature, sendType,
                new DataPayload {Payload = Encoding.UTF8.GetBytes(data)});
        }


        [Obsolete("This Method is Deprecated,Use SendPrivateMessage(string receiverId, byte[] data) Instead")]
        public override void SendPrivateMessage(string receiverId, string data)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "SendPrivateMessage");
            if (string.IsNullOrEmpty(receiverId) && string.IsNullOrEmpty(data))
                throw new GameServiceException("data Or receiverId Cant Be EmptyOrNull").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "SendPrivateMessage");
            if (GameService.GSLive.GetGsHandler().RealTimeHandler == null)
                throw new GameServiceException("You Must Create or Join Room First").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "SendPrivateMessage");

            GameService.GSLive.GetGsHandler().RealTimeHandler.Request(SendPrivateMessageHandler.Signature,
                GProtocolSendType.Reliable,
                new DataPayload {ReceiverId = receiverId, Payload = Encoding.UTF8.GetBytes(data)});
        }


        public override void SendPublicMessage(byte[] data, GProtocolSendType sendType)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "SendPublicMessage");
            if (data == null)
                throw new GameServiceException("data Cant Be Null").LogException<GsLiveRealTime>(DebugLocation.RealTime,
                    "SendPublicMessage");
            if (GameService.GSLive.GetGsHandler().RealTimeHandler == null)
                throw new GameServiceException("You Must Create or Join Room First").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "SendPublicMessage");

            GameService.GSLive.GetGsHandler().RealTimeHandler.Request(SendPublicMessageHandler.Signature, sendType,
                new DataPayload {Payload = data});
        }


        public override void SendPrivateMessage(string receiverId, byte[] data)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "SendPrivateMessage");
            if (string.IsNullOrEmpty(receiverId) && data == null)
                throw new GameServiceException("data Or receiverId Cant Be EmptyOrNull").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "SendPrivateMessage");
            if (GameService.GSLive.GetGsHandler().RealTimeHandler == null)
                throw new GameServiceException("You Must Create or Join Room First").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "SendPrivateMessage");

            GameService.GSLive.GetGsHandler().RealTimeHandler.Request(SendPrivateMessageHandler.Signature,
                GProtocolSendType.Reliable,
                new DataPayload {ReceiverId = receiverId, Payload = data});
        }

        public override void GetRoomMembersDetail()
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "GetRoomMembersDetail");
            if (GameService.GSLive.GetGsHandler().RealTimeHandler == null)
                throw new GameServiceException("You Must Create or Join Room First").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "GetRoomMembersDetail");

            GameService.GSLive.GetGsHandler().RealTimeHandler.Request(GetMemberHandler.Signature,
                GProtocolSendType.Reliable,
                isCritical: true);
        }


        /// <summary>
        ///     Get Current Room Info
        /// </summary>
        public override void GetCurrentRoomInfo()
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveTurnBased>(
                    DebugLocation.RealTime, "GetCurrentRoomInfo");
            if (GameService.GSLive.GetGsHandler().RealTimeHandler == null)
                throw new GameServiceException("You Must Create or Join Room First").LogException<GsLiveTurnBased>(
                    DebugLocation.RealTime, "GetCurrentRoomInfo");

            GameService.GSLive.GetGsHandler().RealTimeHandler
                .Request(RoomInfoHandler.Signature, GProtocolSendType.Reliable);
        }


        /// <summary>
        ///     Get Your Invite Inbox
        /// </summary>
        public override async Task GetInviteInbox()
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "GetInviteInbox");

            await GameService.GSLive.GetGsHandler().CommandHandler.RequestAsync(InviteListHandler.Signature,
                new RoomDetail {GsLiveType = (int) GSLiveType.RealTime});
        }


        /// <summary>
        ///     Invite a Specific Player To Specific Room
        /// </summary>
        /// <param name="roomId">(NOTNULL) (Type : RoomID)Room's ID</param>
        /// <param name="userId">(NOTNULL) (Type : UserID)User's ID</param>
        public override async Task InviteUser(string roomId, string userId)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "InviteUser");
            if (string.IsNullOrEmpty(roomId) && string.IsNullOrEmpty(userId))
                throw new GameServiceException("roomId Or userId Cant Be EmptyOrNull").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "InviteUser");

            await GameService.GSLive.GetGsHandler().CommandHandler.RequestAsync(InviteUserHandler.Signature,
                new RoomDetail {UserOrMemberId = userId, Id = roomId, GsLiveType = (int) GSLiveType.RealTime});
        }


        /// <summary>
        ///     Accept a Specific Invite With Invite ID
        ///     Note: After accepting the invitation, you will be automatically entered into the game room
        /// </summary>
        /// <param name="inviteId">(NOTNULL) (Type : InviteID) Invite's ID</param>
        /// <param name="extra">Specifies the Extra Data To Send to Other Clients</param>
        public override async Task AcceptInvite(string inviteId, string extra = null)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "AcceptInvite");
            if (string.IsNullOrEmpty(inviteId))
                throw new GameServiceException("inviteId Cant Be EmptyOrNull").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "AcceptInvite");

            await GameService.GSLive.GetGsHandler().CommandHandler.RequestAsync(AcceptInviteHandler.Signature,
                new RoomDetail {Invite = inviteId, Extra = extra, GsLiveType = (int) GSLiveType.RealTime});
        }

        /// <summary>
        ///     Find All Member With Specific Query
        /// </summary>
        /// <param name="query">(NOTNULL) Query </param>
        /// <param name="limit">(Max = 15) The Result Limits</param>
        public override async Task FindMember(string query, int limit = 10)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "FindMember");
            if (string.IsNullOrEmpty(query))
                throw new GameServiceException("query Cant Be EmptyOrNull").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "FindMember");
            if (limit <= 0 || limit > 15)
                throw new GameServiceException("invalid Limit Value").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "FindMember");

            await GameService.GSLive.GetGsHandler().CommandHandler.RequestAsync(FindMemberHandler.Signature,
                new RoomDetail {Max = limit, UserOrMemberId = query});
        }


        internal override void SendEvent(byte[] caller, byte[] data, GProtocolSendType sendType)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "SendEvent");
            if (caller == null || data == null)
                throw new GameServiceException("caller or data Cant Be Null").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "SendEvent");
            if (GameService.GSLive.GetGsHandler().RealTimeHandler == null)
                throw new GameServiceException("You Must Create or Join Room First").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "SendEvent");

            GameService.GSLive.GetGsHandler().RealTimeHandler.Request(NewEventHandler.Signature, sendType,
                new DataPayload {Payload = data, ExtraData = caller}, true);
        }


        internal override void SendObserver(byte[] caller, byte[] data)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "SendObserver");
            if (data == null)
                throw new GameServiceException("caller or data Cant Be Null").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "SendObserver");
            if (GameService.GSLive.GetGsHandler().RealTimeHandler == null)
                throw new GameServiceException("You Must Create or Join Room First").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "SendObserver");

            ObserverCompacterUtil.AddToQueue(new DataPayload {Payload = data, ExtraData = caller});
        }
    }
}