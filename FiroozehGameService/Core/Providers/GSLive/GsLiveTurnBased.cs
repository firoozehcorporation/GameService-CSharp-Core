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
using System.Threading.Tasks;
using FiroozehGameService.Core.GSLive;
using FiroozehGameService.Handlers.Command.RequestHandlers;
using FiroozehGameService.Handlers.TurnBased.RequestHandlers;
using FiroozehGameService.Models;
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
        public override async Task CreateRoom(GSLiveOption.CreateRoomOption option)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "CreateRoom");
            if (option == null)
                throw new GameServiceException("option Cant Be Null").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "CreateRoom");

            option.GsLiveType = GSLiveType.TurnBased;
            await GameService.GSLive.GetGsHandler().CommandHandler.RequestAsync(CreateRoomHandler.Signature, option);
        }


        public override async Task AutoMatch(GSLiveOption.AutoMatchOption option)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "AutoMatch");
            if (option == null)
                throw new GameServiceException("option Cant Be Null").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "AutoMatch");

            option.GsLiveType = GSLiveType.TurnBased;
            await GameService.GSLive.GetGsHandler().CommandHandler.RequestAsync(AutoMatchHandler.Signature, option);
        }


        public override async Task CancelAutoMatch()
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "CancelAutoMatch");
            await GameService.GSLive.GetGsHandler().CommandHandler.RequestAsync(CancelAutoMatchHandler.Signature);
        }


        public override async Task JoinRoom(string roomId, string extra = null, string password = null)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "JoinRoom");
            if (string.IsNullOrEmpty(roomId))
                throw new GameServiceException("roomId Cant Be EmptyOrNull").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "JoinRoom");

            await GameService.GSLive.GetGsHandler().CommandHandler.RequestAsync(JoinRoomHandler.Signature,
                new RoomDetail {Id = roomId, Extra = extra, RoomPassword = password});
        }


        public override async Task GetAvailableRooms(string role)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "GetAvailableRooms");
            if (string.IsNullOrEmpty(role))
                throw new GameServiceException("role Cant Be EmptyOrNull").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "GetAvailableRooms");

            await GameService.GSLive.GetGsHandler().CommandHandler.RequestAsync(GetRoomsHandler.Signature,
                new RoomDetail {Role = role, GsLiveType = (int) GSLiveType.TurnBased});
        }


        public override async Task TakeTurn(string data = null, string whoIsNext = null)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "TakeTurn");
            if (GameService.GSLive.GetGsHandler().TurnBasedHandler == null)
                throw new GameServiceException("You Must Create or Join Room First").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "TakeTurn");

            await GameService.GSLive.GetGsHandler().TurnBasedHandler.RequestAsync(TakeTurnHandler.Signature,
                new DataPayload {Data = data, NextId = whoIsNext});
        }


        public override async Task ChooseNext(string whoIsNext = null)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "ChooseNext");
            if (GameService.GSLive.GetGsHandler().TurnBasedHandler == null)
                throw new GameServiceException("You Must Create or Join Room First").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "ChooseNext");

            await GameService.GSLive.GetGsHandler().TurnBasedHandler.RequestAsync(ChooseNextHandler.Signature,
                new DataPayload {NextId = whoIsNext});
        }


        public override async Task SetProperty(PropertyType type, string key, string value)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "SetProperty");
            if (GameService.GSLive.GetGsHandler().TurnBasedHandler == null)
                throw new GameServiceException("You Must Create or Join Room First").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "SetProperty");

            if (string.IsNullOrEmpty(key))
                throw new GameServiceException("Key Cant Be EmptyOrNull").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "SetProperty");
            if (string.IsNullOrEmpty(value))
                throw new GameServiceException("Value Cant Be EmptyOrNull").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "SetProperty");

            await GameService.GSLive.GetGsHandler().TurnBasedHandler.RequestAsync(PropertyHandler.Signature,
                new DataPayload
                {
                    Action = type == PropertyType.Member
                        ? InternalPropertyAction.MemberSetOrUpdate
                        : InternalPropertyAction.RoomSetOrUpdate,
                    Id = key, Data = value
                });
        }


        public override async Task RemoveProperty(PropertyType type, string key)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "RemoveProperty");
            if (GameService.GSLive.GetGsHandler().TurnBasedHandler == null)
                throw new GameServiceException("You Must Create or Join Room First").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "RemoveProperty");

            if (string.IsNullOrEmpty(key))
                throw new GameServiceException("Key Cant Be EmptyOrNull").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "CreateRoom");

            await GameService.GSLive.GetGsHandler().TurnBasedHandler.RequestAsync(PropertyHandler.Signature,
                new DataPayload
                {
                    Action = type == PropertyType.Member
                        ? InternalPropertyAction.MemberDelete
                        : InternalPropertyAction.RoomDelete,
                    Id = key
                });
        }


        public override async Task GetMemberProperties()
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "GetProperties");
            if (GameService.GSLive.GetGsHandler().TurnBasedHandler == null)
                throw new GameServiceException("You Must Create or Join Room First").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "GetProperties");

            await GameService.GSLive.GetGsHandler().TurnBasedHandler.RequestAsync(SnapshotHandler.Signature);
        }


        public override async Task GetCurrentRoomInfo()
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "GetCurrentRoomInfo");
            if (GameService.GSLive.GetGsHandler().TurnBasedHandler == null)
                throw new GameServiceException("You Must Create or Join Room First").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "GetCurrentRoomInfo");

            await GameService.GSLive.GetGsHandler().TurnBasedHandler.RequestAsync(RoomInfoHandler.Signature);
        }


        public override async Task LeaveRoom(string whoIsNext = null)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "LeaveRoom");
            if (GameService.GSLive.GetGsHandler().TurnBasedHandler == null)
                throw new GameServiceException("You Must Create or Join Room First").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "LeaveRoom");

            await GameService.GSLive.GetGsHandler().TurnBasedHandler
                ?.RequestAsync(LeaveRoomHandler.Signature, new DataPayload {NextId = whoIsNext});
            GameService.GSLive.GetGsHandler().TurnBasedHandler?.Dispose();
        }


        public override async Task Vote(Dictionary<string, Outcome> outcomes)
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

            await GameService.GSLive.GetGsHandler().TurnBasedHandler.RequestAsync(VoteHandler.Signature,
                new DataPayload {Outcomes = outcomes});
        }


        public override async Task Complete(string memberId)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "Complete");
            if (string.IsNullOrEmpty(memberId))
                throw new GameServiceException("memberId Cant Be EmptyOrNull").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "Complete");
            if (GameService.GSLive.GetGsHandler().TurnBasedHandler == null)
                throw new GameServiceException("You Must Create or Join Room First").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "Complete");

            await GameService.GSLive.GetGsHandler().TurnBasedHandler.RequestAsync(CompleteHandler.Signature,
                new DataPayload {Id = memberId});
        }


        public override async Task GetRoomMembersDetail()
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "GetRoomMembersDetail");
            if (GameService.GSLive.GetGsHandler().TurnBasedHandler == null)
                throw new GameServiceException("You Must Create or Join Room First").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "GetRoomMembersDetail");

            await GameService.GSLive.GetGsHandler().TurnBasedHandler.RequestAsync(GetMemberHandler.Signature);
        }


        public override async Task GetCurrentTurnMember()
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "GetCurrentTurnMember");
            if (GameService.GSLive.GetGsHandler().TurnBasedHandler == null)
                throw new GameServiceException("You Must Create or Join Room First").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "GetCurrentTurnMember");

            await GameService.GSLive.GetGsHandler().TurnBasedHandler.RequestAsync(CurrentTurnHandler.Signature);
        }


        public override async Task GetInviteInbox()
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "GetInviteInbox");
            await GameService.GSLive.GetGsHandler().CommandHandler.RequestAsync(InviteListHandler.Signature,
                new RoomDetail {GsLiveType = (int) GSLiveType.TurnBased});
        }


        public override async Task InviteUser(string roomId, string userId)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "InviteUser");
            if (string.IsNullOrEmpty(roomId) && string.IsNullOrEmpty(userId))
                throw new GameServiceException("roomId Or userId Cant Be EmptyOrNull").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "InviteUser");

            await GameService.GSLive.GetGsHandler().CommandHandler.RequestAsync(InviteUserHandler.Signature,
                new RoomDetail {UserOrMemberId = userId, Id = roomId, GsLiveType = (int) GSLiveType.TurnBased});
        }


        public override async Task AcceptInvite(string inviteId, string extra = null)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "AcceptInvite");
            if (string.IsNullOrEmpty(inviteId))
                throw new GameServiceException("inviteId Cant Be EmptyOrNull").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "AcceptInvite");

            await GameService.GSLive.GetGsHandler().CommandHandler.RequestAsync(AcceptInviteHandler.Signature,
                new RoomDetail {Invite = inviteId, Extra = extra, GsLiveType = (int) GSLiveType.RealTime});
        }


        public override async Task FindMember(string query, int limit = 10)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "FindMember");
            if (string.IsNullOrEmpty(query))
                throw new GameServiceException("query Cant Be EmptyOrNull").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "FindMember");
            if (limit < 0 || limit > 15)
                throw new GameServiceException("invalid Limit Value").LogException<GsLiveTurnBased>(
                    DebugLocation.TurnBased, "FindMember");

            await GameService.GSLive.GetGsHandler().CommandHandler.RequestAsync(FindMemberHandler.Signature,
                new RoomDetail {Max = limit, UserOrMemberId = query});
        }
    }
}