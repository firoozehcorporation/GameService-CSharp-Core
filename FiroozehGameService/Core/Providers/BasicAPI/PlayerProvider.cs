// <copyright file="PlayerProvider.cs" company="Firoozeh Technology LTD">
// Copyright (C) 2021 Firoozeh Technology LTD. All Rights Reserved.
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

using System.Collections.Generic;
using System.Threading.Tasks;
using FiroozehGameService.Core.ApiWebRequest;
using FiroozehGameService.Models;
using FiroozehGameService.Models.BasicApi;
using FiroozehGameService.Models.BasicApi.Providers;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.GSLive;
using FiroozehGameService.Utils;

namespace FiroozehGameService.Core.Providers.BasicAPI
{
    /// <summary>
    ///     Represents Player Provider Model In Game Service Basic API
    /// </summary>
    internal class PlayerProvider : IPlayerProvider
    {
        public async Task<Member> GetCurrentPlayer()
        {
            if (!GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException(typeof(PlayerProvider),
                    DebugLocation.Internal, "GetCurrentPlayer");
            return await ApiRequest.GetCurrentPlayer();
        }

        public async Task<User> GetUserData(string userId)
        {
            if (!GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException(typeof(PlayerProvider),
                    DebugLocation.Internal, "GetUserData");
            if (string.IsNullOrEmpty(userId))
                throw new GameServiceException("userId Cant Be EmptyOrNull").LogException(typeof(PlayerProvider),
                    DebugLocation.Internal, "GetUserData");
            return await ApiRequest.GetUserData(userId);
        }

        public async Task<Member> GetMemberData(string memberId)
        {
            if (!GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException(typeof(PlayerProvider),
                    DebugLocation.Internal, "GetMemberData");
            if (string.IsNullOrEmpty(memberId))
                throw new GameServiceException("memberId Cant Be EmptyOrNull").LogException(typeof(PlayerProvider),
                    DebugLocation.Internal, "GetMemberData");
            return await ApiRequest.GetMemberData(memberId);
        }

        public async Task<MemberInfo> GetLastLoginMemberInfo()
        {
            if (!GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException(typeof(PlayerProvider),
                    DebugLocation.Internal, "GetLastLoginMemberInfo");
            return await ApiRequest.GetLastLoginMemberInfo();
        }

        public async Task<MemberInfo> EditCurrentPlayerProfile(EditUserProfile profile)
        {
            if (!GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException(typeof(PlayerProvider),
                    DebugLocation.Internal, "EditCurrentPlayerProfile");
            if (profile == null)
                throw new GameServiceException("profile Cant Be Null").LogException(typeof(PlayerProvider),
                    DebugLocation.Internal, "EditCurrentPlayerProfile");
            return await ApiRequest.EditCurrentPlayer(profile);
        }

        public async Task<MemberInfo> SetCurrentPlayerTags(List<string> tags)
        {
            if (!GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException(typeof(PlayerProvider),
                    DebugLocation.Internal, "SetCurrentPlayerTags");
            if (tags == null)
                throw new GameServiceException("tags Cant Be Null").LogException(typeof(PlayerProvider),
                    DebugLocation.Internal, "SetCurrentPlayerTags");

            return await ApiRequest.SetCurrentPlayerTags(tags);
        }

        public async Task<MemberInfo> SetCurrentPlayerLabel(string label)
        {
            if (!GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException(typeof(PlayerProvider),
                    DebugLocation.Internal, "SetCurrentPlayerLabel");
            if (string.IsNullOrEmpty(label))
                throw new GameServiceException("label Cant Be NullOrEmpty").LogException(typeof(PlayerProvider),
                    DebugLocation.Internal, "SetCurrentPlayerLabel");
            if (label.Length < 3 || label.Length > 21)
                throw new GameServiceException("Invalid label Length, label Length Must Between 3 and 21 Characters")
                    .LogException(
                        typeof(PlayerProvider),
                        DebugLocation.Internal, "SetCurrentPlayerLabel");

            return await ApiRequest.SetCurrentPlayerLabel(label);
        }

        public async Task<MemberInfo> SetCurrentPlayerGlobalProperty(string globalProperty)
        {
            if (!GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException(typeof(PlayerProvider),
                    DebugLocation.Internal, "SetCurrentPlayerGlobalProperty");
            if (string.IsNullOrEmpty(globalProperty))
                throw new GameServiceException("globalProperty Cant Be NullOrEmpty").LogException(
                    typeof(PlayerProvider),
                    DebugLocation.Internal, "SetCurrentPlayerGlobalProperty");
            if (globalProperty.Length > 2048)
                throw new GameServiceException("globalProperty is Too Large").LogException(
                    typeof(PlayerProvider),
                    DebugLocation.Internal, "SetCurrentPlayerGlobalProperty");

            return await ApiRequest.SetCurrentPlayerGlobalProperty(globalProperty);
        }

        public async Task<bool> ChangePassword(string currentPassword, string newPassword)
        {
            if (!GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException(typeof(PlayerProvider),
                    DebugLocation.Internal, "ChangePassword");
            if (string.IsNullOrEmpty(currentPassword))
                throw new GameServiceException("currentPassword Cant Be EmptyOrNull").LogException(
                    typeof(PlayerProvider),
                    DebugLocation.Internal, "ChangePassword");
            if (string.IsNullOrEmpty(newPassword))
                throw new GameServiceException("newPassword Cant Be EmptyOrNull").LogException(typeof(PlayerProvider),
                    DebugLocation.Internal, "ChangePassword");

            return await ApiRequest.ChangePassword(currentPassword, newPassword);
        }

        public async Task<List<ActiveDevice>> GetActiveDevices()
        {
            if (!GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException(typeof(PlayerProvider),
                    DebugLocation.Internal, "GetActiveDevices");
            return await ApiRequest.GetActiveDevices();
        }

        public async Task<bool> RevokeActiveDevice(string deviceId)
        {
            if (!GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException(typeof(PlayerProvider),
                    DebugLocation.Internal, "RevokeActiveDevice");
            if (string.IsNullOrEmpty(deviceId))
                throw new GameServiceException("deviceId Cant Be EmptyOrNull").LogException(typeof(PlayerProvider),
                    DebugLocation.Internal, "RevokeActiveDevice");
            return await ApiRequest.RevokeDevice(deviceId);
        }
    }
}