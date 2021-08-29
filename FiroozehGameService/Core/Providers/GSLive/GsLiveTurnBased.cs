// <copyright file="GsLiveTurnBased.cs" company="Firoozeh Technology LTD">
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


using System.Collections.Generic;
using System.Threading;
using FiroozehGameService.Core.GSLive;
using FiroozehGameService.Handlers;
using FiroozehGameService.Handlers.Command.RequestHandlers;
using FiroozehGameService.Handlers.TurnBased.RequestHandlers;
using FiroozehGameService.Models;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.Enums.GSLive;
using FiroozehGameService.Models.Enums.GSLive.TB;
using FiroozehGameService.Models.GSLive.Command;
using FiroozehGameService.Models.GSLive.Providers;
using FiroozehGameService.Models.GSLive.TB;
using FiroozehGameService.Models.Internal;
using FiroozehGameService.Utils;

namespace FiroozehGameService.Core.Providers.GSLive
{
    internal class GsLiveTurnBased : GsLiveTurnBasedProvider
    {
        internal static bool InAutoMatch;

        public override void CreateRoom(GSLiveOption.CreateRoomOption option)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "CreateRoom");

            if (option == null)
                throw new GameServiceException("option Cant Be Null").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "CreateRoom");

            if (option.MaxPlayer < TurnBasedConst.MinPlayer || option.MaxPlayer > TurnBasedConst.MaxPlayer)
                throw new GameServiceException("Invalid MaxPlayer Value")
                    .LogException<GsLiveTurnBased>(DebugLocation.TurnBased, "CreateRoom");

            if (InAutoMatch)
                throw new GameServiceException("You Can't Create Room When You are In AutoMatch State")
                    .LogException<GsLiveTurnBased>(
                        DebugLocation.TurnBased, "CreateRoom");

            if (GameService.GSLive.GetGsHandler().TurnBasedHandler != null)
                throw new GameServiceException("You Can't Connect To Multiple Rooms").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "CreateRoom");

            option.GsLiveType = GSLiveType.TurnBased;
            GameService.GSLive.GetGsHandler().CommandHandler.Send(CreateRoomHandler.Signature, option);
        }


        public override void AutoMatch(GSLiveOption.AutoMatchOption option)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "AutoMatch");

            if (option == null)
                throw new GameServiceException("option Cant Be Null").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "AutoMatch");

            if (option.MinPlayer < TurnBasedConst.MinPlayer || option.MinPlayer > TurnBasedConst.MaxPlayer)
                throw new GameServiceException("Invalid MinPlayer Value")
                    .LogException<GsLiveTurnBased>(DebugLocation.TurnBased, "AutoMatch");

            if (option.MaxPlayer < TurnBasedConst.MinPlayer || option.MaxPlayer > TurnBasedConst.MaxPlayer)
                throw new GameServiceException("Invalid MaxPlayer Value")
                    .LogException<GsLiveTurnBased>(DebugLocation.TurnBased, "AutoMatch");

            if (InAutoMatch)
                throw new GameServiceException("You Can't Do Multiple AutoMatch In SameTime")
                    .LogException<GsLiveTurnBased>(
                        DebugLocation.TurnBased, "AutoMatch");

            if (GameService.GSLive.GetGsHandler().TurnBasedHandler != null)
                throw new GameServiceException("You Can't Connect To Multiple Rooms").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "AutoMatch");


            option.GsLiveType = GSLiveType.TurnBased;
            InAutoMatch = true;
            GameService.GSLive.GetGsHandler().CommandHandler.Send(AutoMatchHandler.Signature, option);
        }


        public override void CancelAutoMatch()
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "CancelAutoMatch");

            InAutoMatch = false;
            GameService.GSLive.GetGsHandler().CommandHandler.Send(CancelAutoMatchHandler.Signature);
        }


        public override void JoinRoom(string roomId, string extra = null, string password = null)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "JoinRoom");
            if (string.IsNullOrEmpty(roomId))
                throw new GameServiceException("roomId Cant Be NullOrEmpty").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "JoinRoom");

            if (InAutoMatch)
                throw new GameServiceException("You Can't Join Room When You are in AutoMatch State")
                    .LogException<GsLiveTurnBased>(
                        DebugLocation.TurnBased, "JoinRoom");

            if (GameService.GSLive.GetGsHandler().TurnBasedHandler != null)
                throw new GameServiceException("You Can't Connect To Multiple Rooms").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "JoinRoom");


            GameService.GSLive.GetGsHandler().CommandHandler.Send(JoinRoomHandler.Signature,
                new RoomDetail
                    {Id = roomId, Extra = extra, RoomPassword = password, GsLiveType = (int) GSLiveType.TurnBased});
        }


        public override void GetAvailableRooms(string role)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "GetAvailableRooms");
            if (string.IsNullOrEmpty(role))
                throw new GameServiceException("Role Cant Be NullOrEmpty").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "GetAvailableRooms");

            GameService.GSLive.GetGsHandler().CommandHandler.Send(GetRoomsHandler.Signature,
                new RoomDetail {Role = role, GsLiveType = (int) GSLiveType.TurnBased});
        }

        public override void SendPublicMessage(string data)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "SendPublicMessage");

            if (string.IsNullOrEmpty(data))
                throw new GameServiceException("data Cant Be NullOrEmpty").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "SendPublicMessage");

            if (data.Length > TurnBasedConst.MaxDataLength)
                throw new GameServiceException("The Data is Too Long, Max Data Length Is " +
                                               TurnBasedConst.MaxDataLength + " Characters.")
                    .LogException<GsLiveTurnBased>(
                        DebugLocation.TurnBased, "SendPublicMessage");

            if (GameService.GSLive.GetGsHandler().TurnBasedHandler == null)
                throw new GameServiceException("You Must Create or Join Room First").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "SendPublicMessage");

            GameService.GSLive.GetGsHandler().TurnBasedHandler.Send(SendPublicMessageHandler.Signature,
                new DataPayload {Data = data});
        }

        public override void SendPrivateMessage(string receiverMemberId, string data)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "SendPrivateMessage");

            if (string.IsNullOrEmpty(receiverMemberId) || string.IsNullOrEmpty(data))
                throw new GameServiceException("receiverMemberId Or data Cant Be NullOrEmpty")
                    .LogException<GsLiveTurnBased>(
                        DebugLocation.TurnBased, "SendPrivateMessage");

            if (data.Length > TurnBasedConst.MaxDataLength)
                throw new GameServiceException("The Data is Too Long, Max Data Length Is " +
                                               TurnBasedConst.MaxDataLength + " Characters.")
                    .LogException<GsLiveTurnBased>(
                        DebugLocation.TurnBased, "SendPrivateMessage");

            if (GameService.GSLive.GetGsHandler().TurnBasedHandler == null)
                throw new GameServiceException("You Must Create or Join Room First").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "SendPrivateMessage");

            GameService.GSLive.GetGsHandler().TurnBasedHandler.Send(SendPrivateMessageHandler.Signature,
                new DataPayload {Data = data, Id = receiverMemberId});
        }


        public override void TakeTurn(string data = null, string whoIsNext = null)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "TakeTurn");

            if (GameService.GSLive.GetGsHandler().TurnBasedHandler == null)
                throw new GameServiceException("You Must Create or Join Room First").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "TakeTurn");

            GameService.GSLive.GetGsHandler().TurnBasedHandler.Send(TakeTurnHandler.Signature,
                new DataPayload {Data = data, NextId = whoIsNext});
        }


        public override void ChooseNext(string whoIsNext = null)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "ChooseNext");

            if (GameService.GSLive.GetGsHandler().TurnBasedHandler == null)
                throw new GameServiceException("You Must Create or Join Room First").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "ChooseNext");

            GameService.GSLive.GetGsHandler().TurnBasedHandler.Send(ChooseNextHandler.Signature,
                new DataPayload {NextId = whoIsNext});
        }


        public override void SetOrUpdateProperty(PropertyType type, KeyValuePair<string, string> propertyData)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "SetProperty");

            if (GameService.GSLive.GetGsHandler().TurnBasedHandler == null)
                throw new GameServiceException("You Must Create or Join Room First").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "SetProperty");

            if (string.IsNullOrEmpty(propertyData.Key))
                throw new GameServiceException("property Key Cant Be NullOrEmpty").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "SetProperty");
            if (string.IsNullOrEmpty(propertyData.Value))
                throw new GameServiceException("property Value Cant Be NullOrEmpty").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "SetProperty");

            GameService.GSLive.GetGsHandler().TurnBasedHandler.Send(PropertyHandler.Signature,
                new DataPayload
                {
                    Action = type == PropertyType.Member
                        ? InternalPropertyAction.MemberSetOrUpdate
                        : InternalPropertyAction.RoomSetOrUpdate,
                    Id = propertyData.Key, Data = propertyData.Value
                });
        }


        public override void RemoveProperty(PropertyType type, string key)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "RemoveProperty");

            if (GameService.GSLive.GetGsHandler().TurnBasedHandler == null)
                throw new GameServiceException("You Must Create or Join Room First").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "RemoveProperty");

            if (string.IsNullOrEmpty(key))
                throw new GameServiceException("Key Cant Be NullOrEmpty").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "CreateRoom");

            GameService.GSLive.GetGsHandler().TurnBasedHandler.Send(PropertyHandler.Signature,
                new DataPayload
                {
                    Action = type == PropertyType.Member
                        ? InternalPropertyAction.MemberDelete
                        : InternalPropertyAction.RoomDelete,
                    Id = key
                });
        }


        public override void GetMemberProperties()
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "GetProperties");

            if (GameService.GSLive.GetGsHandler().TurnBasedHandler == null)
                throw new GameServiceException("You Must Create or Join Room First").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "GetProperties");

            GameService.GSLive.GetGsHandler().TurnBasedHandler.Send(SnapshotHandler.Signature);
        }


        public override void GetCurrentRoomInfo()
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "GetCurrentRoomInfo");

            if (GameService.GSLive.GetGsHandler().TurnBasedHandler == null)
                throw new GameServiceException("You Must Create or Join Room First").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "GetCurrentRoomInfo");

            GameService.GSLive.GetGsHandler().TurnBasedHandler.Send(RoomInfoHandler.Signature);
        }


        public override void LeaveRoom(string whoIsNext = null)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "LeaveRoom");

            if (GameService.GSLive.GetGsHandler().TurnBasedHandler == null)
                throw new GameServiceException("You Must Create or Join Room First").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "LeaveRoom");

            GameService.GSLive.GetGsHandler().TurnBasedHandler
                .Send(LeaveRoomHandler.Signature, new DataPayload {NextId = whoIsNext});

            Thread.Sleep(200);

            CoreEventHandlers.Dispose?.Invoke(null, new DisposeData
            {
                Type = GSLiveType.TurnBased, IsGraceful = true
            });
        }


        public override void Vote(Dictionary<string, Outcome> outcomes)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "Finish");

            if (outcomes == null)
                throw new GameServiceException("outcomes Cant Be Null").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "Finish");

            if (GameService.GSLive.GetGsHandler().TurnBasedHandler == null)
                throw new GameServiceException("You Must Create or Join Room First").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "Finish");

            GameService.GSLive.GetGsHandler().TurnBasedHandler.Send(VoteHandler.Signature,
                new DataPayload {Outcomes = outcomes});
        }


        public override void AcceptVote(string memberId)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "AcceptVote");

            if (string.IsNullOrEmpty(memberId))
                throw new GameServiceException("memberId Cant Be NullOrEmpty").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "AcceptVote");

            if (GameService.GSLive.GetGsHandler().TurnBasedHandler == null)
                throw new GameServiceException("You Must Create or Join Room First").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "AcceptVote");

            GameService.GSLive.GetGsHandler().TurnBasedHandler.Send(AcceptVoteHandler.Signature,
                new DataPayload {Id = memberId});
        }


        public override void GetRoomMembersDetail()
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "GetRoomMembersDetail");

            if (GameService.GSLive.GetGsHandler().TurnBasedHandler == null)
                throw new GameServiceException("You Must Create or Join Room First").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "GetRoomMembersDetail");

            GameService.GSLive.GetGsHandler().TurnBasedHandler.Send(GetMemberHandler.Signature);
        }


        public override void GetCurrentTurnMember()
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "GetCurrentTurnMember");
            if (GameService.GSLive.GetGsHandler().TurnBasedHandler == null)
                throw new GameServiceException("You Must Create or Join Room First").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "GetCurrentTurnMember");

            GameService.GSLive.GetGsHandler().TurnBasedHandler.Send(CurrentTurnHandler.Signature);
        }


        public override void GetInviteInbox()
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "GetInviteInbox");
            GameService.GSLive.GetGsHandler().CommandHandler.Send(InviteListHandler.Signature,
                new RoomDetail {GsLiveType = (int) GSLiveType.TurnBased});
        }


        public override void InviteUser(string roomId, string userId)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "InviteUser");
            if (string.IsNullOrEmpty(roomId) || string.IsNullOrEmpty(userId))
                throw new GameServiceException("roomId Or userId Cant Be NullOrEmpty").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "InviteUser");

            GameService.GSLive.GetGsHandler().CommandHandler.Send(InviteUserHandler.Signature,
                new RoomDetail {UserOrMemberId = userId, Id = roomId, GsLiveType = (int) GSLiveType.TurnBased});
        }


        public override void AcceptInvite(string inviteId, string extra = null)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "AcceptInvite");

            if (string.IsNullOrEmpty(inviteId))
                throw new GameServiceException("inviteId Cant Be NullOrEmpty").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "AcceptInvite");

            if (InAutoMatch)
                throw new GameServiceException("You Can't Accept Invite When You are in AutoMatch State")
                    .LogException<GsLiveTurnBased>(
                        DebugLocation.TurnBased, "AcceptInvite");


            if (GameService.GSLive.GetGsHandler().TurnBasedHandler != null)
                throw new GameServiceException("You Can't Connect To Multiple Rooms").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "AcceptInvite");


            GameService.GSLive.GetGsHandler().CommandHandler.Send(AcceptInviteHandler.Signature,
                new RoomDetail {Invite = inviteId, Extra = extra, GsLiveType = (int) GSLiveType.TurnBased});
        }


        public override void FindMember(string query, int limit = 10)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "FindMember");
            if (string.IsNullOrEmpty(query))
                throw new GameServiceException("query Cant Be NullOrEmpty").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "FindMember");
            if (limit < 0 || limit > 15)
                throw new GameServiceException("invalid Limit Value").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "FindMember");

            GameService.GSLive.GetGsHandler().CommandHandler.Send(FindMemberHandler.Signature,
                new RoomDetail {Max = limit, UserOrMemberId = query});
        }

        public override void GetRoomsInfo(string role)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "GetRoomsInfo");
            if (string.IsNullOrEmpty(role))
                throw new GameServiceException("Role Cant Be NullOrEmpty").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "GetRoomsInfo");

            GameService.GSLive.GetGsHandler().CommandHandler.Send(GetRoomsInfoHandler.Signature,
                new RoomDetail {Role = role, GsLiveType = (int) GSLiveType.TurnBased});
        }
    }
}