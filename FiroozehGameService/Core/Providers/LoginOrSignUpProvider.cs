// <copyright file="LoginOrSignUpProvider.cs" company="Firoozeh Technology LTD">
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
using FiroozehGameService.Builder;
using FiroozehGameService.Core.ApiWebRequest;
using FiroozehGameService.Handlers;
using FiroozehGameService.Models;
using FiroozehGameService.Models.BasicApi.Providers;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Utils;

namespace FiroozehGameService.Core.Providers
{
    /// <summary>
    ///     Represents Achievement Data Model In Game Service Basic API
    /// </summary>
    internal class LoginOrSignUpProvider : ILoginOrSignUpProvider
    {
        private static GameServiceClientConfiguration Configuration => GameService.Configuration;

        public async Task<string> Login(string email, string password)
        {
            if (Configuration == null)
                throw new GameServiceException("You Must Configuration First").LogException(typeof(GameService),
                    DebugLocation.Internal, "Login");
            if (!NetworkUtil.IsConnected())
                throw new GameServiceException("Network Unreachable").LogException(typeof(GameService),
                    DebugLocation.Internal, "Login");
            if (string.IsNullOrEmpty(email))
                throw new GameServiceException("Email Cant Be EmptyOrNull").LogException(typeof(GameService),
                    DebugLocation.Internal, "Login");
            if (string.IsNullOrEmpty(password))
                throw new GameServiceException("Password Cant Be EmptyOrNull").LogException(typeof(GameService),
                    DebugLocation.Internal, "Login");
            if (GameService.IsAuthenticated()) GameService.Logout();

            var login = await ApiRequest.Login(email, password);
            GameService.UserToken = login.Token;
            var auth = await ApiRequest.Authorize();
            GameService.CommandInfo = auth.CommandInfo;
            GameService.PlayToken = auth.Token;
            GameService.CurrentInternalGame = auth.Game;
            GameService.IsAvailable = true;
            GameService.IsGuest = false;
            await GSLive.GSLive.Init();

            return GameService.UserToken;
        }


        public async Task<string> LoginOrSignUpWithSms(string nickName, string phoneNumber, string smsCode)
        {
            if (Configuration == null)
                throw new GameServiceException("You Must Configuration First").LogException(typeof(GameService),
                    DebugLocation.Internal, "LoginWithPhoneNumber");
            if (!NetworkUtil.IsConnected())
                throw new GameServiceException("Network Unreachable").LogException(typeof(GameService),
                    DebugLocation.Internal, "LoginWithPhoneNumber");
            if (string.IsNullOrEmpty(nickName))
                throw new GameServiceException("nickName Cant Be EmptyOrNull").LogException(typeof(GameService),
                    DebugLocation.Internal, "LoginWithPhoneNumber");
            if (string.IsNullOrEmpty(phoneNumber))
                throw new GameServiceException("phoneNumber Cant Be EmptyOrNull").LogException(typeof(GameService),
                    DebugLocation.Internal, "LoginWithPhoneNumber");
            if (string.IsNullOrEmpty(smsCode))
                throw new GameServiceException("smsCode Cant Be EmptyOrNull").LogException(typeof(GameService),
                    DebugLocation.Internal, "LoginWithPhoneNumber");
            if (GameService.IsAuthenticated()) GameService.Logout();

            var login = await ApiRequest.LoginWithPhoneNumber(nickName, phoneNumber, smsCode);
            GameService.UserToken = login.Token;
            var auth = await ApiRequest.Authorize();
            GameService.CommandInfo = auth.CommandInfo;
            GameService.PlayToken = auth.Token;
            GameService.CurrentInternalGame = auth.Game;
            GameService.IsAvailable = true;
            GameService.IsGuest = false;
            await GSLive.GSLive.Init();
            return GameService.UserToken;
        }

        public async Task LoginWithToken(string userToken)
        {
            if (Configuration == null)
                throw new GameServiceException("You Must Configuration First").LogException(typeof(GameService),
                    DebugLocation.Internal, "LoginWithToken");
            if (!NetworkUtil.IsConnected())
                throw new GameServiceException("Network Unreachable").LogException(typeof(GameService),
                    DebugLocation.Internal, "LoginWithToken");
            if (string.IsNullOrEmpty(userToken))
                throw new GameServiceException("UserToken Cant Be EmptyOrNull").LogException(typeof(GameService),
                    DebugLocation.Internal, "LoginWithToken");
            if (GameService.IsAuthenticated()) GameService.Logout();

            GameService.UserToken = userToken;
            var auth = await ApiRequest.Authorize();
            GameService.CommandInfo = auth.CommandInfo;
            GameService.PlayToken = auth.Token;
            GameService.CurrentInternalGame = auth.Game;
            GameService.IsAvailable = true;
            GameService.IsGuest = false;
            await GSLive.GSLive.Init();
        }

        public async Task<string> LoginOrSignUpWithGoogle(string googleIdToken)
        {
            if (Configuration == null)
                throw new GameServiceException("You Must Configuration First").LogException(typeof(GameService),
                    DebugLocation.Internal, "LoginOrSignUpWithGoogle");
            if (!NetworkUtil.IsConnected())
                throw new GameServiceException("Network Unreachable").LogException(typeof(GameService),
                    DebugLocation.Internal, "LoginOrSignUpWithGoogle");
            if (string.IsNullOrEmpty(googleIdToken))
                throw new GameServiceException("IdToken Cant Be EmptyOrNull").LogException(typeof(GameService),
                    DebugLocation.Internal, "LoginOrSignUpWithGoogle");
            if (GameService.IsAuthenticated()) GameService.Logout();

            var login = await ApiRequest.LoginWithGoogle(googleIdToken);
            GameService.UserToken = login.Token;
            var auth = await ApiRequest.Authorize();
            GameService.CommandInfo = auth.CommandInfo;
            GameService.PlayToken = auth.Token;
            GameService.CurrentInternalGame = auth.Game;
            GameService.IsAvailable = true;
            GameService.IsGuest = false;
            await GSLive.GSLive.Init();
            return GameService.UserToken;
        }


        public async Task<string> SignUp(string nickName, string email, string password)
        {
            if (Configuration == null)
                throw new GameServiceException("You Must Configuration First").LogException(typeof(GameService),
                    DebugLocation.Internal, "SignUp");
            if (!NetworkUtil.IsConnected())
                throw new GameServiceException("Network Unreachable").LogException(typeof(GameService),
                    DebugLocation.Internal, "SignUp");
            if (string.IsNullOrEmpty(nickName))
                throw new GameServiceException("NickName Cant Be EmptyOrNull").LogException(typeof(GameService),
                    DebugLocation.Internal, "SignUp");
            if (string.IsNullOrEmpty(email))
                throw new GameServiceException("Email Cant Be EmptyOrNull").LogException(typeof(GameService),
                    DebugLocation.Internal, "SignUp");
            if (string.IsNullOrEmpty(password))
                throw new GameServiceException("Password Cant Be EmptyOrNull").LogException(typeof(GameService),
                    DebugLocation.Internal, "SignUp");
            if (GameService.IsAuthenticated()) GameService.Logout();

            var login = await ApiRequest.SignUp(nickName, email, password);
            GameService.UserToken = login.Token;
            var auth = await ApiRequest.Authorize();
            GameService.CommandInfo = auth.CommandInfo;
            GameService.PlayToken = auth.Token;
            GameService.CurrentInternalGame = auth.Game;
            GameService.IsAvailable = true;
            GameService.IsGuest = false;
            await GSLive.GSLive.Init();
            return GameService.UserToken;
        }

        public async Task<bool> CheckSmsStatus()
        {
            if (GameService.Configuration == null)
                throw new GameServiceException("You Must Configuration First").LogException(typeof(GameService),
                    DebugLocation.Internal, "CheckSmsStatus");
            return await ApiRequest.CheckPhoneLoginStatus();
        }

        public async Task<bool> SendLoginCodeSms(string phoneNumber)
        {
            if (Configuration == null)
                throw new GameServiceException("You Must Configuration First").LogException(typeof(GameService),
                    DebugLocation.Internal, "SendLoginCodeSms");
            if (!NetworkUtil.IsConnected())
                throw new GameServiceException("Network Unreachable").LogException(typeof(GameService),
                    DebugLocation.Internal, "SendLoginCodeSms");
            if (string.IsNullOrEmpty(phoneNumber))
                throw new GameServiceException("phoneNumber Cant Be EmptyOrNull").LogException(typeof(GameService),
                    DebugLocation.Internal, "SendLoginCodeSms");
            if (GameService.IsAuthenticated()) GameService.Logout();
            return await ApiRequest.SendLoginCodeWithSms(phoneNumber);
        }

        public async Task LoginAsGuest()
        {
            if (Configuration == null)
                throw new GameServiceException("You Must Configuration First").LogException(typeof(GameService),
                    DebugLocation.Internal, "LoginAsGuest");
            if (!NetworkUtil.IsConnected())
                throw new GameServiceException("Network Unreachable").LogException(typeof(GameService),
                    DebugLocation.Internal, "LoginAsGuest");
            if (GameService.IsAuthenticated()) GameService.Logout();

            var login = await ApiRequest.LoginAsGuest();
            GameService.UserToken = login.Token;
            var auth = await ApiRequest.Authorize();
            GameService.PlayToken = auth.Token;
            GameService.CurrentInternalGame = auth.Game;
            GameService.IsAvailable = true;
            GameService.IsGuest = true;
            CoreEventHandlers.SuccessfullyLogined?.Invoke(null, null);
        }
    }
}