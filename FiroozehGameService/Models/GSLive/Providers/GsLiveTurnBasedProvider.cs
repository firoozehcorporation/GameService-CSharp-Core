// <copyright file="GsLiveTurnBasedProvider.cs" company="Firoozeh Technology LTD">
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


using System.Collections.Generic;
using System.Threading.Tasks;
using FiroozehGameService.Core.GSLive;
using FiroozehGameService.Models.Enums.GSLive.TB;
using FiroozehGameService.Models.GSLive.TB;

namespace FiroozehGameService.Models.GSLive.Providers
{
    /// <summary>
    ///     Represents Game Service TurnBased MultiPlayer System
    /// </summary>
    public abstract class GsLiveTurnBasedProvider
    {
        /// <summary>
        ///     Create Room With Option Like : Name , Min , Max , Role , IsPrivate
        /// </summary>
        /// <param name="option">(NOTNULL)Create Room Option</param>
        public abstract Task CreateRoom(GSLiveOption.CreateRoomOption option);


        /// <summary>
        ///     Create AutoMatch With Option Like :  Min , Max , Role
        /// </summary>
        /// <param name="option">(NOTNULL)AutoMatch Option</param>
        public abstract Task AutoMatch(GSLiveOption.AutoMatchOption option);


        /// <summary>
        ///     Cancel Current AutoMatch
        /// </summary>
        public abstract Task CancelAutoMatch();


        /// <summary>
        ///     Join In Room With RoomID
        /// </summary>
        /// <param name="roomId">(NOTNULL)Room's id You Want To Join</param>
        /// <param name="extra">(NULLABLE)Specifies the Extra Data To Send to Other Clients</param>
        /// <param name="password">(NULLABLE)Specifies the Password if Room is Private</param>
        public abstract Task JoinRoom(string roomId, string extra = null, string password = null);


        /// <summary>
        ///     Get Available Rooms According To Room's Role
        /// </summary>
        /// <param name="role">(NOTNULL)Room's Role </param>
        public abstract Task GetAvailableRooms(string role);


        /// <summary>
        ///     If is your Turn, you can send data to other players using this function.
        ///     Also if You Want to Move Your Turn to the Next player
        ///     put the next player ID in the function entry
        ///     You can use this function several times
        /// </summary>
        /// <param name="data">(NULLABLE) Data to Send </param>
        /// <param name="whoIsNext">(NULLABLE) Next Player's ID </param>
        public abstract Task TakeTurn(string data = null, string whoIsNext = null);


        /// <summary>
        ///     If it's your turn, you can transfer the turn to the next player without sending data
        ///     if whoIsNext Set Null , Server Automatically Selects Next Turn
        /// </summary>
        /// <param name="whoIsNext">(NULLABLE)Next Player's ID </param>
        public abstract Task ChooseNext(string whoIsNext = null);


        /// <summary>
        ///     Every Member can Set Properties to Sync Data With EachOthers
        /// </summary>
        /// <param name="type">The Type of Property </param>
        /// <param name="propertyData">(NOTNULL) The property Data</param>
        public abstract Task SetOrUpdateProperty(PropertyType type, KeyValuePair<string, string> propertyData);


        /// <summary>
        ///     Delete Properties And Sync it With EachOthers
        /// </summary>
        /// <param name="key">(NOTNULL) The Key Value</param>
        /// <param name="type"> The Type of Property </param>
        public abstract Task RemoveProperty(PropertyType type, string key);


        /// <summary>
        ///     Get All Member Properties
        /// </summary>
        public abstract Task GetMemberProperties();


        /// <summary>
        ///     Get Current Room Info
        /// </summary>
        public abstract Task GetCurrentRoomInfo();


        /// <summary>
        ///     Leave The Current Room , if whoIsNext Set Null , Server Automatically Selects Next Turn
        /// </summary>
        /// <param name="whoIsNext">(NULLABLE)(Type : Member's ID) Player's id You Want To Select Next Turn</param>
        public abstract Task LeaveRoom(string whoIsNext = null);


        /// <summary>
        ///     If you want to announce the end of the game, use this function to send the result of your game to other players.
        /// </summary>
        /// <param name="outcomes">(NOTNULL) A set of players and their results</param>
        public abstract Task Vote(Dictionary<string, Outcome> outcomes);


        /// <summary>
        ///     If you would like to confirm one of the results posted by other Players
        /// </summary>
        /// <param name="memberId">(NOTNULL)The Specific player ID</param>
        public abstract Task AcceptVote(string memberId);


        /// <summary>
        ///     Get Room Members Details
        /// </summary>
        public abstract Task GetRoomMembersDetail();


        /// <summary>
        ///     Get Current Turn Member Details
        /// </summary>
        public abstract Task GetCurrentTurnMember();


        /// <summary>
        ///     Get Your Invite Inbox
        /// </summary>
        public abstract Task GetInviteInbox();


        /// <summary>
        ///     Invite a Specific Player To Specific Room
        /// </summary>
        /// <param name="roomId">(NOTNULL) (Type : RoomID)Room's ID</param>
        /// <param name="userId">(NOTNULL) (Type : UserID)User's ID</param>
        public abstract Task InviteUser(string roomId, string userId);


        /// <summary>
        ///     Accept a Specific Invite With Invite ID
        ///     Note: After accepting the invitation, you will be automatically entered into the game room
        /// </summary>
        /// <param name="inviteId">(NOTNULL) (Type : InviteID) Invite's ID</param>
        /// <param name="extra">Specifies the Extra Data To Send to Other Clients</param>
        public abstract Task AcceptInvite(string inviteId, string extra = null);


        /// <summary>
        ///     Find All Member With Specific Query
        /// </summary>
        /// <param name="query">(NOTNULL) Query </param>
        /// <param name="limit">(Max = 15) The Result Limits</param>
        public abstract Task FindMember(string query, int limit = 10);
    }
}