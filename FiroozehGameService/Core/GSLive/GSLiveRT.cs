// <copyright file="GSLiveRT.cs" company="Firoozeh Technology LTD">
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


using System.Threading.Tasks;
using FiroozehGameService.Handlers;
using FiroozehGameService.Handlers.Command.RequestHandlers;
using FiroozehGameService.Handlers.RealTime.RequestHandlers;
using FiroozehGameService.Models;
using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.Enums.GSLive;
using FiroozehGameService.Models.GSLive.RT;

namespace FiroozehGameService.Core.GSLive
{
    /// <summary>
    /// Represents Game Service Realtime MultiPlayer System
    /// </summary>
    public class GSLiveRT
    {
        private const string Tag = "GSLive-RealTime";        
        public GSLiveRT() {}
        
        /// <summary>
        /// Create Room With Option Like : Name , Min , Max , Role , IsPrivate
        /// </summary>
        /// <param name="option">(NOTNULL)Create Room Option</param>
        public void CreateRoom(GSLiveOption.CreateRoomOption option)
        {
            option.GsLiveType = GSLiveType.RealTime;
            GSLive.Handler.CommandHandler.Request(CreateRoomHandler.Signature,option);     
        }
        
        
        /// <summary>
        /// Create AutoMatch With Option Like :  Min , Max , Role 
        /// </summary>
        /// <param name="option">(NOTNULL)AutoMatch Option</param>
        public void AutoMatch(GSLiveOption.AutoMatchOption option)
        {
            option.GsLiveType = GSLiveType.RealTime;
            GSLive.Handler.CommandHandler.Request(AutoMatchHandler.Signature,option);     
        }
        
        
        /// <summary>
        /// Join In Room With RoomID
        /// </summary>
        /// <param name="roomId">(NOTNULL)Room's id You Want To Join</param>
        public void JoinRoom(string roomId)
        {
           GSLive.Handler.CommandHandler.Request(JoinRoomHandler.Signature,new RoomDetail{Id = roomId});     
        }
        
        /// <summary>
        /// Leave The Current Room
        /// </summary>
        public void LeaveRoom()
        {
            if(GSLive.Handler.RealTimeHandler == null) throw new GameServiceException("You Must Create or Join Room First");
            GSLive.Handler.RealTimeHandler.Request(LeaveRoomHandler.Signature);     
            GSLive.Handler.RealTimeHandler.Dispose();
        }

        
        /// <summary>
        /// Get Available Rooms According To Room's Role  
        /// </summary>
        /// <param name="role">(NOTNULL)Room's Role </param>
        public void GetAvailableRooms(string role)
        {
            GSLive.Handler.CommandHandler.Request(GetRoomsHandler.Signature,new RoomDetail{Role = role , GsLiveType = (int) GSLiveType.RealTime});     
        }
        
       
        /// <summary>
        /// Send A Data To All Players in Room. 
        /// </summary>
        /// <param name="data">(NOTNULL) Data To BroadCast </param>
        public void SendPublicMessage(string data)
        {
            if(GSLive.Handler.RealTimeHandler == null) throw new GameServiceException("You Must Create or Join Room First");
            GSLive.Handler.RealTimeHandler.Request(SendPublicMessageHandler.Signature,new DataPayload{Payload = data});     
        }    
        
        
        /// <summary>
        /// Send A Data To Specific Player in Room. 
        /// </summary>
        /// <param name="receiverId">(NOTNULL) (Type : MemberID)Player's ID</param>
        /// <param name="data">(NOTNULL) Data for Send</param>
        public void SendPrivateMessage(string receiverId,string data)
        {
            if(GSLive.Handler.RealTimeHandler == null) throw new GameServiceException("You Must Create or Join Room First");
            GSLive.Handler.RealTimeHandler.Request(SendPrivateMessageHandler.Signature,new DataPayload{ReceiverId = receiverId,Payload = data});     
        }    
       
        
        /// <summary>
        /// Get Room Members Details 
        /// </summary>
        public void GetRoomMembersDetail()
        {
            if(GSLive.Handler.RealTimeHandler == null) throw new GameServiceException("You Must Create or Join Room First");
            GSLive.Handler.RealTimeHandler.Request(GetMemberHandler.Signature);     
        }
        
        
        /// <summary>
        /// Get Your Invite Inbox
        /// </summary>
        public void GetInviteInbox()
        {
            GSLive.Handler.CommandHandler.Request(InviteListHandler.Signature,new RoomDetail{GsLiveType = (int) GSLiveType.RealTime});     
        }
        
        
        /// <summary>
        /// Invite a Specific Player To Specific Room
        /// </summary>
        /// <param name="roomId">(NOTNULL) (Type : RoomID)Room's ID</param>
        /// <param name="userId">(NOTNULL) (Type : UserID)User's ID</param>
        public void InviteUser(string roomId,string userId)
        {
           GSLive.Handler.CommandHandler.Request(InviteUserHandler.Signature,new RoomDetail{User = userId , Id = roomId , GsLiveType = (int) GSLiveType.RealTime});     
        }
        
        
        /// <summary>
        /// Accept a Specific Invite With Invite ID
        /// Note: After accepting the invitation, you will be automatically entered into the game room
        /// </summary>
        /// <param name="inviteId">(NOTNULL) (Type : InviteID) Invite's ID</param>
        public void AcceptInvite(string inviteId)
        {
           GSLive.Handler.CommandHandler.Request(AcceptInviteHandler.Signature,new RoomDetail{Invite = inviteId , GsLiveType = (int) GSLiveType.RealTime});     
        }
        
        /// <summary>
        /// Find All Users With Specific NickName
        /// </summary>
        /// <param name="query">(NOTNULL) Player's NickName</param>
        /// <param name="limit">(Max = 15) The Result Limits</param>
        public void FindUser(string query,int limit)
        {
            GSLive.Handler.CommandHandler.Request(FindUserHandler.Signature,new RoomDetail{Max = limit , User = query});     
        }
        
    }
}