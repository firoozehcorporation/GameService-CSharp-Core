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
        private const string BaseUrl1 = "https://gamesservice.ir";
        internal const string BaseUrl2 = "https://api.gamesservice.ir";
        internal const string FaaS = "https://faas.gamesservice.ir/";
        private const string DBaaS = "https://dbaas.gamesservice.ir";


        internal const string LoginUser = BaseUrl2 + "/auth/app/login";
        internal const string LoginWithGoogle = BaseUrl2 + "/auth/g/callback";
        internal const string LoginWithPhoneNumber = BaseUrl2 + "/auth/phone";
        internal const string Start = BaseUrl2 + "/auth/start";
        internal const string GetLastLoginInfo = BaseUrl2 + "/auth/app/login/info";
        internal const string GetMemberData = BaseUrl2 + "/v1/member/";
        internal const string GetUserData = BaseUrl2 + "/v1/user/";
        internal const string ChangePassword = BaseUrl2 + "/v1/user/changepassword";


        internal const string SaveGame = BaseUrl2 + "/v1/savegame/";
        internal const string GetAchievements = BaseUrl2 + "/v1/achievement/";
        internal const string UnlockAchievements = BaseUrl2 + "/v2/achievement/";
        internal const string GetLeaderBoard = BaseUrl2 + "/v1/leaderboard/";
        internal const string UseLeaderBoard = BaseUrl2 + "/v2/leaderboard/";

        internal const string Table = DBaaS + "/v1/bucket/";
        internal const string TableNonPermission = DBaaS + "/bucket/";


        internal const string GetEvents = BaseUrl2 + "/v1/events/";

        internal const string GetCurrentGame = BaseUrl2 + "/v1/currentgame";


        internal const string Friends = BaseUrl2 + "/v1/friends/";
        internal const string GetMyFriends = BaseUrl2 + "/v1/friends/me";
        internal const string GetFriendRequests = BaseUrl2 + "/v1/friends/me/pending";


        internal const string Parties = BaseUrl2 + "/v1/parties/party/";
        internal const string GetAllParties = BaseUrl2 + "/v1/parties/party";
        internal const string GetMyParties = BaseUrl2 + "/v1/parties/me";
        internal const string PartyImage = BaseUrl1 + "/api/v1/party/";


        internal const string CurrentTime = BaseUrl2 + "/syncedtime/";

        internal const string Devices = BaseUrl2 + "/v1/devices";


        internal const string UserProfileLogo = BaseUrl1 + "/Application/image";
    }
}