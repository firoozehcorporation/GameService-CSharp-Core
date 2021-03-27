// <copyright file="IPlayerProvider.cs" company="Firoozeh Technology LTD">
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

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FiroozehGameService.Models.GSLive;

namespace FiroozehGameService.Models.BasicApi.Providers
{
    /// <summary>
    ///     Represents Player Provider Model In Game Service Basic API
    /// </summary>
    public interface IPlayerProvider
    {
        /// <summary>
        ///     With this command you can get information about the current player is playing
        /// </summary>
        /// <value> return CurrentPlayer Data </value>
        Task<Member> GetCurrentPlayer();


        /// <summary>
        ///     With this command you can get a User Data with the User ID
        /// </summary>
        /// <param name="userId">(NOTNULL)The ID of User you Want To get Detail</param>
        /// <value> return User Data </value>
        [Obsolete("This Method is Deprecated,Use GetMemberData() Instead")]
        Task<User> GetUserData(string userId);


        /// <summary>
        ///     With this command you can get a Member Data with the Member ID
        /// </summary>
        /// <param name="memberId">(NOTNULL)The ID of Member you Want To get Detail</param>
        /// <value> return Member Data </value>
        Task<Member> GetMemberData(string memberId);


        /// <summary>
        ///     With this command you can get The Last Login Member Info
        /// </summary>
        /// <value> return Member Data </value>
        Task<MemberInfo> GetLastLoginMemberInfo();


        /// <summary>
        ///     With this command you can Edit information about the current player is playing
        /// </summary>
        /// <value> return Edited Current Member Info Data </value>
        /// <param name="profile">(NOTNULL)Specifies EditUserProfile Class </param>
        Task<MemberInfo> EditCurrentPlayerProfile(EditUserProfile profile);


        /// <summary>
        ///     Change Password With CurrentPassword and New One
        /// </summary>
        /// <param name="currentPassword">(NOTNULL)Specifies the Current Password </param>
        /// <param name="newPassword">(NOTNULL)Specifies the New Password </param>
        Task<bool> ChangePassword(string currentPassword, string newPassword);


        /// <summary>
        ///     Get All Active Devices
        ///     You Can Get Active Devices to Revoke Some Devices Access
        /// </summary>
        /// <value> return Active Devices </value>
        Task<List<ActiveDevice>> GetActiveDevices();

        /// <summary>
        ///     Revoke a Active Device with deviceId
        ///     You Can Get Active Devices to Revoke Some Devices Access
        /// </summary>
        /// <param name="deviceId">(NOTNULL)Specifies the device Id that you want to revoke</param>
        Task<bool> RevokeActiveDevice(string deviceId);
    }
}