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


using FiroozehGameService.Core.GSLive;
using FiroozehGameService.Handlers;
using FiroozehGameService.Handlers.Command.RequestHandlers;
using FiroozehGameService.Handlers.RealTime;
using FiroozehGameService.Handlers.RealTime.RequestHandlers;
using FiroozehGameService.Models;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.Enums.GSLive;
using FiroozehGameService.Models.GSLive.Command;
using FiroozehGameService.Models.GSLive.Providers;
using FiroozehGameService.Models.GSLive.RT;
using FiroozehGameService.Models.Internal;
using FiroozehGameService.Utils;

namespace FiroozehGameService.Core.Providers.GSLive
{
    internal class GsLiveRealTime : GsLiveRealTimeProvider
    {
        internal static bool InAutoMatch;

        public override void CreateRoom(GSLiveOption.CreateRoomOption option)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "CreateRoom");

            if (option == null)
                throw new GameServiceException("option Cant Be Null").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "CreateRoom");

            if (option.MinPlayer < RealTimeConst.MinPlayer || option.MinPlayer > RealTimeConst.MaxPlayer)
                throw new GameServiceException("Invalid MinPlayer Value")
                    .LogException<GsLiveRealTime>(DebugLocation.RealTime, "CreateRoom");

            if (option.MaxPlayer < RealTimeConst.MinPlayer || option.MaxPlayer > RealTimeConst.MaxPlayer)
                throw new GameServiceException("Invalid MaxPlayer Value")
                    .LogException<GsLiveRealTime>(DebugLocation.RealTime, "CreateRoom");

            if (InAutoMatch)
                throw new GameServiceException("You Can't Create Room When You are In AutoMatch State")
                    .LogException<GsLiveRealTime>(
                        DebugLocation.RealTime, "CreateRoom");

            if (GameService.GSLive.GetGsHandler().RealTimeHandler != null)
                throw new GameServiceException("You Can't Connect To Multiple Rooms").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "CreateRoom");


            option.GsLiveType = GSLiveType.RealTime;
            GameService.GSLive.GetGsHandler().CommandHandler.Send(CreateRoomHandler.Signature, option);
        }

        public override void AutoMatch(GSLiveOption.AutoMatchOption option)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "AutoMatch");

            if (option == null)
                throw new GameServiceException("option Cant Be Null").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "AutoMatch");

            if (option.MinPlayer < RealTimeConst.MinPlayer || option.MinPlayer > RealTimeConst.MaxPlayer)
                throw new GameServiceException("Invalid MinPlayer Value")
                    .LogException<GsLiveRealTime>(DebugLocation.RealTime, "AutoMatch");

            if (option.MaxPlayer < RealTimeConst.MinPlayer || option.MaxPlayer > RealTimeConst.MaxPlayer)
                throw new GameServiceException("Invalid MaxPlayer Value")
                    .LogException<GsLiveRealTime>(DebugLocation.RealTime, "AutoMatch");

            if (InAutoMatch)
                throw new GameServiceException("You Can't Do Multiple AutoMatch In SameTime")
                    .LogException<GsLiveRealTime>(
                        DebugLocation.RealTime, "AutoMatch");

            if (GameService.GSLive.GetGsHandler().RealTimeHandler != null)
                throw new GameServiceException("You Can't Connect To Multiple Rooms").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "AutoMatch");


            option.GsLiveType = GSLiveType.RealTime;
            InAutoMatch = true;
            GameService.GSLive.GetGsHandler().CommandHandler.Send(AutoMatchHandler.Signature, option);
        }

        public override void CancelAutoMatch()
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "CancelAutoMatch");

            InAutoMatch = false;
            GameService.GSLive.GetGsHandler().CommandHandler.Send(CancelAutoMatchHandler.Signature);
        }


        public override void JoinRoom(string roomId, string extra = null, string password = null)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "JoinRoom");

            if (string.IsNullOrEmpty(roomId))
                throw new GameServiceException("roomId Cant Be EmptyOrNull").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "JoinRoom");

            if (InAutoMatch)
                throw new GameServiceException("You Can't Join Room When You are in AutoMatch State")
                    .LogException<GsLiveRealTime>(
                        DebugLocation.RealTime, "JoinRoom");

            if (GameService.GSLive.GetGsHandler().RealTimeHandler != null)
                throw new GameServiceException("You Can't Connect To Multiple Rooms").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "JoinRoom");


            GameService.GSLive.GetGsHandler().CommandHandler.Send(JoinRoomHandler.Signature,
                new RoomDetail
                    {Id = roomId, Extra = extra, RoomPassword = password, GsLiveType = (int) GSLiveType.RealTime});
        }

        public override void LeaveRoom()
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "LeaveRoom");

            if (GameService.GSLive.GetGsHandler().RealTimeHandler == null)
                throw new GameServiceException("You Must Create or Join Room First").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "LeaveRoom");

            GameService.GSLive.GetGsHandler().RealTimeHandler.Request(LeaveRoomHandler.Signature,
                GProtocolSendType.Reliable,
                isCritical: true);

            CoreEventHandlers.Dispose?.Invoke(null, new DisposeData
            {
                Type = GSLiveType.RealTime, IsGraceful = true
            });
        }


        public override void GetAvailableRooms(string role)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "GetAvailableRooms");
            if (string.IsNullOrEmpty(role))
                throw new GameServiceException("role Cant Be EmptyOrNull").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "GetAvailableRooms");

            GameService.GSLive.GetGsHandler().CommandHandler.Send(GetRoomsHandler.Signature,
                new RoomDetail {Role = role, GsLiveType = (int) GSLiveType.RealTime});
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


        public override void SendPrivateMessage(string receiverMemberId, byte[] data)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "SendPrivateMessage");
            if (string.IsNullOrEmpty(receiverMemberId) || data == null)
                throw new GameServiceException("data Or receiverMemberId Cant Be EmptyOrNull")
                    .LogException<GsLiveRealTime>(
                        DebugLocation.RealTime, "SendPrivateMessage");
            if (GameService.GSLive.GetGsHandler().RealTimeHandler == null)
                throw new GameServiceException("You Must Create or Join Room First").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "SendPrivateMessage");

            GameService.GSLive.GetGsHandler().RealTimeHandler.Request(SendPrivateMessageHandler.Signature,
                GProtocolSendType.Reliable,
                new DataPayload {ReceiverId = receiverMemberId, Payload = data});
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
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "GetCurrentRoomInfo");
            if (GameService.GSLive.GetGsHandler().RealTimeHandler == null)
                throw new GameServiceException("You Must Create or Join Room First").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "GetCurrentRoomInfo");

            GameService.GSLive.GetGsHandler().RealTimeHandler
                .Request(RoomInfoHandler.Signature, GProtocolSendType.Reliable);
        }


        /// <summary>
        ///     Get Your Invite Inbox
        /// </summary>
        public override void GetInviteInbox()
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "GetInviteInbox");

            GameService.GSLive.GetGsHandler().CommandHandler.Send(InviteListHandler.Signature,
                new RoomDetail {GsLiveType = (int) GSLiveType.RealTime});
        }


        /// <summary>
        ///     Invite a Specific Player To Specific Room
        /// </summary>
        /// <param name="roomId">(NOTNULL) (Type : RoomID)Room's ID</param>
        /// <param name="userId">(NOTNULL) (Type : UserID)User's ID</param>
        public override void InviteUser(string roomId, string userId)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "InviteUser");
            if (string.IsNullOrEmpty(roomId) || string.IsNullOrEmpty(userId))
                throw new GameServiceException("roomId Or userId Cant Be EmptyOrNull").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "InviteUser");

            GameService.GSLive.GetGsHandler().CommandHandler.Send(InviteUserHandler.Signature,
                new RoomDetail {UserOrMemberId = userId, Id = roomId, GsLiveType = (int) GSLiveType.RealTime});
        }


        /// <summary>
        ///     Accept a Specific Invite With Invite ID
        ///     Note: After accepting the invitation, you will be automatically entered into the game room
        /// </summary>
        /// <param name="inviteId">(NOTNULL) (Type : InviteID) Invite's ID</param>
        /// <param name="extra">Specifies the Extra Data To Send to Other Clients</param>
        public override void AcceptInvite(string inviteId, string extra = null)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "AcceptInvite");

            if (string.IsNullOrEmpty(inviteId))
                throw new GameServiceException("inviteId Cant Be EmptyOrNull").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "AcceptInvite");

            if (InAutoMatch)
                throw new GameServiceException("You Can't Accept Invite When You are in AutoMatch State")
                    .LogException<GsLiveRealTime>(
                        DebugLocation.RealTime, "AcceptInvite");


            if (GameService.GSLive.GetGsHandler().RealTimeHandler != null)
                throw new GameServiceException("You Can't Connect To Multiple Rooms").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "AcceptInvite");


            GameService.GSLive.GetGsHandler().CommandHandler.Send(AcceptInviteHandler.Signature,
                new RoomDetail {Invite = inviteId, Extra = extra, GsLiveType = (int) GSLiveType.RealTime});
        }

        /// <summary>
        ///     Find All Member With Specific Query
        /// </summary>
        /// <param name="query">(NOTNULL) Query </param>
        /// <param name="limit">(Max = 15) The Result Limits</param>
        public override void FindMember(string query, int limit = 10)
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

            GameService.GSLive.GetGsHandler().CommandHandler.Send(FindMemberHandler.Signature,
                new RoomDetail {Max = limit, UserOrMemberId = query});
        }

        /// <summary>
        ///     Get Current RoundTripTime(RTT)
        ///     NOTE : You Must Join To RealTime Servers To Get Valid Data, Otherwise Return -1
        /// </summary>
        public override int GetRoundTripTime()
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "GetRoundTripTime");

            return RealTimeHandler.GetRoundTripTime();
        }


        /// <summary>
        ///     Get Current PacketLost
        ///     NOTE : You Must Join To RealTime Servers To Get Valid Data, Otherwise Return -1
        /// </summary>
        public override long GetPacketLost()
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "GetPacketLost");

            return RealTimeHandler.GetPacketLost();
        }

        public override void GetRoomsInfo(string role)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "GetRoomsInfo");
            if (string.IsNullOrEmpty(role))
                throw new GameServiceException("role Cant Be EmptyOrNull").LogException<GsLiveRealTime>(
                    DebugLocation.RealTime, "GetRoomsInfo");

            GameService.GSLive.GetGsHandler().CommandHandler.Send(GetRoomsInfoHandler.Signature,
                new RoomDetail {Role = role, GsLiveType = (int) GSLiveType.RealTime});
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
                new DataPayload {Payload = data, ExtraData = caller}, false, true, true);
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