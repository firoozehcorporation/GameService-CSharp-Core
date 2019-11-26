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


using FiroozehGameService.Handlers.CommandServer_RequestHandlers;
using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.Interfaces;

namespace FiroozehGameService.Core.GSLive
{
    /// <summary>
    /// Represents Game Service Realtime MultiPlayer System
    /// </summary>
    public class GSLiveRT
    {
        private const string Tag = "GSLive-RealTime";
        //private GSLiveRealTimeListener _realTimeListener;
        public bool IsAvailable { get; private set; }

        public GSLiveRT()
        {
            
        }
        
      
        private static void AddEventListener(GSLiveRealTimeListener listener)
        {
        }

        /// <summary>
        /// Create Room With Option Like : Name , Min , Max , Role , IsPrivate
        /// </summary>
        /// <param name="option">(NOTNULL)Create Room Option</param>
        public void CreateRoom(GSLiveOption.CreateRoomOption option)
        {
            GSLive.Handler.CommandHandler.Request(CreateRoomHandler.Signature,option);     
        }
        
        
        /// <summary>
        /// Create AutoMatch With Option Like :  Min , Max , Role 
        /// </summary>
        /// <param name="option">(NOTNULL)AutoMatch Option</param>
        public void AutoMatch(GSLiveOption.AutoMatchOption option)
        {
            GSLive.Handler.CommandHandler.Request(AutoMatchHandler.Signature,option);     
        }
        
        
        /// <summary>
        /// Join In Room With RoomID
        /// </summary>
        /// <param name="roomId">(NOTNULL)Room's id You Want To Join</param>
        public void JoinRoom(string roomId)
        {
            GSLive.Handler.CommandHandler.Request(JoinRoomHandler.Signature,roomId);     
        }
        
        /// <summary>
        /// Leave The Current Room
        /// </summary>
        public void LeaveRoom()
        {
            // TODO Implement Rooms Functions
           // GSLive.Handler._commandHandler.Request(Leav.Signature,null);     
        }

        
        /// <summary>
        /// Get Available Rooms According To Room's Role  
        /// </summary>
        /// <param name="role">(NOTNULL)Room's Role </param>
        public void GetAvailableRooms(string role)
        {
            GSLive.Handler.CommandHandler.Request(GetRoomsHandler.Signature,role);     
        }
        
       
        /// <summary>
        /// Send A Data To All Players in Room. 
        /// </summary>
        /// <param name="data">(NOTNULL) Data To BroadCast </param>
        public void SendPublicMessage(string data)
        {
            // TODO Implement Rooms Functions
        }    
        
        
        /// <summary>
        /// Send A Data To Specific Player in Room. 
        /// </summary>
        /// <param name="receiverId">(NOTNULL) (Type : MemberID)Player's ID</param>
        /// <param name="data">(NOTNULL) Data for Send</param>
        public void SendPrivateMessage(string receiverId,string data)
        {
            // TODO Implement Rooms Functions
        }    
       
        
        /// <summary>
        /// Get Room Members Details 
        /// </summary>
        public void GetRoomMembersDetail()
        {
            // TODO Implement Rooms Functions
        }
        
        
        /// <summary>
        /// Get Your Invite Inbox
        /// </summary>
        public void GetInviteInbox()
        {
            GSLive.Handler.CommandHandler.Request(InviteListHandler.Signature,null);     
        }
        
        
        /// <summary>
        /// Invite a Specific Player To Specific Room
        /// </summary>
        /// <param name="roomId">(NOTNULL) (Type : RoomID)Room's ID</param>
        /// <param name="userId">(NOTNULL) (Type : UserID)User's ID</param>
        public void InviteUser(string roomId,string userId)
        {
            GSLive.Handler.CommandHandler.Request(InviteUserHandler.Signature,new RoomDetail{User = userId , Id = roomId});     
        }
        
        
        /// <summary>
        /// Accept a Specific Invite With Invite ID
        /// Note: After accepting the invitation, you will be automatically entered into the game room
        /// </summary>
        /// <param name="inviteId">(NOTNULL) (Type : InviteID) Invite's ID</param>
        public void AcceptInvite(string inviteId)
        {
            GSLive.Handler.CommandHandler.Request(AcceptInviteHandler.Signature,inviteId);     

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