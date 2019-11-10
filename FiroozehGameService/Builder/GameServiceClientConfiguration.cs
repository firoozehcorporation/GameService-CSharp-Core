// <copyright file="GameServiceClientConfiguration.cs" company="Firoozeh Technology LTD">
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


using FiroozehGameService.Models;

namespace FiroozehGameService.Builder
{
    /// <summary>
    /// Represents ClientConfiguration For Game Service 
    /// </summary>
    public class GameServiceClientConfiguration
    {
        public readonly bool HaveNotification;
        public readonly bool CheckAppStatus;
        public readonly bool CheckOptionalUpdate;
        public readonly bool EnableLog;
        public readonly string ClientId;
        public readonly string ClientSecret;
        

        public GameServiceClientConfiguration(string clientId = null, string clientSecret = null,bool haveNotification = default, bool checkAppStatus = default,
            bool checkOptionalUpdate = default, bool enableLog = default)
        {
            HaveNotification = haveNotification;
            CheckAppStatus = checkAppStatus;
            CheckOptionalUpdate = checkOptionalUpdate;
            EnableLog = enableLog;
            ClientId = string.IsNullOrEmpty(clientId) ? throw new GameServiceException("clientId Cant Be Empty") : ClientId = clientId;
            ClientSecret = string.IsNullOrEmpty(clientSecret) ? throw new GameServiceException("clientSecret Cant Be Empty") : ClientSecret = clientSecret;
        }
    }
}