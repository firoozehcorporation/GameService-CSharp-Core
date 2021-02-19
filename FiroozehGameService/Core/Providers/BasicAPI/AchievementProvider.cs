// <copyright file="AchievementProvider.cs" company="Firoozeh Technology LTD">
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
using FiroozehGameService.Utils;

namespace FiroozehGameService.Core.Providers.BasicAPI
{
    /// <summary>
    ///     Represents Achievement Provider Model In Game Service Basic API
    /// </summary>
    internal class AchievementProvider : IAchievementProvider
    {
        public async Task<List<Achievement>> GetAchievements()
        {
            if (!GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException(typeof(AchievementProvider),
                    DebugLocation.Internal, "GetAchievements");
            return await ApiRequest.GetAchievements();
        }

        public async Task<Achievement> UnlockAchievement(string achievementId)
        {
            if (!GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException(typeof(AchievementProvider),
                    DebugLocation.Internal, "UnlockAchievement");
            if (string.IsNullOrEmpty(achievementId))
                throw new GameServiceException("AchievementId Cant Be EmptyOrNull").LogException(
                    typeof(AchievementProvider),
                    DebugLocation.Internal, "UnlockAchievement");
            return await ApiRequest.UnlockAchievement(achievementId);
        }
    }
}