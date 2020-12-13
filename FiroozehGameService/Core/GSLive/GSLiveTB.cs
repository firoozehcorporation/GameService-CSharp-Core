// <copyright file="GSLiveTB.cs" company="Firoozeh Technology LTD">
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
using System.Collections.Generic;
using System.Threading.Tasks;
using FiroozehGameService.Handlers.Command.RequestHandlers;
using FiroozehGameService.Handlers.TurnBased.RequestHandlers;
using FiroozehGameService.Models;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.Enums.GSLive;
using FiroozehGameService.Models.Enums.GSLive.TB;
using FiroozehGameService.Models.GSLive.Command;
using FiroozehGameService.Models.GSLive.TB;
using FiroozehGameService.Models.Internal;
using FiroozehGameService.Utils;

namespace FiroozehGameService.Core.GSLive
{
    /// <summary>
    ///     Represents Game Service TurnBased MultiPlayer System
    /// </summary>
    public class GSLiveTB
    {
        private const string Tag = "GSLive-TurnBased";

        internal GSLiveTB()
        {
        }

        /// <summary>
        ///     Create Room With Option Like : Name , Min , Max , Role , IsPrivate
        /// </summary>
        /// <param name="option">(NOTNULL)Create Room Option</param>
        public async Task CreateRoom(GSLiveOption.CreateRoomOption option)
        {
            if (GameService.IsGuest) throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GSLiveTB>(DebugLocation.TurnBased,"CreateRoom");
            if (option == null) throw new GameServiceException("option Cant Be Null").LogException<GSLiveTB>(DebugLocation.TurnBased,"CreateRoom");
            
            option.GsLiveType = GSLiveType.TurnBased;
            await GSLive.Handler.CommandHandler.RequestAsync(CreateRoomHandler.Signature, option);
        }


        /// <summary>
        ///     Create AutoMatch With Option Like :  Min , Max , Role
        /// </summary>
        /// <param name="option">(NOTNULL)AutoMatch Option</param>
        public async Task AutoMatch(GSLiveOption.AutoMatchOption option)
        {
            if (GameService.IsGuest) throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GSLiveTB>(DebugLocation.TurnBased,"AutoMatch");
            if (option == null) throw new GameServiceException("option Cant Be Null").LogException<GSLiveTB>(DebugLocation.TurnBased,"AutoMatch");
            
            option.GsLiveType = GSLiveType.TurnBased;
            await GSLive.Handler.CommandHandler.RequestAsync(AutoMatchHandler.Signature, option);
        }


        /// <summary>
        ///     Cancel Current AutoMatch
        /// </summary>
        public async Task CancelAutoMatch()
        {
            if (GameService.IsGuest) throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GSLiveTB>(DebugLocation.TurnBased,"CancelAutoMatch");
            await GSLive.Handler.CommandHandler.RequestAsync(CancelAutoMatchHandler.Signature);
        }


        /// <summary>
        ///     Join In Room With RoomID
        /// </summary>
        /// <param name="roomId">(NOTNULL)Room's id You Want To Join</param>
        /// <param name="extra">(NULLABLE)Specifies the Extra Data To Send to Other Clients</param>
        /// <param name="password">(NULLABLE)Specifies the Password if Room is Private</param>
        public async Task JoinRoom(string roomId, string extra = null,string password = null)
        {
            if (GameService.IsGuest) throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GSLiveTB>(DebugLocation.TurnBased,"JoinRoom");
            if (string.IsNullOrEmpty(roomId)) throw new GameServiceException("roomId Cant Be EmptyOrNull").LogException<GSLiveTB>(DebugLocation.TurnBased,"JoinRoom");
           
            await GSLive.Handler.CommandHandler.RequestAsync(JoinRoomHandler.Signature,
                new RoomDetail {Id = roomId, Extra = extra , RoomPassword = password});
        }


        /// <summary>
        ///     Get Available Rooms According To Room's Role
        /// </summary>
        /// <param name="role">(NOTNULL)Room's Role </param>
        public async Task GetAvailableRooms(string role)
        {
            if (GameService.IsGuest) throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GSLiveTB>(DebugLocation.TurnBased,"GetAvailableRooms");
            if (string.IsNullOrEmpty(role)) throw new GameServiceException("role Cant Be EmptyOrNull").LogException<GSLiveTB>(DebugLocation.TurnBased,"GetAvailableRooms");
          
            await GSLive.Handler.CommandHandler.RequestAsync(GetRoomsHandler.Signature,
                new RoomDetail {Role = role, GsLiveType = (int) GSLiveType.TurnBased});
        }


        /// <summary>
        ///     If is your Turn, you can send data to other players using this function.
        ///     Also if You Want to Move Your Turn to the Next player
        ///     put the next player ID in the function entry
        ///     You can use this function several times
        /// </summary>
        /// <param name="data">(NULLABLE)Room's Role </param>
        /// <param name="whoIsNext">(NULLABLE) Next Player's ID </param>
        public async Task TakeTurn(string data = null, string whoIsNext = null)
        {
            if (GameService.IsGuest) throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GSLiveTB>(DebugLocation.TurnBased,"TakeTurn");
            if (GSLive.Handler.TurnBasedHandler == null) throw new GameServiceException("You Must Create or Join Room First").LogException<GSLiveTB>(DebugLocation.TurnBased,"TakeTurn");
         
            await GSLive.Handler.TurnBasedHandler.RequestAsync(TakeTurnHandler.Signature,
                new DataPayload {Data = data, NextId = whoIsNext});
        }


        /// <summary>
        ///     If it's your turn, you can transfer the turn to the next player without sending data
        ///     if whoIsNext Set Null , Server Automatically Selects Next Turn
        /// </summary>
        /// <param name="whoIsNext">(NULLABLE)Next Player's ID </param>
        public async Task ChooseNext(string whoIsNext = null)
        {
            if (GameService.IsGuest) throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GSLiveTB>(DebugLocation.TurnBased,"ChooseNext");
            if (GSLive.Handler.TurnBasedHandler == null) throw new GameServiceException("You Must Create or Join Room First").LogException<GSLiveTB>(DebugLocation.TurnBased,"ChooseNext");
            
            await GSLive.Handler.TurnBasedHandler.RequestAsync(ChooseNextHandler.Signature,
                new DataPayload {NextId = whoIsNext});
        }

        
        /// <summary>
        ///     Every Member can Set Properties to Sync Data With EachOthers
        /// </summary>
        /// <param name="key">(NOTNULL)  The Key Value</param>
        /// <param name="value">(NOTNULL) The Value of Key </param>
        /// <param name="type">The Type of Property </param>
        public async Task SetProperty(PropertyType type,string key,string value)
        {
            if (GameService.IsGuest) throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GSLiveTB>(DebugLocation.TurnBased,"SetProperty");
            if (GSLive.Handler.TurnBasedHandler == null) throw new GameServiceException("You Must Create or Join Room First").LogException<GSLiveTB>(DebugLocation.TurnBased,"SetProperty");
          
            if (string.IsNullOrEmpty(key)) throw new GameServiceException("Key Cant Be EmptyOrNull").LogException<GSLiveTB>(DebugLocation.TurnBased,"SetProperty");
            if (string.IsNullOrEmpty(value)) throw new GameServiceException("Value Cant Be EmptyOrNull").LogException<GSLiveTB>(DebugLocation.TurnBased,"SetProperty");

            await GSLive.Handler.TurnBasedHandler.RequestAsync(PropertyHandler.Signature,
                new DataPayload {Action = type == PropertyType.Member ? InternalPropertyAction.MemberSetOrUpdate : InternalPropertyAction.RoomSetOrUpdate,Id = key , Data = value});
        }

        
        
        /// <summary>
        ///     Delete Properties And Sync it With EachOthers
        /// </summary>
        /// <param name="key">(NOTNULL) The Key Value</param>
        /// <param name="type"> The Type of Property </param>
        public async Task RemoveProperty(PropertyType type,string key)
        {
            if (GameService.IsGuest) throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GSLiveTB>(DebugLocation.TurnBased,"RemoveProperty");
            if (GSLive.Handler.TurnBasedHandler == null) throw new GameServiceException("You Must Create or Join Room First").LogException<GSLiveTB>(DebugLocation.TurnBased,"RemoveProperty");
          
            if (string.IsNullOrEmpty(key)) throw new GameServiceException("Key Cant Be EmptyOrNull").LogException<GSLiveTB>(DebugLocation.TurnBased,"CreateRoom");

            await GSLive.Handler.TurnBasedHandler.RequestAsync(PropertyHandler.Signature,
                new DataPayload {Action = type == PropertyType.Member ? InternalPropertyAction.MemberDelete : InternalPropertyAction.RoomDelete,Id = key});
        }

        
        /// <summary>
        ///     Get All Properties
        /// </summary>
        public async Task GetProperties()
        {
            if (GameService.IsGuest) throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GSLiveTB>(DebugLocation.TurnBased,"GetProperties");
            if (GSLive.Handler.TurnBasedHandler == null) throw new GameServiceException("You Must Create or Join Room First").LogException<GSLiveTB>(DebugLocation.TurnBased,"GetProperties");
            
            await GSLive.Handler.TurnBasedHandler.RequestAsync(SnapshotHandler.Signature);
        }
        
        
        
        /// <summary>
        ///     Get Current Room Info
        /// </summary>
        public async Task GetCurrentRoomInfo()
        {
            if (GameService.IsGuest) throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GSLiveTB>(DebugLocation.TurnBased,"GetCurrentRoomInfo");
            if (GSLive.Handler.TurnBasedHandler == null) throw new GameServiceException("You Must Create or Join Room First").LogException<GSLiveTB>(DebugLocation.TurnBased,"GetCurrentRoomInfo");
            
            await GSLive.Handler.TurnBasedHandler.RequestAsync(RoomInfoHandler.Signature);
        }


        /// <summary>
        ///     Leave The Current Room , if whoIsNext Set Null , Server Automatically Selects Next Turn
        /// </summary>
        /// <param name="whoIsNext">(NULLABLE)(Type : Member's ID) Player's id You Want To Select Next Turn</param>
        public async Task LeaveRoom(string whoIsNext = null)
        {
            if (GameService.IsGuest) throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GSLiveTB>(DebugLocation.TurnBased,"LeaveRoom");
            if (GSLive.Handler.TurnBasedHandler == null) throw new GameServiceException("You Must Create or Join Room First").LogException<GSLiveTB>(DebugLocation.TurnBased,"LeaveRoom");
            
            await GSLive.Handler.TurnBasedHandler?.RequestAsync(LeaveRoomHandler.Signature, new DataPayload {NextId = whoIsNext});
            GSLive.Handler.TurnBasedHandler?.Dispose();
        }
        
        
        /// <summary>
        ///     If you want to announce the end of the game, use this function to send the result of your game to other players.
        /// </summary>
        /// <param name="outcomes">(NOTNULL) A set of players and their results</param>
        public async Task Vote(Dictionary<string, Outcome> outcomes)
        {
            if (GameService.IsGuest) throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GSLiveTB>(DebugLocation.TurnBased,"Finish");
            if (outcomes == null) throw new GameServiceException("outcomes Cant Be Null").LogException<GSLiveTB>(DebugLocation.TurnBased,"Finish");
            if (GSLive.Handler.TurnBasedHandler == null) throw new GameServiceException("You Must Create or Join Room First").LogException<GSLiveTB>(DebugLocation.TurnBased,"Finish");
           
            await GSLive.Handler.TurnBasedHandler.RequestAsync(VoteHandler.Signature,
                new DataPayload {Outcomes = outcomes});
        }


        /// <summary>
        ///     If you would like to confirm one of the results posted by other Players
        /// </summary>
        /// <param name="memberId">(NOTNULL)The Specific player ID</param>
        public async Task Complete(string memberId)
        {
            if (GameService.IsGuest) throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GSLiveTB>(DebugLocation.TurnBased,"Complete");
            if (string.IsNullOrEmpty(memberId)) throw new GameServiceException("memberId Cant Be EmptyOrNull").LogException<GSLiveTB>(DebugLocation.TurnBased,"Complete");
            if (GSLive.Handler.TurnBasedHandler == null) throw new GameServiceException("You Must Create or Join Room First").LogException<GSLiveTB>(DebugLocation.TurnBased,"Complete");
          
            await GSLive.Handler.TurnBasedHandler.RequestAsync(CompleteHandler.Signature,
                new DataPayload {Id = memberId});
        }


        /// <summary>
        ///     Get Room Members Details
        /// </summary>
        public async Task GetRoomMembersDetail()
        {
            if (GameService.IsGuest) throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GSLiveTB>(DebugLocation.TurnBased,"GetRoomMembersDetail");
            if (GSLive.Handler.TurnBasedHandler == null) throw new GameServiceException("You Must Create or Join Room First").LogException<GSLiveTB>(DebugLocation.TurnBased,"GetRoomMembersDetail");
           
            await GSLive.Handler.TurnBasedHandler.RequestAsync(GetMemberHandler.Signature);
        }


        /// <summary>
        ///     Get Current Turn Member Details
        /// </summary>
        public async Task GetCurrentTurnMember()
        {
            if (GameService.IsGuest) throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GSLiveTB>(DebugLocation.TurnBased,"GetCurrentTurnMember");
            if (GSLive.Handler.TurnBasedHandler == null) throw new GameServiceException("You Must Create or Join Room First").LogException<GSLiveTB>(DebugLocation.TurnBased,"GetCurrentTurnMember");
         
            await GSLive.Handler.TurnBasedHandler.RequestAsync(CurrentTurnHandler.Signature);
        }


        /// <summary>
        ///     Get Your Invite Inbox
        /// </summary>
        public async Task GetInviteInbox()
        {
            if (GameService.IsGuest) throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GSLiveTB>(DebugLocation.TurnBased,"GetInviteInbox");
            await GSLive.Handler.CommandHandler.RequestAsync(InviteListHandler.Signature,
                new RoomDetail {GsLiveType = (int) GSLiveType.TurnBased});
        }


        /// <summary>
        ///     Invite a Specific Player To Specific Room
        /// </summary>
        /// <param name="roomId">(NOTNULL) (Type : RoomID)Room's ID</param>
        /// <param name="userId">(NOTNULL) (Type : UserID)User's ID</param>
        public async Task InviteUser(string roomId, string userId)
        {
            if (GameService.IsGuest) throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GSLiveTB>(DebugLocation.TurnBased,"InviteUser");
            if (string.IsNullOrEmpty(roomId) && string.IsNullOrEmpty(userId)) throw new GameServiceException("roomId Or userId Cant Be EmptyOrNull").LogException<GSLiveTB>(DebugLocation.TurnBased,"InviteUser");
           
            await GSLive.Handler.CommandHandler.RequestAsync(InviteUserHandler.Signature,
                new RoomDetail {UserOrMemberId = userId, Id = roomId, GsLiveType = (int) GSLiveType.TurnBased});
        }


        /// <summary>
        ///     Accept a Specific Invite With Invite ID
        ///     Note: After accepting the invitation, you will be automatically entered into the game room
        /// </summary>
        /// <param name="inviteId">(NOTNULL) (Type : InviteID) Invite's ID</param>
        /// <param name="extra">Specifies the Extra Data To Send to Other Clients</param>
        public async Task AcceptInvite(string inviteId, string extra = null)
        {
            if (GameService.IsGuest) throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GSLiveTB>(DebugLocation.TurnBased,"AcceptInvite");
            if (string.IsNullOrEmpty(inviteId)) throw new GameServiceException("inviteId Cant Be EmptyOrNull").LogException<GSLiveTB>(DebugLocation.TurnBased,"AcceptInvite");
            
            await GSLive.Handler.CommandHandler.RequestAsync(AcceptInviteHandler.Signature,
                new RoomDetail {Invite = inviteId, Extra = extra, GsLiveType = (int) GSLiveType.RealTime});
        }

        /// <summary>
        ///     Find All Member With Specific Query
        /// </summary>
        /// <param name="query">(NOTNULL) Query </param>
        /// <param name="limit">(Max = 15) The Result Limits</param>
        public async Task FindMember(string query, int limit = 10)
        {
            if (GameService.IsGuest) throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GSLiveTB>(DebugLocation.TurnBased,"FindMember");
            if (string.IsNullOrEmpty(query)) throw new GameServiceException("query Cant Be EmptyOrNull").LogException<GSLiveTB>(DebugLocation.TurnBased,"FindMember");
            if (limit < 0 || limit > 15) throw new GameServiceException("invalid Limit Value").LogException<GSLiveTB>(DebugLocation.TurnBased,"FindMember");
         
            await GSLive.Handler.CommandHandler.RequestAsync(FindMemberHandler.Signature,
                new RoomDetail {Max = limit, UserOrMemberId = query});
        }
    }
}