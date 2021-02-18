// <copyright file="IAchievementProvider.cs" company="Firoozeh Technology LTD">
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

namespace FiroozehGameService.Models.BasicApi.Providers
{
    /// <summary>
    ///     Represents Achievement Provider Model In Game Service Basic API
    /// </summary>
    public interface IAchievementProvider
    {
        /// <summary>
        ///     With this command you can get list of all your game achievements
        ///     that you have registered in the Developer panel.
        /// </summary>
        /// <value> GetAchievements List </value>
        Task<List<Achievement>> GetAchievements();


        /// <summary>
        ///     With this command you can Unlock achievement with the achievement ID
        ///     you registered in the Developer panel.
        /// </summary>
        /// <param name="achievementId">(Not NULL)The ID of Achievement you Want To Unlock it</param>
        /// <value> return unlocked Achievement </value>
        Task<Achievement> UnlockAchievement(string achievementId);
    }
}