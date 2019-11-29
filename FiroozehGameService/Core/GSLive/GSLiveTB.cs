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


using System.Collections.Generic;
using System.Threading.Tasks;
using FiroozehGameService.Handlers.Command.RequestHandlers;
using FiroozehGameService.Handlers.TurnBased.RequestHandlers;
using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.Enums.GSLive;
using FiroozehGameService.Models.GSLive.TB;

namespace FiroozehGameService.Core.GSLive
{
    /// <summary>
    /// Represents Game Service TurnBased MultiPlayer System
    /// </summary>
    public class GSLiveTB
    {
        private const string Tag = "GSLive-TurnBased";
        
        /// <summary>
        /// Create Room With Option Like : Name , Min , Max , Role , IsPrivate
        /// </summary>
        /// <param name="option">(NOTNULL)Create Room Option</param>
        public async Task CreateRoom(GSLiveOption.CreateRoomOption option)
        {
            option.GsLiveType = GSLiveType.TurnBased;
            await GSLive.Handler.CommandHandler.Request(CreateRoomHandler.Signature,option);     
        }
        
        
        /// <summary>
        /// Create AutoMatch With Option Like :  Min , Max , Role 
        /// </summary>
        /// <param name="option">(NOTNULL)AutoMatch Option</param>
        public async Task AutoMatch(GSLiveOption.AutoMatchOption option)
        {
            option.GsLiveType = GSLiveType.TurnBased;
            await GSLive.Handler.CommandHandler.Request(AutoMatchHandler.Signature,option);     
        }
        
        
        /// <summary>
        /// Join In Room With RoomID
        /// </summary>
        /// <param name="roomId">(NOTNULL)Room's id You Want To Join</param>
        public async Task JoinRoom(string roomId)
        {
            await GSLive.Handler.CommandHandler.Request(JoinRoomHandler.Signature,new RoomDetail{Id = roomId});     
        }
        
        
        /// <summary>
        /// Get Available Rooms According To Room's Role  
        /// </summary>
        /// <param name="role">(NOTNULL)Room's Role </param>
        public async Task GetAvailableRooms(string role)
        {
            await GSLive.Handler.CommandHandler.Request(GetRoomsHandler.Signature,new RoomDetail{Role = role});     
        }

       
        /// <summary>
        /// If is your Turn, you can send data to other players using this function.
        /// Also if You Want to Move Your Turn to the Next player
        /// put the next player ID in the function entry
        /// You can use this function several times
        /// </summary>
        /// <param name="data">(NULLABLE)Room's Role </param>
        /// <param name="whoIsNext">(NULLABLE) Next Player's ID </param>
        public async Task TakeTurn(string data , string whoIsNext)
        {
            await GSLive.Handler.TurnBasedHandler.Request(TakeTurnHandler.Signature,new DataPayload{Data = data,NextId = whoIsNext});     
        }
        
        
        
        /// <summary>
        /// If it's your turn, you can transfer the turn to the next player without sending data
        /// if whoIsNext Set Null , Server Automatically Selects Next Turn 
        /// </summary>
        /// <param name="whoIsNext">(NULLABLE)Next Player's ID </param>
        public async Task ChooseNext(string whoIsNext)
        {
           await GSLive.Handler.TurnBasedHandler.Request(ChooseNextHandler.Signature,new DataPayload{NextId = whoIsNext});     
        }


        /// <summary>
        /// Leave The Current Room , if whoIsNext Set Null , Server Automatically Selects Next Turn 
        /// </summary>
        /// <param name="whoIsNext">(NULLABLE)(Type : Member's ID) Player's id You Want To Select Next Turn</param>
        public async Task LeaveRoom(string whoIsNext)
        {
           await GSLive.Handler.TurnBasedHandler.Request(LeaveRoomHandler.Signature,new DataPayload{NextId = whoIsNext});     
           GSLive.Handler.TurnBasedHandler.Dispose();
        }


        /// <summary>
        /// If you want to announce the end of the game, use this function to send the result of your game to other players.
        /// </summary>
        /// <param name="outcomes">(NOTNULL) A set of players and their results</param>
        public async Task Finish(Dictionary <string,Outcome> outcomes)
        {
           await GSLive.Handler.TurnBasedHandler.Request(FinishHandler.Signature,new DataPayload{Outcomes = outcomes});     
        }
        
        
        /// <summary>
        /// If you would like to confirm one of the results posted by other Players
        /// </summary>
        /// <param name="memberId">(NOTNULL)The Specific player ID</param>
        public async Task Complete(string memberId)
        {
           await GSLive.Handler.TurnBasedHandler.Request(FinishHandler.Signature,new DataPayload{Id = memberId});     
        }
        
        
        /// <summary>
        /// Get Room Members Details 
        /// </summary>
        public async Task GetRoomMembersDetail()
        {
           await GSLive.Handler.TurnBasedHandler.Request(GetMemberHandler.Signature);     
        }
        
        
        /// <summary>
        /// Get Your Invite Inbox
        /// </summary>
        public async Task GetInviteInbox()
        {
            await GSLive.Handler.CommandHandler.Request(InviteListHandler.Signature,null);     
        }
        
        
        /// <summary>
        /// Invite a Specific Player To Specific Room
        /// </summary>
        /// <param name="roomId">(NOTNULL) (Type : RoomID)Room's ID</param>
        /// <param name="userId">(NOTNULL) (Type : UserID)User's ID</param>
        public async Task InviteUser(string roomId,string userId)
        {
            await GSLive.Handler.CommandHandler.Request(InviteUserHandler.Signature,new RoomDetail{User = userId , Id = roomId});     

        }
        
        
        /// <summary>
        /// Accept a Specific Invite With Invite ID
        /// Note: After accepting the invitation, you will be automatically entered into the game room
        /// </summary>
        /// <param name="inviteId">(NOTNULL) (Type : InviteID) Invite's ID</param>
        public async Task AcceptInvite(string inviteId)
        {
            await GSLive.Handler.CommandHandler.Request(AcceptInviteHandler.Signature,new RoomDetail{Invite = inviteId});     
        }
        
        
        /// <summary>
        /// Find All Users With Specific NickName
        /// </summary>
        /// <param name="query">(NOTNULL) Player's NickName</param>
        /// <param name="limit">(Max = 15) The Result Limits</param>
        public async Task FindUser(string query,int limit)
        {
            await GSLive.Handler.CommandHandler.Request(FindUserHandler.Signature,new RoomDetail{Max = limit , User = query});     
        }
   
    }
}