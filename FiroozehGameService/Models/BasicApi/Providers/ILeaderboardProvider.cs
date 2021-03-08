// <copyright file="ILeaderboardProvider.cs" company="Firoozeh Technology LTD">
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
    ///     Represents Leaderboard Provider Model In Game Service Basic API
    /// </summary>
    public interface ILeaderboardProvider
    {
        /// <summary>
        ///     With this command you can get  list of all your game LeaderBoard
        ///     that you have registered in the Developer panel
        /// </summary>
        /// <value> GetLeaderBoards List </value>
        Task<List<LeaderBoard>> GetLeaderBoards();


        /// <summary>
        ///     This command allows you to Submit Player Score with the ID of the leaderBoard
        ///     you have Registered in the Developer panel
        /// </summary>
        /// <param name="leaderBoardId">(NOTNULL)leaderBoardId</param>
        /// <param name="scoreValue">scoreValue(The value must not exceed the maximum value Registered in the Developer Panel)</param>
        /// <value> return SubmitScore </value>
        Task<SubmitScoreResponse> SubmitScore(string leaderBoardId, int scoreValue);


        /// <summary>
        ///     With this command you can get a LeaderBoardDetails with the ID of the LeaderBoard list
        ///     you registered in the Developer panel.
        /// </summary>
        /// <param name="leaderBoardId">(NOTNULL)The ID of leaderBoard you Want To get Detail</param>
        /// <param name="scoreLimit">(Min = 10,Max = 50) The Score List Limits</param>
        /// <param name="onlyFriends"> if this option Enabled , returns the Friends Score </param>
        /// <value> return LeaderBoardDetails </value>
        Task<LeaderBoardDetails> GetLeaderBoardDetails(string leaderBoardId, int scoreLimit = 10,
            bool onlyFriends = false);


        /// <summary>
        ///     With this command you can get Current Player Score with the ID of the LeaderBoard id
        ///     you registered in the Developer panel.
        /// </summary>
        /// <param name="leaderBoardId">(NOTNULL)The ID of leaderBoard you Want To get Score</param>
        /// <value> return Score </value>
        Task<Score> GetCurrentPlayerScore(string leaderBoardId);
    }
}