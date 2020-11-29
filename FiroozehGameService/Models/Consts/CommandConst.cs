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
        public const int ActionAuth = 0;
        public const int ActionAutoMatch = 1;
        public const int ActionCreateRoom = 2;
        public const int ActionGetRooms = 3;
        public const int ActionJoinRoom = 4;
        public const int ActionPing = 5;
        public const int ActionInviteUser = 6;
        public const int ActionKickUser = 7;
        public const int ActionGetInviteList = 8;
        public const int ActionAcceptInvite = 9;
        public const int ActionFindMember = 10;
        public const int ActionNotification = 11;
        public const int ActionOnInvite = 15;
        public const int ActionCancelAutoMatch = 16;


        public const int ActionSubscribe = 12;
        public const int ActionPublicChat = 13;
        public const int ActionUnSubscribe = 14;
        public const int ActionGetChannelsSubscribed = 17;
        public const int ActionPrivateChat = 18;
        public const int ActionChatRoomDetails = 19;
        public const int ActionGetLastChats = 20;
        public const int ActionGetPendingChats = 21;


        public const int Error = 100;

        // Limit Checker
        internal const int TimeLimit = 5; // 10 Request per sec
        internal const short MaxRetryConnect = 7;
    }
}