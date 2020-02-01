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
using System.Threading;
using System.Threading.Tasks;
using FiroozehGameService.Builder;
using FiroozehGameService.Core.ApiWebRequest;
using FiroozehGameService.Models;
using FiroozehGameService.Models.BasicApi;
using FiroozehGameService.Models.BasicApi.Buckets;
using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.Internal;
using FiroozehGameService.Utils;
using EditUserProfile = FiroozehGameService.Models.BasicApi.EditUserProfile;

namespace FiroozehGameService.Core
{
    
    /// <summary>
    /// Represents Game Service Main Initializer
    /// </summary>
    public sealed class GameService
    {
        
        #region GameServiceRegion
        private const string Tag = "FiroozehGameService";
        private static bool _isAvailable;
        public static event EventHandler<Notification> NotificationReceived;
        
        internal static string UserToken;
        internal static string PlayToken;
        internal static Game CurrentGame;
        internal static long StartPlaying;
        internal static SynchronizationContext SynchronizationContext;

        public static GSLive.GSLive GSLive { get; private set; }
        private static GameServiceClientConfiguration Configuration { get; set; }
        private static DownloadManager _downloadManager;
        #endregion
        
        /// <summary>
        /// Set configuration For Initialize Game Service.
        /// </summary>
        /// <param name="configuration">(Not NULL)configuration For Initialize Game Service</param>
        public static void ConfigurationInstance(GameServiceClientConfiguration configuration)
        {
            if(SynchronizationContext.Current == null)
                SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
            SynchronizationContext = SynchronizationContext.Current;

            if(IsAuthenticated()) throw new GameServiceException("Must Logout First To ReConfiguration");
            Configuration = configuration;   
            _downloadManager = new DownloadManager(Configuration);
            GSLive = new GSLive.GSLive();
        }

        internal static void OnNotificationReceived(Notification notification)
            => NotificationReceived?.Invoke(null,notification);



        /// <summary>
        /// With this command you can get  list of all your game LeaderBoard
        /// that you have registered in the Developer panel
        /// </summary>
        /// <value> GetLeaderBoards List </value>
        public static async Task<List<LeaderBoard>> GetLeaderBoards()
        {
            if(!IsAuthenticated()) throw new GameServiceException("GameService Not Available");
            return await ApiRequest.GetLeaderBoard();
        }
        
        /// <summary>
        /// With this command you can get list of all your game achievements
        /// that you have registered in the Developer panel.
        /// </summary>
        /// <value> GetAchievements List </value>
        public static async Task<List<Achievement>> GetAchievements()
        {
            if(!IsAuthenticated()) throw new GameServiceException("GameService Not Available");
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
            if(!IsAuthenticated()) throw new GameServiceException("GameService Not Available");
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
            if(!IsAuthenticated()) throw new GameServiceException("GameService Not Available");
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
            if(!IsAuthenticated()) throw new GameServiceException("GameService Not Available");
            return await ApiRequest.UnlockAchievement(achievementId);
        }


        /// <summary>
        /// This command will get you the last save you saved
        /// </summary>
        /// <value> return Player Last Save </value>
        public static async Task<T> GetSaveGame<T>()
        {
            if(!IsAuthenticated()) throw new GameServiceException("GameService Not Available");
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
            if(!IsAuthenticated()) throw new GameServiceException("GameService Not Available");
            return await ApiRequest.GetLeaderBoardDetails(leaderBoardId);
        }


        /// <summary>
        /// With this command you can get information about the current player is playing
        /// </summary>
        /// <value> return CurrentPlayer Data </value>
        public static async Task<User> GetCurrentPlayer()
        {
            if(!IsAuthenticated()) throw new GameServiceException("GameService Not Available");
            return await ApiRequest.GetCurrentPlayer();
        }
        
        /// <summary>
        /// With this command you can Edit information about the current player is playing
        /// </summary>
        /// <value> return CurrentPlayer Data </value>
        public static async Task<User> EditCurrentPlayerProfile(EditUserProfile profile)
        {
            if(!IsAuthenticated()) throw new GameServiceException("GameService Not Available");
            return await ApiRequest.EditCurrentPlayer(profile);
        }


        /// <summary>
        /// This command can remove the last current user saved
        /// </summary>
        /// <value> return true if Remove Successfully </value>
        public static async Task<bool> RemoveLastSave()
        {
            if(!IsAuthenticated()) throw new GameServiceException("GameService Not Available");
            return await ApiRequest.RemoveLastSave();
        }


        /// <summary>
        /// This command will return all information about the bucket with a specific ID
        /// </summary>
        /// <param name="bucketId">The ID of Bucket you Want To get Detail</param>
        /// <param name="options">(Optional)The Bucket Options</param>
        /// <value> return List of all Bucket Items</value>
        public static async Task<List<TBucket>> GetBucketItems<TBucket>(string bucketId,BucketOption[] options = null)
        {
            if(!IsAuthenticated()) throw new GameServiceException("GameService Not Available");
            return await ApiRequest.GetBucketItems<TBucket>(bucketId,options);
        }


        /// <summary>
        /// This command returns one of the Specific bucket information with a specific ID
        /// </summary>
        /// <param name="bucketId">The ID of Bucket you Want To get Detail</param>
        /// <param name="itemId">The ID of BucketItem you Want To get Detail</param>
        /// <value> return a Bucket Item</value>
        public static async Task<Bucket<TBucket>> GetBucketItem<TBucket>(string bucketId, string itemId)
        {
            if(!IsAuthenticated()) throw new GameServiceException("GameService Not Available");
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
            if(!IsAuthenticated()) throw new GameServiceException("GameService Not Available");
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
            if(!IsAuthenticated()) throw new GameServiceException("GameService Not Available");
            return await ApiRequest.AddBucketItem(bucketId,newBucket);
        }



        /// <summary>
        /// This command will delete All of the bucket Items information with a specific ID
        /// </summary>
        /// <param name="bucketId">The ID of Bucket you Want To Delete All Items</param>
        /// <value> return true if Remove Successfully </value>
        public static async Task<bool> DeleteBucketItems(string bucketId)
        {
            if(!IsAuthenticated()) throw new GameServiceException("GameService Not Available");
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
            if(!IsAuthenticated()) throw new GameServiceException("GameService Not Available");
            return await ApiRequest.DeleteBucketItem(bucketId,itemId);
        }
        
        
        /// <summary>
        /// Download Asset With Tag
        /// Set DownloadManager Event Handlers To Get Download Status 
        /// </summary>
        /// <param name="tag">(Not NULL)Specifies the Asset tag that Set in Developers Panel.</param>
        /// <param name="dirPath">(Not NULL)Specifies the Download File Directory Path </param>
        public static async Task DownloadAsset(string tag,string dirPath)
        {
            if (Configuration == null) throw new GameServiceException("You Must Configuration First");
            await _downloadManager.StartDownload(tag, dirPath);
        }
        
        
        /// <summary>
        /// Download Asset With Tag
        /// Set DownloadManager Event Handlers To Get Download Status 
        /// </summary>
        /// <param name="tag">(Not NULL)Specifies the Asset tag that Set in Developers Panel.</param>
        public static async Task DownloadAsset(string tag)
        {
            if (Configuration == null) throw new GameServiceException("You Must Configuration First");
            await _downloadManager.StartDownload(tag);
        }

        
        
        /// <summary>
        /// Normal Login (InFirstOnly) To Game Service
        /// It May Throw Exception
        /// </summary>
        /// <value> return UserToken if Login Successfully </value>
        public static async Task<string> Login(string email , string password)
        {
            if(!NetworkUtil.IsConnected()) throw new GameServiceException("Network Unreachable");
            if(Configuration == null) throw new GameServiceException("You Must Configuration First");
            if(IsAuthenticated()) Logout();
            
            var login = await ApiRequest.Login(email, password);
            UserToken = login.Token;
            var auth = await ApiRequest.Authorize(Configuration, false);
            PlayToken = auth.Token;
            CurrentGame = auth.Game;
            StartPlaying = (long) DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            _isAvailable = true;
            await Core.GSLive.GSLive.Init();
            return UserToken;
        }
        
        /// <summary>
        /// Normal Login With UserToken To Game Service
        /// It May Throw Exception
        /// </summary>
        public static async Task Login(string userToken)
        {
            if(!NetworkUtil.IsConnected()) throw new GameServiceException("Network Unreachable");
            if(Configuration == null) throw new GameServiceException("You Must Configuration First");
            if(IsAuthenticated()) Logout();
            
            UserToken = userToken;
            var auth = await ApiRequest.Authorize(Configuration, false);
            PlayToken = auth.Token;
            CurrentGame = auth.Game;
            StartPlaying = (long) DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            _isAvailable = true;
            await Core.GSLive.GSLive.Init();
        }
        
        /// <summary>
        /// Login To Game Service As Guest
        /// It May Throw Exception
        /// </summary>
        public static async Task Login()
        {
            if(!NetworkUtil.IsConnected()) throw new GameServiceException("Network Unreachable");
            if(Configuration == null) throw new GameServiceException("You Must Configuration First");
            if(IsAuthenticated()) Logout();
           
            var auth = await ApiRequest.Authorize(Configuration, true);
            PlayToken = auth.Token;
            CurrentGame = auth.Game;
            StartPlaying = (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            _isAvailable = true;
            await Core.GSLive.GSLive.Init();
        }
        
        /// <summary>
        /// Normal SignUp To Game Service
        /// It May Throw Exception
        /// </summary>
        /// <value> return UserToken if SignUp Successfully </value>
        public static async Task<string> SignUp(string nickName,string email,string password)
        {
            if(!NetworkUtil.IsConnected()) throw new GameServiceException("Network Unreachable");
            if(Configuration == null) throw new GameServiceException("You Must Configuration First");
            if(IsAuthenticated()) Logout();
           
            var login = await ApiRequest.SignUp(nickName,email,password);
            UserToken = login.Token;
            var auth = await ApiRequest.Authorize(Configuration, false);
            PlayToken = auth.Token;
            CurrentGame = auth.Game;
            StartPlaying = (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            _isAvailable = true;
            await Core.GSLive.GSLive.Init();
            return UserToken;
        }


        /// <summary>
        /// Check if Current User Authenticated
        /// </summary>
        /// <value> return True if Current User Authenticated Before </value>
        public static bool IsAuthenticated()
            => _isAvailable;

        
        /// <summary>
        /// Logout From Game Service
        /// </summary>
        public static void Logout()
        {
            GSLive?.Dispose();
            UserToken = null;
            CurrentGame = null;
            PlayToken = null;
            _downloadManager = null; 
            _isAvailable = false;
        }

    }  
}
