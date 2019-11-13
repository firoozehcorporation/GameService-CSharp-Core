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
using System.Collections.Generic;
using System.Threading.Tasks;
using FiroozehGameService.Builder;
using FiroozehGameService.Core.ApiWebRequest;
using FiroozehGameService.Models;
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
        public static event EventHandler<Notification> NotificationReceived;
        public static DownloadManager DownloadManager;
        
        internal static string UserToken;
        internal static string PlayToken;
        internal static Game CurrentGame;
        internal static bool IsAvailable;
        internal static long StartPlaying;


        public static GSLive.GSLive GSLive { get; private set; }
        public static GameServiceClientConfiguration Configuration { get; private set; }

        
        /// <summary>
        /// Set configuration For Initialize Game Service.
        /// </summary>
        /// <param name="configuration">(Not NULL)configuration For Initialize Game Service</param>
        public static void ConfigurationInstance(GameServiceClientConfiguration configuration)
        {  
            Configuration = configuration;   
            DownloadManager = new DownloadManager(Configuration);
            GSLive = new GSLive.GSLive();
        }

        public static void OnNotificationReceived(Notification notification)
            => NotificationReceived?.Invoke(null,notification);



        /// <summary>
        /// With this command you can get  list of all your game LeaderBoard
        /// that you have registered in the Developer panel
        /// </summary>
        /// <value> GetLeaderBoards List </value>
        public static async Task<List<LeaderBoard>> GetLeaderBoards()
        {
            if (!IsAvailable) throw new GameServiceException("GameService Not Available");
            return await ApiRequest.GetLeaderBoard();
        }
        
        /// <summary>
        /// With this command you can get list of all your game achievements
        /// that you have registered in the Developer panel.
        /// </summary>
        /// <value> GetAchievements List </value>
        public static async Task<List<Achievement>> GetAchievements()
        {
            if (!IsAvailable) throw new GameServiceException("GameService Not Available");
            return await ApiRequest.GetAchievements();
        }



        /// <summary>
        /// With this command you can save your Current Status in Game
        /// </summary>
        /// <param name="saveGameName">saveGameName</param>
        /// <param name="saveGameDescription">saveGameDescription</param>
        /// <param name="saveGameObj">the Object that you Want To Save it</param>
        /// <value> return SaveDetails </value>
        public static async Task<SaveDetails> SaveGame(
            string saveGameName
            , string saveGameDescription
            , object saveGameObj)
        {
            if (!IsAvailable) throw new GameServiceException("GameService Not Available");
            return await ApiRequest.SaveGame(saveGameName,saveGameDescription,saveGameObj);
        }

        
        /// <summary>
        /// This command allows you to Submit Player Score with the ID of the leaderBoard
        /// you have Registered in the Developer panel
        /// </summary>
        /// <param name="leaderBoardId">leaderBoardId</param>
        /// <param name="scoreValue">scoreValue(The value must not exceed the maximum value Registered in the Developer Panel)</param>
        /// <value> return SaveDetails </value>
        public static async Task<SubmitScoreResponse> SubmitScore(
            string leaderBoardId,int scoreValue)
        {
            if (!IsAvailable) throw new GameServiceException("GameService Not Available");
            return await ApiRequest.SubmitScore(leaderBoardId,scoreValue);
        }


        /// <summary>
        /// With this command you can Unlock achievement with the achievement ID
        /// you registered in the Developer panel.
        /// </summary>
        /// <param name="achievementId">The ID of Achievement you Want To Unlock it</param>
        /// <value> return unlocked Achievement </value>
        public static async Task<Achievement> UnlockAchievement(string achievementId)
        {
            if (!IsAvailable) throw new GameServiceException("GameService Not Available");
            return await ApiRequest.UnlockAchievement(achievementId);
        }


        /// <summary>
        /// This command will get you the last save you saved
        /// </summary>
        /// <value> return Player Last Save </value>
        public static async Task<T> GetSaveGame<T>()
        {
            if (!IsAvailable) throw new GameServiceException("GameService Not Available");
            return await ApiRequest.GetSaveGame<T>();
        }



        /// <summary>
        /// With this command you can get a LeaderBoardDetails with the ID of the LeaderBoard list
        /// you registered in the Developer panel.
        /// </summary>
        /// <param name="leaderBoardId">The ID of leaderBoard you Want To get Detail</param>
        /// <value> return LeaderBoardDetails </value>
        public static async Task<LeaderBoardDetails> GetLeaderBoardDetails(string leaderBoardId)
        {
            if (!IsAvailable) throw new GameServiceException("GameService Not Available");
            return await ApiRequest.GetLeaderBoardDetails(leaderBoardId);
        }


        /// <summary>
        /// With this command you can get information about the current player is playing
        /// </summary>
        /// <value> return GetCurrentPlayer </value>
        public static async Task<User> GetCurrentPlayer()
        {
            if (!IsAvailable) throw new GameServiceException("GameService Not Available");
            return await ApiRequest.GetCurrentPlayer();
        }


        /// <summary>
        /// This command can remove the last current user saved
        /// </summary>
        /// <value> return true if Remove Successfully </value>
        public static async Task<bool> RemoveLastSave()
        {
            if (!IsAvailable) throw new GameServiceException("GameService Not Available");
            return await ApiRequest.RemoveLastSave();
        }


        /// <summary>
        /// This command will return all information about the bucket with a specific ID
        /// </summary>
        /// <param name="bucketId">The ID of Bucket you Want To get Detail</param>
        /// <value> return List of all Bucket Items</value>
        public static async Task<List<TBucket>> GetBucketItems<TBucket>(string bucketId)
        {
            if (!IsAvailable) throw new GameServiceException("GameService Not Available");
            return await ApiRequest.GetBucketItems<TBucket>(bucketId);
        }


        /// <summary>
        /// This command returns one of the Specific bucket information with a specific ID
        /// </summary>
        /// <param name="bucketId">The ID of Bucket you Want To get Detail</param>
        /// <param name="itemId">The ID of BucketItem you Want To get Detail</param>
        /// <value> return a Bucket Item</value>
        public static async Task<Bucket<TBucket>> GetBucketItem<TBucket>(string bucketId, string itemId)
        {
            if (!IsAvailable) throw new GameServiceException("GameService Not Available");
            return await ApiRequest.GetBucketItem<TBucket>(bucketId,itemId);
        }

        
        
        /// <summary>
        /// This command will edit one of the bucket information with a specific ID
        /// </summary>
        /// <param name="bucketId">The ID of Bucket you Want To Edit Details</param>
        /// <param name="itemId">The ID of BucketItem you Want To Edit Details</param>
        /// <param name="editedBucket">The Object of BucketItem you Want To Edit Detail</param>
        /// <value> return Edited Bucket Item</value>
        public static async Task<Bucket<TBucket>> UpdateBucketItem<TBucket>(string bucketId, string itemId, TBucket editedBucket)
        {
            if (!IsAvailable) throw new GameServiceException("GameService Not Available");
            return await ApiRequest.UpdateBucketItem(bucketId,itemId,editedBucket);
        }

        /// <summary>
        /// This command will Add new bucket information with a specific ID
        /// </summary>
        /// <param name="bucketId">The ID of Bucket you Want To Add Item</param>
        /// <param name="newBucket">The Object of BucketItem you Want To Add</param>
        /// <value> return Added Bucket Item</value>
        public static async Task<Bucket<TBucket>> AddBucketItem<TBucket>(string bucketId, TBucket newBucket)
        {
            if (!IsAvailable) throw new GameServiceException("GameService Not Available");
            return await ApiRequest.AddBucketItem(bucketId,newBucket);
        }



        /// <summary>
        /// This command will delete All of the bucket Items information with a specific ID
        /// </summary>
        /// <param name="bucketId">The ID of Bucket you Want To Delete All Items</param>
        /// <value> return true if Remove Successfully </value>
        public static async Task<bool> DeleteBucketItems(string bucketId)
        {
            if (!IsAvailable) throw new GameServiceException("GameService Not Available");
            return await ApiRequest.DeleteBucketItems(bucketId);
        }


        /// <summary>
        /// This command will delete one of the bucket information with a specific ID
        /// </summary>
        /// <param name="bucketId">The ID of Bucket you Want To Delete one of Items</param>
        /// <param name="itemId">The ID of BucketItem you Want To Delete it</param>
        /// <value> return true if Remove Successfully </value>
        public static async Task<bool> DeleteBucketItem(string bucketId, string itemId)
        {
            if (!IsAvailable) throw new GameServiceException("GameService Not Available");
            return await ApiRequest.DeleteBucketItem(bucketId,itemId);
        }
        
        
        
        /// <summary>
        /// Normal Login To Game Service
        /// It May Throw Exception
        /// </summary>
        public static async Task<bool> Login(string email , string password)
        {
            Logout();
            var login = await ApiRequest.Login(email, password);
            UserToken = login.Token;
            var auth = await ApiRequest.Authorize(Configuration, false);
            PlayToken = auth.Token;
            CurrentGame = auth.Game;
            StartPlaying = (long) DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            IsAvailable = true;
            return true;
        }
        
        /// <summary>
        /// Login To Game Service As Guest
        /// It May Throw Exception
        /// </summary>
        public static async Task<bool> Login()
        {
            Logout();
            var auth = await ApiRequest.Authorize(Configuration, true);
            PlayToken = auth.Token;
            CurrentGame = auth.Game;
            StartPlaying = (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            IsAvailable = true;
            return true;
        }
        
        /// <summary>
        /// Normal SignUp To Game Service
        /// It May Throw Exception
        /// </summary>
        public static async Task<bool> SignUp(string nickName,string email , string password)
        {
            Logout();
            var login = await ApiRequest.SignUp(nickName,email,password);
            UserToken = login.Token;
            var auth = await ApiRequest.Authorize(Configuration, false);
            PlayToken = auth.Token;
            CurrentGame = auth.Game;
            StartPlaying = (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            IsAvailable = true;
            return true;
        }
               
        
        /// <summary>
        /// Logout To Game Service
        /// </summary>
        public static void Logout()
        {
            UserToken = null;
            CurrentGame = null;
            PlayToken = null;
            DownloadManager = null; 
            IsAvailable = false;
        }

    }  
}
