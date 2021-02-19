// <copyright file="FriendProvider.cs" company="Firoozeh Technology LTD">
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


using System.Threading.Tasks;
using FiroozehGameService.Models.GSLive;

namespace FiroozehGameService.Models.BasicApi.Social.Providers
{
    /// <summary>
    ///     Represents Friend in GameService Social System
    /// </summary>
    public abstract class FriendProvider
    {
        /// <summary>
        ///     Find All Member With Specific Member Name Query
        /// </summary>
        /// <param name="query">(NULLABLE) Member Name Query . NOTE : if set query null , return all Members</param>
        /// <param name="skip">The Result Skips</param>
        /// <param name="limit">(Max = 25) The Result Limits</param>
        public abstract Task<Results<Member>> FindMembers(string query = null, int skip = 0, int limit = 25);


        /// <summary>
        ///     Get Current Member Friends With Specific skip & limit
        /// </summary>
        /// <param name="skip">The Result Skips</param>
        /// <param name="limit">(Max = 25) The Result Limits</param>
        public abstract Task<Results<FriendData>> GetMyFriends(int skip = 0, int limit = 25);


        /// <summary>
        ///     Get Friend Requests With Specific skip & limit
        /// </summary>
        /// <param name="skip">The Result Skips</param>
        /// <param name="limit">(Max = 25) The Result Limits</param>
        public abstract Task<Results<FriendData>> GetFriendRequests(int skip = 0, int limit = 25);


        /// <summary>
        ///     Send Friend Request With Specific Member ID
        /// </summary>
        /// <param name="memberId">(NOTNULL)the Member ID That You want Friend Request</param>
        /// <returns>Returns true if Send Friend Request Successfully Done</returns>
        public abstract Task<bool> SendFriendRequest(string memberId);


        /// <summary>
        ///     Accept Friend Request With Specific Requester Member ID
        /// </summary>
        /// <param name="memberId">(NOTNULL)the Requester Member ID That You want Accept Friend Request</param>
        /// <returns>Returns true if Accept Friend Request Successfully</returns>
        public abstract Task<bool> AcceptFriendRequest(string memberId);


        /// <summary>
        ///     Delete Friend  With Specific Friend Member ID
        /// </summary>
        /// <param name="memberId">(NOTNULL)the Friend Member ID That You want Delete it</param>
        /// <returns>Returns true if Delete Friend Successfully Done</returns>
        public abstract Task<bool> DeleteFriend(string memberId);
    }
}