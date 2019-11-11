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
        
        /*
        private static void SetEventListener(IEventListener listener)
        {
           var rt = GSLiveProvider.GetGSLiveRT();
           rt.Call("SetListener", listener);
        }

        /// <summary>
        /// Set Listener For Receive GSLive RealTime System Events.
        /// </summary>
        /// <param name="realTimeListener">(NOTNULL)Listener For Receive GSLive RealTime System Events</param>
        public void SetListener(GSLiveRealTimeListener realTimeListener)
        {            
            if (realTimeListener != null)
            {
                _realTimeListener = realTimeListener;
                var eventListener = new IEventListener((type, payload) =>
                {
                    switch ((EventType) type)
                    {
                        case EventType.JoinRoom:
                            var join = JsonConvert.DeserializeObject<JoinData>(payload);
                            _realTimeListener.OnJoin(join,(JoinType)join.JoinType);
                            break;
                        case EventType.LeaveRoom:
                            var leave = JsonConvert.DeserializeObject<Message>(payload);
                            _realTimeListener.OnLeave(new Leave {RoomId = leave.RoomId, MemberLeave = JsonConvert.DeserializeObject<Member>(leave.Data)});
                            break;
                        case EventType.PublicMessageReceive:
                            _realTimeListener.OnMessageReceive(JsonConvert.DeserializeObject<Message>(payload),MessageType.Public);
                            break;
                        case EventType.PrivateMessageReceive:
                            _realTimeListener.OnMessageReceive(JsonConvert.DeserializeObject<Message>(payload),MessageType.Private);
                            break;
                        case EventType.AvailableRoom:
                            _realTimeListener.OnAvailableRooms(JsonConvert.DeserializeObject<List<Room>>(payload));
                            break;
                        case EventType.MemberForAutoMatch:
                            _realTimeListener.OnAutoMatchUpdate(AutoMatchStatus.OnUserJoined,JsonConvert.DeserializeObject<List<User>>(payload));
                            break;
                        case EventType.AutoMatchWaiting:
                            _realTimeListener.OnAutoMatchUpdate(AutoMatchStatus.OnWaiting,JsonConvert.DeserializeObject<List<User>>(payload));
                            break;
                        case EventType.MembersDetail:
                            var details = JsonConvert.DeserializeObject<Message>(payload);
                            _realTimeListener.OnRoomMembersDetail(JsonConvert.DeserializeObject<List<Member>>(details.Data));
                            break; 
                        case EventType.ActionGetInviteList:
                            _realTimeListener.OnInviteInbox(JsonConvert.DeserializeObject<List<Invite>>(payload));
                            break; 
                        case EventType.ActionInviteUser:
                            _realTimeListener.OnInviteSend();
                            break; 
                        case EventType.ActionFindUser:
                            _realTimeListener.OnFindUsers(JsonConvert.DeserializeObject<List<User>>(payload));
                            break; 
                        case EventType.ActionInviteReceive:
                            _realTimeListener.OnInviteReceive(JsonConvert.DeserializeObject<Invite>(payload));
                            break; 
                        case EventType.Success:
                            _realTimeListener.OnSuccess();
                            break;   
                        default:
                            break;
                    }

                }, _realTimeListener.OnRealTimeError);

                IsAvailable = true;
                SetEventListener(eventListener);
            }
            else
            {
                LogUtil.LogError(Tag,"Listener Must not be NULL");
            }
        }
        */

        
        /// <summary>
        /// Create Room With Option Like : Name , Min , Max , Role , IsPrivate
        /// </summary>
        /// <param name="option">(NOTNULL)Create Room Option</param>
        public void CreateRoom(GSLiveOption.CreateRoomOption option)
        {
                     
        }
        
        
        /// <summary>
        /// Create AutoMatch With Option Like :  Min , Max , Role 
        /// </summary>
        /// <param name="option">(NOTNULL)AutoMatch Option</param>
        public void AutoMatch(GSLiveOption.AutoMatchOption option)
        {
            
        }
        
        
        /// <summary>
        /// Join In Room With RoomID
        /// </summary>
        /// <param name="roomId">(NOTNULL)Room's id You Want To Join</param>
        public void JoinRoom(string roomId)
        {
           
        }
        
        /// <summary>
        /// Leave The Current Room
        /// </summary>
        public void LeaveRoom()
        {
           
        }

        
        /// <summary>
        /// Get Available Rooms According To Room's Role  
        /// </summary>
        /// <param name="role">(NOTNULL)Room's Role </param>
        public void GetAvailableRooms(string role)
        {
           
        }
        
       
        /// <summary>
        /// Send A Data To All Players in Room. 
        /// </summary>
        /// <param name="data">(NOTNULL) Data To BroadCast </param>
        public void SendPublicMessage(string data)
        {
             
        }    
        
        
        /// <summary>
        /// Send A Data To Specific Player in Room. 
        /// </summary>
        /// <param name="receiverId">(NOTNULL) (Type : MemberID)Player's ID</param>
        /// <param name="data">(NOTNULL) Data for Send</param>
        public void SendPrivateMessage(string receiverId,string data)
        {
           
        }    
       
        
        /// <summary>
        /// Get Room Members Details 
        /// </summary>
        public void GetRoomMembersDetail()
        {
             
        }
        
        
        /// <summary>
        /// Get Your Invite Inbox
        /// </summary>
        public void GetInviteInbox()
        {
           
        }
        
        
        /// <summary>
        /// Invite a Specific Player To Specific Room
        /// </summary>
        /// <param name="roomId">(NOTNULL) (Type : RoomID)Room's ID</param>
        /// <param name="userId">(NOTNULL) (Type : UserID)User's ID</param>
        public void InviteUser(string roomId,string userId)
        {
           
        }
        
        
        /// <summary>
        /// Accept a Specific Invite With Invite ID
        /// Note: After accepting the invitation, you will be automatically entered into the game room
        /// </summary>
        /// <param name="inviteId">(NOTNULL) (Type : InviteID) Invite's ID</param>
        public void AcceptInvite(string inviteId)
        {
           
        }
        
        /// <summary>
        /// Find All Users With Specific NickName
        /// </summary>
        /// <param name="query">(NOTNULL) Player's NickName</param>
        /// <param name="limit">(Max = 15) The Result Limits</param>
        public void FindUser(string query,int limit)
        {
           
        }
        
    }
}