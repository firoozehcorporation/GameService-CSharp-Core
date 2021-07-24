// <copyright file="TurnBasedConst.cs" company="Firoozeh Technology LTD">
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
    internal static class TurnBasedConst
    {
        // Packet Actions
        internal const int ActionAuth = 1;
        internal const int ActionPing = 2;
        internal const int OnTakeTurn = 4;
        internal const int OnChooseNext = 5;
        internal const int OnLeave = 6;
        internal const int OnVote = 7;
        internal const int OnAcceptVote = 8;
        internal const int GetMembers = 9;
        internal const int OnJoin = 11;
        internal const int OnCurrentTurnDetail = 12;
        internal const int OnProperty = 13;
        internal const int OnRoomInfo = 14;
        internal const int OnSnapshot = 15;

        internal const int ActionMirror = 99;
        internal const int Errors = 100;

        internal const int TurnBasedLimit = 5; // 5 Request per sec
        internal const int ConnectivityCheckInterval = 1500;
        internal const short MaxRetryConnect = 15;

        internal const int MinPlayer = 2;
        internal const int MaxPlayer = 10;
    }
}