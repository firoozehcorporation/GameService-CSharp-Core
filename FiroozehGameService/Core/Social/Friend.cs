// <copyright file="Friends.cs" company="Firoozeh Technology LTD">
// Copyright (C) 2020 Firoozeh Technology LTD. All Rights Reserved.
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
using FiroozehGameService.Core.ApiWebRequest;
using FiroozehGameService.Models;
using FiroozehGameService.Models.BasicApi;
using FiroozehGameService.Models.BasicApi.Social;
using FiroozehGameService.Models.BasicApi.Social.Providers;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.GSLive;
using FiroozehGameService.Models.Internal;
using FiroozehGameService.Utils;

namespace FiroozehGameService.Core.Social
{
    internal class Friend : FriendProvider
    {
        public override async Task<Results<Member>> FindMembers(string query = null, int skip = 0, int limit = 25)
        {
            if (!GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException<Friend>(DebugLocation.Friend,
                    "FindMembers");
            return await ApiRequest.GetAllMembers(new QueryData(query, skip, limit).ToQueryString());
        }

        public override async Task<Results<FriendData>> GetMyFriends(int skip = 0, int limit = 25)
        {
            if (!GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException<Friend>(DebugLocation.Friend,
                    "GetMyFriends");
            return await ApiRequest.GetMyFriends(new QueryData(null, skip, limit).ToQueryString());
        }

        public override async Task<Results<FriendData>> GetFriendRequests(int skip = 0, int limit = 25)
        {
            if (!GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException<Friend>(DebugLocation.Friend,
                    "GetFriendRequests");
            return await ApiRequest.GetFriendPendingRequests(new QueryData(null, skip, limit).ToQueryString());
        }

        public override async Task<bool> SendFriendRequest(string memberId)
        {
            if (!GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException<Friend>(DebugLocation.Friend,
                    "SendFriendRequest");
            if (string.IsNullOrEmpty(memberId))
                throw new GameServiceException("memberId Cant Be EmptyOrNull").LogException<Friend>(
                    DebugLocation.Friend, "SendFriendRequest");
            return await ApiRequest.FriendRequest(memberId);
        }


        public override async Task<bool> AcceptFriendRequest(string memberId)
        {
            if (!GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException<Friend>(DebugLocation.Friend,
                    "AcceptFriendRequest");
            if (string.IsNullOrEmpty(memberId))
                throw new GameServiceException("memberId Cant Be EmptyOrNull").LogException<Friend>(
                    DebugLocation.Friend, "AcceptFriendRequest");
            return await ApiRequest.AcceptFriendRequest(memberId);
        }


        public override async Task<bool> DeleteFriend(string memberId)
        {
            if (!GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException<Friend>(DebugLocation.Friend,
                    "DeleteFriend");
            if (string.IsNullOrEmpty(memberId))
                throw new GameServiceException("memberId Cant Be EmptyOrNull").LogException<Friend>(
                    DebugLocation.Friend, "DeleteFriend");
            return await ApiRequest.DeleteFriend(memberId);
        }
    }
}