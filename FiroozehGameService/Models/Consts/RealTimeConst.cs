// <copyright file="RealTimeConst.cs" company="Firoozeh Technology LTD">
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
    internal static class RealTimeConst
    {
        // Packet Actions
        internal const int ActionAuth = 1;
        internal const int ActionPublicMessage = 3;
        internal const int ActionPrivateMessage = 4;
        internal const int ActionJoin = 5;
        internal const int ActionMembersDetail = 6;
        internal const int ActionLeave = 7;
        internal const int ActionEvent = 11;
        internal const int ActionSnapShot = 12;
        internal const int ActionObserver = 13;
        internal const int ActionRoomInfo = 14;

        internal const int Error = 100;


        internal const int MaxPacketSize = 1 * 1024;
        internal const int MaxPacketBeforeSize = 8 * 1024;

        // Limit Checker
        internal const int MinObserverThreshold = 8; // 8 Request per sec
        internal const int MaxObserverThreshold = 12; // 12 Request per sec

        internal const int RestLimit = 1000; //  RestLimit per sec in long
        internal const int RealTimeSendLimit = 15;
        internal const int DataGetter = 250; //  Data like rtt and packet lost

        internal const int MinPlayer = 2;
        internal const int MaxPlayer = 50;
    }
}