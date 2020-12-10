// <copyright file="CommandConst.cs" company="Firoozeh Technology LTD">
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

namespace FiroozehGameService.Models.Consts
{
    internal static class CommandConst
    {
        // For Send
        internal const int ActionAuth = 0;
        internal const int ActionAutoMatch = 1;
        internal const int ActionCreateRoom = 2;
        internal const int ActionGetRooms = 3;
        internal const int ActionJoinRoom = 4;
        internal const int ActionPing = 5;
        internal const int ActionInviteUser = 6;
        internal const int ActionKickUser = 7;
        internal const int ActionGetInviteList = 8;
        internal const int ActionAcceptInvite = 9;
        internal const int ActionFindMember = 10;
        internal const int ActionNotification = 11;
        internal const int ActionOnInvite = 15;
        internal const int ActionCancelAutoMatch = 16;


        internal const int ActionSubscribe = 12;
        internal const int ActionPublicChat = 13;
        internal const int ActionUnSubscribe = 14;
        internal const int ActionGetChannelsSubscribed = 17;
        internal const int ActionPrivateChat = 18;
        internal const int ActionChatRoomDetails = 19;
        internal const int ActionGetLastChats = 20;
        internal const int ActionGetPendingChats = 21;
        
        internal const int ActionKeepAlive = byte.MaxValue;



        internal const int Error = 100;

        // Limit Checker
        internal const int TimeLimit = 5; // 10 Request per sec
        internal const int KeepAliveTime = 2500;
        internal const short MaxRetryConnect = 7;
    }
}