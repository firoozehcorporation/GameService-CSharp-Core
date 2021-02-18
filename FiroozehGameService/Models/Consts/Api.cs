// <copyright file="Api.cs" company="Firoozeh Technology LTD">
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
    internal static class Api
    {
        public const string BaseUrl1 = "https://gamesservice.ir";
        public const string BaseUrl2 = "https://api.gamesservice.ir";
        public const string FaaS = "https://faas.gamesservice.ir/";
        private const string DBaaS = "https://dbaas.gamesservice.ir";


        public const string LoginUser = BaseUrl2 + "/auth/app/login";
        public const string LoginWithGoogle = BaseUrl2 + "/auth/g/callback";
        public const string LoginWithPhoneNumber = BaseUrl2 + "/auth/phone";
        public const string Start = BaseUrl2 + "/auth/start";
        public const string GetLastLoginInfo = BaseUrl2 + "/auth/app/login/info";
        public const string GetMemberData = BaseUrl2 + "/v1/member/";
        public const string GetUserData = BaseUrl2 + "/v1/user/";
        public const string ChangePassword = BaseUrl2 + "/v1/user/changepassword";


        public const string SaveGame = BaseUrl2 + "/v1/savegame/";
        public const string Achievements = BaseUrl2 + "/v1/achievement/";
        public const string LeaderBoard = BaseUrl2 + "/v1/leaderboard/";
        public const string Table = DBaaS + "/v1/bucket/";
        public const string TableNonPermission = DBaaS + "/bucket/";


        public const string GetEvents = BaseUrl2 + "/v1/events/";

        public const string GetCurrentGame = BaseUrl2 + "/v1/currentgame";


        public const string Friends = BaseUrl2 + "/v1/friends/";
        public const string GetMyFriends = BaseUrl2 + "/v1/friends/me";
        public const string GetFriendRequests = BaseUrl2 + "/v1/friends/me/pending";


        public const string Parties = BaseUrl2 + "/v1/parties/party/";
        public const string GetAllParties = BaseUrl2 + "/v1/parties/party";
        public const string GetMyParties = BaseUrl2 + "/v1/parties/me";
        public const string PartyImage = BaseUrl1 + "/api/v1/party/";


        public const string CurrentTime = BaseUrl2 + "/syncedtime/";

        public const string Devices = BaseUrl2 + "/v1/devices";


        public const string UserProfileLogo = BaseUrl1 + "/Application/image";
    }
}