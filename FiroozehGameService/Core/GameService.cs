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
using FiroozehGameService.Models.BasicApi;
using FiroozehGameService.Models.Command;

namespace FiroozehGameService.Core
{
    
    /// <summary>
    /// Represents Game Service Main Initializer
    /// </summary>
    public sealed class GameService
    {       
        private const string Tag = "FiroozehGameService";
        public event EventHandler<Notification> NotificationReceived;
        public static DownloadManager DownloadManager;
        
        private static string _userToken;
        private static string _playToken;
        private static Game _currentGame;
        private static bool _isAvailable;


       //TODO public static GameService Instance { get; private set; }
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
        public static async Task<bool> Login(string email , string password)
        {
           var login = await ApiRequest.Login(email, password);
            _userToken = login.Token;
            
            var auth = await ApiRequest.Auth(Configuration, _userToken, false);
            _currentGame = auth.Game;
            _playToken = auth.Token;
            DownloadManager = new DownloadManager(Configuration,_playToken);
            _isAvailable = true;
            return true;
        }
        
        /// <summary>
        /// Login To Game Service As Guest
        /// It May Throw Exception
        /// </summary>
        public static async Task<bool> Login()
        {
            var auth = await ApiRequest.Auth(Configuration, _userToken, true);
            _playToken = auth.Token;
            _currentGame = auth.Game;
            DownloadManager = new DownloadManager(Configuration,_playToken);
            _isAvailable = true;
            return true;
        }
        
        /// <summary>
        /// Normal SignUp To Game Service
        /// It May Throw Exception
        /// </summary>
        public static async Task<bool> SignUp(string nickName,string email , string password)
        {
            var login = await ApiRequest.SignUp(nickName,email,password);
            _userToken = login.Token;
            var auth = await ApiRequest.Auth(Configuration, _userToken, false);
            _playToken = auth.Token;
            _currentGame = auth.Game;
            DownloadManager = new DownloadManager(Configuration,_playToken);
            _isAvailable = true;
            return true;
        }
               
        
        /// <summary>
        /// Logout To Game Service
        /// </summary>
        public static bool Logout()
        {
            _userToken = null;
            _currentGame = null;
            _playToken = null;
            DownloadManager = null; 
            _isAvailable = false;
            return true;
        }

    }  
}
