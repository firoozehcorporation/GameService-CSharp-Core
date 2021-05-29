// <copyright file="ILoginOrSignUpProvider.cs" company="Firoozeh Technology LTD">
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

namespace FiroozehGameService.Models.BasicApi.Providers
{
    /// <summary>
    ///     Represents ILoginOrSignUpProvider Model In Game Service Basic API
    /// </summary>
    public interface ILoginOrSignUpProvider
    {
        /// <summary>
        ///     Normal Login (InFirstOnly) To Game Service
        ///     It May Throw Exception
        /// </summary>
        /// <param name="email">(NOTNULL)Specifies the Email </param>
        /// <param name="password">(NOTNULL)Specifies the Password</param>
        /// <value> return UserToken if Login Successfully </value>
        Task<string> Login(string email, string password);


        /// <summary>
        ///     Normal Login(or signUp) With Phone Number To Game Service
        ///     You Must Call SendLoginCodeWithSms First, to get SMS Code.
        ///     It May Throw Exception
        ///     <param name="nickName">(NOTNULL)Specifies Nick Name (NOTE : Name Sets Only in FirstTime(Register)) </param>
        ///     <param name="phoneNumber">(NOTNULL)Specifies the Phone Number</param>
        ///     <param name="smsCode">(NOTNULL)Specifies SMS Code</param>
        /// </summary>
        /// <value> return UserToken if Login Successfully </value>
        Task<string> LoginOrSignUpWithSms(string nickName, string phoneNumber, string smsCode);


        /// <summary>
        ///     Normal Login With UserToken To Game Service
        ///     It May Throw Exception
        ///     <param name="userToken">(NOTNULL)Specifies the User Token </param>
        /// </summary>
        Task LoginWithToken(string userToken);


        /// <summary>
        ///     Normal Login(or signUp) With GoogleSignInUser To Game Service
        ///     It May Throw Exception
        ///     <param name="googleIdToken">(NOTNULL)Specifies the idToken From GoogleSignInUser Class.</param>
        /// </summary>
        /// <value> return UserToken if Login Successfully </value>
        Task<string> LoginOrSignUpWithGoogle(string googleIdToken);


        /// <summary>
        ///     Normal SignUp To Game Service
        ///     It May Throw Exception
        /// </summary>
        /// <value> return UserToken if SignUp Successfully </value>
        Task<string> SignUp(string nickName, string email, string password);


        /// <summary>
        ///     This command Check Sms Status
        /// </summary>
        /// <value> return The Status</value>
        Task<bool> CheckSmsStatus();


        /// <summary>
        ///     Send Login Code With SMS , If you want to LoginWithPhoneNumber, You Must Call This Function first
        ///     It May Throw Exception
        ///     <param name="phoneNumber">(NOTNULL)Specifies the Phone Number</param>
        /// </summary>
        /// <value> return true if Send Successfully </value>
        Task<bool> SendLoginCodeSms(string phoneNumber);


        /// <summary>
        ///     Login To Game Service As Guest
        ///     It May Throw Exception
        /// </summary>
        Task LoginAsGuest();
    }
}