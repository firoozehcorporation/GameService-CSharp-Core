// <copyright file="FiroozehGameService.cs" company="Firoozeh Technology LTD">
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

using System;
using System.Threading.Tasks;
using FiroozehGameService.Builder;
using FiroozehGameService.Core.ApiWebRequest;
using FiroozehGameService.Models.Command;

namespace FiroozehGameService.Core
{
    
    /// <summary>
    /// Represents Game Service Main Initializer
    /// </summary>
    public sealed class FiroozehGameService
    {       
        private const string Tag = "FiroozehGameService";
        public event EventHandler<Notification> NotificationReceived;
        public static DownloadManager DownloadManager;
        
        private static string _userToken;
        private static string _playToken;
        private static string _gameId;
        private static bool _isAvailable;


        public static GameService Instance { get; private set; }
        public static GameServiceClientConfiguration Configuration { get; private set; }

        
        /// <summary>
        /// Set configuration For Initialize Game Service.
        /// </summary>
        /// <param name="configuration">(Not NULL)configuration For Initialize Game Service</param>
        public static void ConfigurationInstance(GameServiceClientConfiguration configuration)
        {  
            Configuration = configuration;     
        }

        public void OnNotificationReceived(Notification notification)
            => NotificationReceived?.Invoke(this,notification);
        
        
        
        
        /// <summary>
        /// Normal Login To Game Service
        /// It May Throw Exception
        /// </summary>
        public static async Task Login(string email , string password)
        {
           var login = await ApiRequest.Login(email, password);
            _userToken = login.UserToken;
            
            var auth = await ApiRequest.Auth(Configuration, _userToken, false);
            DownloadManager = new DownloadManager(Configuration,auth.Game);
            _playToken = auth.UserToken;
            _gameId = auth.Game;
            _isAvailable = true;
        }
        
        /// <summary>
        /// Login To Game Service As Guest
        /// It May Throw Exception
        /// </summary>
        public static async Task Login()
        {
            var auth = await ApiRequest.Auth(Configuration, _userToken, true);
            DownloadManager = new DownloadManager(Configuration,auth.Game);
            _playToken = auth.UserToken;
            _gameId = auth.Game;
            _isAvailable = true;
        }
        
        /// <summary>
        /// Normal SignUp To Game Service
        /// It May Throw Exception
        /// </summary>
        public static async Task SignUp(string nickName,string email , string password)
        {
            var login = await ApiRequest.SignUp(nickName,email,password);
            _userToken = login.UserToken;
            var auth = await ApiRequest.Auth(Configuration, _userToken, false);
            DownloadManager = new DownloadManager(Configuration,auth.Game);
            _playToken = auth.UserToken;
            _gameId = auth.Game;
            _isAvailable = true;
        }
               
        
        /// <summary>
        /// Logout To Game Service
        /// </summary>
        public static bool Logout()
        {
            _userToken = null;
            _gameId = null;
            _playToken = null;
            DownloadManager = null; 
            _isAvailable = false;
            return true;
        }

    }  
}
