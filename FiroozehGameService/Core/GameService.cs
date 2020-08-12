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
using FiroozehGameService.Handlers;
using FiroozehGameService.Models;
using FiroozehGameService.Models.BasicApi;
using FiroozehGameService.Models.BasicApi.Buckets;
using FiroozehGameService.Models.GSLive;
using FiroozehGameService.Models.GSLive.Command;
using FiroozehGameService.Models.Internal;
using FiroozehGameService.Utils;
using EditUserProfile = FiroozehGameService.Models.BasicApi.EditUserProfile;
using Game = FiroozehGameService.Models.Internal.Game;

namespace FiroozehGameService.Core
{
    /// <summary>
    ///     Represents Game Service Main Initializer
    /// </summary>
    public static class GameService
    {
        /// <summary>
        ///     Set configuration For Initialize Game Service.
        /// </summary>
        /// <param name="configuration">(Not NULL)configuration For Initialize Game Service</param>
        public static void ConfigurationInstance(GameServiceClientConfiguration configuration)
        {
            if (configuration == null) throw new GameServiceException("Configuration Cant Be Null");
            if (SynchronizationContext.Current == null)
                SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
            SynchronizationContext = SynchronizationContext.Current;

            if (IsAuthenticated()) throw new GameServiceException("Must Logout First To ReConfiguration");
            Configuration = configuration;
            _downloadManager = new DownloadManager(Configuration);
            GSLive = new GSLive.GSLive();
        }

        internal static void OnNotificationReceived(Notification notification)
        {
            NotificationReceived?.Invoke(null, notification);
        }


        /// <summary>
        ///     With this command you can get  list of all your game LeaderBoard
        ///     that you have registered in the Developer panel
        /// </summary>
        /// <value> GetLeaderBoards List </value>
        public static async Task<List<LeaderBoard>> GetLeaderBoards()
        {
            if (!IsAuthenticated()) throw new GameServiceException("GameService Not Available");
            return await ApiRequest.GetLeaderBoard();
        }

        /// <summary>
        ///     With this command you can get list of all your game achievements
        ///     that you have registered in the Developer panel.
        /// </summary>
        /// <value> GetAchievements List </value>
        public static async Task<List<Achievement>> GetAchievements()
        {
            if (!IsAuthenticated()) throw new GameServiceException("GameService Not Available");
            return await ApiRequest.GetAchievements();
        }


        /// <summary>
        ///     With this command you can save your Current Status in Game
        /// </summary>
        /// <param name="saveGameName">saveGameName</param>
        /// <param name="saveGameObj">the Object that you Want To Save it</param>
        /// <value> return SaveDetails </value>
        public static async Task<SaveDetails> SaveGame(string saveGameName, object saveGameObj)
        {
            if (!IsAuthenticated()) throw new GameServiceException("GameService Not Available");
            return await ApiRequest.SaveGame(saveGameName, saveGameObj);
        }


        /// <summary>
        ///     This command allows you to Submit Player Score with the ID of the leaderBoard
        ///     you have Registered in the Developer panel
        /// </summary>
        /// <param name="leaderBoardId">(Not NULL)leaderBoardId</param>
        /// <param name="scoreValue">scoreValue(The value must not exceed the maximum value Registered in the Developer Panel)</param>
        /// <value> return SubmitScore </value>
        public static async Task<SubmitScoreResponse> SubmitScore(
            string leaderBoardId, int scoreValue)
        {
            if (!IsAuthenticated()) throw new GameServiceException("GameService Not Available");
            if (string.IsNullOrEmpty(leaderBoardId))
                throw new GameServiceException("LeaderBoardId Cant Be EmptyOrNull");
            if (scoreValue <= 0) throw new GameServiceException("Invalid ScoreValue");
            return await ApiRequest.SubmitScore(leaderBoardId, scoreValue);
        }


        /// <summary>
        ///     With this command you can Unlock achievement with the achievement ID
        ///     you registered in the Developer panel.
        /// </summary>
        /// <param name="achievementId">(Not NULL)The ID of Achievement you Want To Unlock it</param>
        /// <value> return unlocked Achievement </value>
        public static async Task<Achievement> UnlockAchievement(string achievementId)
        {
            if (!IsAuthenticated()) throw new GameServiceException("GameService Not Available");
            if (string.IsNullOrEmpty(achievementId))
                throw new GameServiceException("AchievementId Cant Be EmptyOrNull");
            return await ApiRequest.UnlockAchievement(achievementId);
        }


        /// <summary>
        ///     This command will get you the last save you saved
        /// </summary>
        /// <value> return Player Last Save </value>
        public static async Task<T> GetSaveGame<T>()
        {
            if (!IsAuthenticated()) throw new GameServiceException("GameService Not Available");
            return await ApiRequest.GetSaveGame<T>();
        }


        /// <summary>
        ///     With this command you can get a LeaderBoardDetails with the ID of the LeaderBoard list
        ///     you registered in the Developer panel.
        /// </summary>
        /// <param name="leaderBoardId">(Not NULL)The ID of leaderBoard you Want To get Detail</param>
        /// <param name="scoreLimit">(Min = 10,Max = 50) The Score List Limits</param>
        /// <value> return LeaderBoardDetails </value>
        public static async Task<LeaderBoardDetails> GetLeaderBoardDetails(string leaderBoardId, int scoreLimit = 10)
        {
            if (!IsAuthenticated()) throw new GameServiceException("GameService Not Available");
            if (string.IsNullOrEmpty(leaderBoardId))
                throw new GameServiceException("LeaderBoardId Cant Be EmptyOrNull");
            if (scoreLimit < 10 || scoreLimit > 50) throw new GameServiceException("Invalid Limit Value");
            return await ApiRequest.GetLeaderBoardDetails(leaderBoardId, scoreLimit);
        }

        
        /// <summary>
        ///     With this command you can get Current Player Score with the ID of the LeaderBoard id
        ///     you registered in the Developer panel.
        /// </summary>
        /// <param name="leaderBoardId">(Not NULL)The ID of leaderBoard you Want To get Score</param>
        /// <value> return Score </value>
        public static async Task<Score> GetCurrentPlayerScore(string leaderBoardId)
        {
            if (!IsAuthenticated()) throw new GameServiceException("GameService Not Available");
            if (string.IsNullOrEmpty(leaderBoardId))
                throw new GameServiceException("LeaderBoardId Cant Be EmptyOrNull");
            return await ApiRequest.GetCurrentPlayerScore(leaderBoardId);
        }
        
        
        /// <summary>
        ///     With this command you can get information about the current player is playing
        /// </summary>
        /// <value> return CurrentPlayer Data </value>
        public static async Task<Member> GetCurrentPlayer()
        {
            if (!IsAuthenticated()) throw new GameServiceException("GameService Not Available");
            return await ApiRequest.GetCurrentPlayer();
        }


        /// <summary>
        ///     With this command you can get a User Data with the User ID
        /// </summary>
        /// <param name="userId">(Not NULL)The ID of User you Want To get Detail</param>
        /// <value> return User Data </value>
        [Obsolete("This Method is Deprecated,Use GetMemberData() Instead")]
        public static async Task<User> GetUserData(string userId)
        {
            if (!IsAuthenticated()) throw new GameServiceException("GameService Not Available");
            if (string.IsNullOrEmpty(userId))
                throw new GameServiceException("userId Cant Be EmptyOrNull");
            return await ApiRequest.GetUserData(userId);
        }


        /// <summary>
        ///     With this command you can get a Member Data with the Member ID
        /// </summary>
        /// <param name="memberId">(Not NULL)The ID of Member you Want To get Detail</param>
        /// <value> return Member Data </value>
        public static async Task<Member> GetMemberData(string memberId)
        {
            if (!IsAuthenticated()) throw new GameServiceException("GameService Not Available");
            if (string.IsNullOrEmpty(memberId))
                throw new GameServiceException("memberId Cant Be EmptyOrNull");
            return await ApiRequest.GetMemberData(memberId);
        }


        /// <summary>
        ///     With this command you can Edit information about the current player is playing
        /// </summary>
        /// <value> return Edited Current Member Data Data </value>
        public static async Task<Member> EditCurrentPlayerProfile(EditUserProfile profile)
        {
            if (!IsAuthenticated()) throw new GameServiceException("GameService Not Available");
            if (profile == null) throw new GameServiceException("EditUserProfile Cant Be Null");
            return await ApiRequest.EditCurrentPlayer(profile);
        }


        /// <summary>
        ///     This command can remove the last current user saved
        /// </summary>
        /// <value> return true if Remove Successfully </value>
        public static async Task<bool> RemoveLastSave()
        {
            if (!IsAuthenticated()) throw new GameServiceException("GameService Not Available");
            return await ApiRequest.RemoveLastSave();
        }


        /// <summary>
        ///     This command will return all information about the bucket with a specific ID
        /// </summary>
        /// <param name="bucketId">(Not NULL)The ID of Bucket you Want To get Detail</param>
        /// <param name="options">(Optional)The Bucket Options</param>
        /// <value> return List of all Bucket Items</value>
        public static async Task<List<TBucket>> GetBucketItems<TBucket>(string bucketId, BucketOption[] options = null)
            where TBucket : BucketCore
        {
            if (!IsAuthenticated()) throw new GameServiceException("GameService Not Available");
            if (string.IsNullOrEmpty(bucketId)) throw new GameServiceException("BucketId Cant Be EmptyOrNull");
            return await ApiRequest.GetBucketItems<TBucket>(bucketId, options);
        }


        /// <summary>
        ///     This command returns one of the Specific bucket information with a specific ID
        /// </summary>
        /// <param name="bucketId">(Not NULL)The ID of Bucket you Want To get Detail</param>
        /// <param name="itemId">(Not NULL)The ID of BucketItem you Want To get Detail</param>
        /// <value> return a Bucket Item</value>
        public static async Task<TBucket> GetBucketItem<TBucket>(string bucketId, string itemId)
            where TBucket : BucketCore
        {
            if (!IsAuthenticated()) throw new GameServiceException("GameService Not Available");
            if (string.IsNullOrEmpty(bucketId)) throw new GameServiceException("BucketId Cant Be EmptyOrNull");
            if (string.IsNullOrEmpty(itemId)) throw new GameServiceException("BucketItemId Cant Be EmptyOrNull");
            return await ApiRequest.GetBucketItem<TBucket>(bucketId, itemId);
        }


        /// <summary>
        ///     This command will edit one of the bucket information with a specific ID
        /// </summary>
        /// <param name="bucketId">(Not NULL)The ID of Bucket you Want To Edit Details</param>
        /// <param name="itemId">(Not NULL)The ID of BucketItem you Want To Edit Details</param>
        /// <param name="editedBucket">(Not NULL)The Object of BucketItem you Want To Edit Detail</param>
        /// <value> return Edited Bucket Item</value>
        public static async Task<TBucket> UpdateBucketItem<TBucket>(string bucketId, string itemId,
            TBucket editedBucket) where TBucket : BucketCore
        {
            if (!IsAuthenticated()) throw new GameServiceException("GameService Not Available");
            if (string.IsNullOrEmpty(bucketId)) throw new GameServiceException("BucketId Cant Be EmptyOrNull");
            if (string.IsNullOrEmpty(itemId)) throw new GameServiceException("BucketItemId Cant Be EmptyOrNull");
            if (editedBucket == null) throw new GameServiceException("EditedBucket Cant Be Null");
            return await ApiRequest.UpdateBucketItem(bucketId, itemId, editedBucket);
        }

        /// <summary>
        ///     This command will Add new bucket information with a specific ID
        /// </summary>
        /// <param name="bucketId">(Not NULL)The ID of Bucket you Want To Add Item</param>
        /// <param name="newBucket">(Not NULL)The Object of BucketItem you Want To Add</param>
        /// <value> return Added Bucket Item</value>
        public static async Task<TBucket> AddBucketItem<TBucket>(string bucketId, TBucket newBucket)
            where TBucket : BucketCore
        {
            if (!IsAuthenticated()) throw new GameServiceException("GameService Not Available");
            if (string.IsNullOrEmpty(bucketId)) throw new GameServiceException("BucketId Cant Be EmptyOrNull");
            if (newBucket == null) throw new GameServiceException("NewBucket Cant Be Null");
            return await ApiRequest.AddBucketItem(bucketId, newBucket);
        }


        /// <summary>
        ///     This command will delete All of the bucket Items information with a specific ID
        /// </summary>
        /// <param name="bucketId">(Not NULL)The ID of Bucket you Want To Delete All Items</param>
        /// <value> return true if Remove Successfully </value>
        public static async Task<bool> DeleteBucketItems(string bucketId)
        {
            if (!IsAuthenticated()) throw new GameServiceException("GameService Not Available");
            if (string.IsNullOrEmpty(bucketId)) throw new GameServiceException("BucketId Cant Be EmptyOrNull");
            return await ApiRequest.DeleteBucketItems(bucketId);
        }


        /// <summary>
        ///     This command will delete one of the bucket information with a specific ID
        /// </summary>
        /// <param name="bucketId">(Not NULL)The ID of Bucket you Want To Delete one of Items</param>
        /// <param name="itemId">(Not NULL)The ID of BucketItem you Want To Delete it</param>
        /// <value> return true if Remove Successfully </value>
        public static async Task<bool> DeleteBucketItem(string bucketId, string itemId)
        {
            if (!IsAuthenticated()) throw new GameServiceException("GameService Not Available");
            if (string.IsNullOrEmpty(bucketId)) throw new GameServiceException("BucketId Cant Be EmptyOrNull");
            if (string.IsNullOrEmpty(itemId)) throw new GameServiceException("BucketItemId Cant Be EmptyOrNull");
            return await ApiRequest.DeleteBucketItem(bucketId, itemId);
        }


        /// <summary>
        ///     This command Gets The Current Times
        /// </summary>
        /// <value> return The Current Times</value>
        public static async Task<GSTime> GetCurrentTime()
        {
            return await TimeUtil.GetCurrentTime();
        }

        
        
        /// <summary>
        ///     This command Check Can Login With Phone Number
        /// </summary>
        /// <value> return The Status</value>
        public static async Task<bool> CanLoginWithPhoneNumber()
        {
            if (Configuration == null) throw new GameServiceException("You Must Configuration First");
            return await ApiRequest.CheckPhoneLoginStatus();
        }


        /// <summary>
        ///     Gets Asset Info With AssetTag
        /// </summary>
        /// <param name="assetTag">(Not NULL)Specifies the Asset tag that Set in Developers Panel.</param>
        public static async Task<AssetInfo> GetAssetInfo(string assetTag)
        {
            if (Configuration == null) throw new GameServiceException("You Must Configuration First");
            if (string.IsNullOrEmpty(assetTag)) throw new GameServiceException("assetTag Cant Be EmptyOrNull");
            return await ApiRequest.GetAssetInfo(Configuration.ClientId, assetTag);
        }


        /// <summary>
        ///     Download Asset With Tag
        ///     Set DownloadManager Event Handlers To Get Download Status
        /// </summary>
        /// <param name="tag">(Not NULL)Specifies the Asset tag that Set in Developers Panel.</param>
        /// <param name="dirPath">(Not NULL)Specifies the Download File Directory Path </param>
        [Obsolete("This Method is Deprecated,Use DownloadAsset(AssetInfo info, string dirPath) Instead")]
        public static async Task DownloadAsset(string tag, string dirPath)
        {
            if (Configuration == null) throw new GameServiceException("You Must Configuration First");
            if (string.IsNullOrEmpty(tag)) throw new GameServiceException("DownloadTag Cant Be EmptyOrNull");
            if (string.IsNullOrEmpty(dirPath)) throw new GameServiceException("DownloadDirPath Cant Be EmptyOrNull");
            await _downloadManager.StartDownload(tag, dirPath);
        }


        /// <summary>
        ///     Download Asset With Tag
        ///     Set DownloadManager Event Handlers To Get Download Status
        /// </summary>
        /// <param name="tag">(Not NULL)Specifies the Asset tag that Set in Developers Panel.</param>
        [Obsolete("This Method is Deprecated,Use DownloadAsset(AssetInfo info) Instead")]
        public static async Task DownloadAsset(string tag)
        {
            if (Configuration == null) throw new GameServiceException("You Must Configuration First");
            if (string.IsNullOrEmpty(tag)) throw new GameServiceException("DownloadTag Cant Be EmptyOrNull");
            await _downloadManager.StartDownload(tag);
        }


        /// <summary>
        ///     Download Asset With AssetInfo
        ///     Set DownloadManager Event Handlers To Get Download Status
        /// </summary>
        /// <param name="info">(Not NULL)Specifies the Asset info</param>
        /// <param name="dirPath">(Not NULL)Specifies the Download File Directory Path </param>
        public static void DownloadAsset(AssetInfo info, string dirPath)
        {
            if (Configuration == null) throw new GameServiceException("You Must Configuration First");
            if (info == null) throw new GameServiceException("AssetInfo Cant Be Null");
            if (string.IsNullOrEmpty(dirPath)) throw new GameServiceException("DownloadDirPath Cant Be EmptyOrNull");
            _downloadManager.StartDownloadWithInfo(info, dirPath);
        }


        /// <summary>
        ///     Download Asset With AssetInfo
        ///     Set DownloadManager Event Handlers To Get Download Status
        /// </summary>
        /// <param name="info">(Not NULL)Specifies the Asset Info</param>
        public static async Task DownloadAsset(AssetInfo info)
        {
            if (Configuration == null) throw new GameServiceException("You Must Configuration First");
            if (info == null) throw new GameServiceException("AssetInfo Cant Be Null");
            await _downloadManager.StartDownloadWithInfo(info);
        }


        /// <summary>
        ///     Cancel All Current Download Assets
        /// </summary>
        public static void CancelAllDownloadAsset()
        {
            if (Configuration == null)
                throw new GameServiceException("You Must Configuration First");
            _downloadManager?.CancelAllDownloads();
        }
        
        
        /// <summary>
        ///     Cancel Download Asset With Asset Tag
        /// </summary>
        public static void CancelDownloadAsset(string tag)
        {
            if (Configuration == null)
                throw new GameServiceException("You Must Configuration First");
            if (string.IsNullOrEmpty(tag)) 
                throw new GameServiceException("Asset Tag Cant Be EmptyOrNull");
            _downloadManager.CancelDownload(tag);
        }
        
        
        /// <summary>
        ///     Cancel Download Asset With Asset Info
        /// </summary>
        public static void CancelDownloadAsset(AssetInfo info)
        {
            if (Configuration == null)
                throw new GameServiceException("You Must Configuration First");
            if (info == null)
                throw new GameServiceException("AssetInfo Cant Be Null");
            _downloadManager?.CancelDownload(info);
        }


        /// <summary>
        ///     Execute Cloud Function
        ///     note : if Function is public , You Can Call it without Login
        /// </summary>
        /// <param name="functionId">(NOTNULL)Specifies the Function Id that Set in Developers Panel</param>
        /// <param name="functionParameters">(NULLABLE)Specifies the Function Input Parameter Class that Set in Developers Panel</param>
        /// <param name="isPublic">Specifies the Function Visibility Type that Set in Developers Panel</param>
        /// <value> return Result in String </value>
        public static async Task<string> ExecuteCloudFunction(string functionId,
            object functionParameters = null, bool isPublic = false)
        {
            if (!isPublic && !IsAuthenticated())
                throw new GameServiceException("You Must Login First In Private Mode");
            if (string.IsNullOrEmpty(functionId)) throw new GameServiceException("functionId Cant Be NullOrEmpty");
            return await ApiRequest.ExecuteCloudFunction(functionId, functionParameters, isPublic);
        }



        /// <summary>
        ///     Send Login Code With SMS , If you want to LoginWithPhoneNumber, You Must Call This Function first
        ///     It May Throw Exception
        ///     <param name="phoneNumber">(Not NULL)Specifies the Phone Number</param>
        /// </summary>
        /// <value> return true if Send Successfully </value>
        public static async Task<bool> SendLoginCodeWithSms(string phoneNumber)
        {
            if (!NetworkUtil.IsConnected()) throw new GameServiceException("Network Unreachable");
            if (Configuration == null) throw new GameServiceException("You Must Configuration First");
            if (string.IsNullOrEmpty(phoneNumber)) throw new GameServiceException("phoneNumber Cant Be EmptyOrNull");
            if (IsAuthenticated()) Logout();
            return await ApiRequest.SendLoginCodeWithSms(phoneNumber);
        }

        
        
        /// <summary>
        ///     Normal Login (InFirstOnly) To Game Service
        ///     It May Throw Exception
        /// </summary>
        /// <value> return UserToken if Login Successfully </value>
        public static async Task<string> Login(string email, string password)
        {
            if (!NetworkUtil.IsConnected()) throw new GameServiceException("Network Unreachable");
            if (Configuration == null) throw new GameServiceException("You Must Configuration First");
            if (string.IsNullOrEmpty(email)) throw new GameServiceException("Email Cant Be EmptyOrNull");
            if (string.IsNullOrEmpty(password)) throw new GameServiceException("Password Cant Be EmptyOrNull");
            if (IsAuthenticated()) Logout();

            var login = await ApiRequest.Login(email, password);
            UserToken = login.Token;
            var auth = await ApiRequest.Authorize();
            CommandInfo = auth.CommandInfo;
            PlayToken = auth.Token;
            CurrentGame = auth.Game;
            _isAvailable = true;
            IsGuest = false;
            await Core.GSLive.GSLive.Init();
            return UserToken;
        }

        /// <summary>
        ///     Normal Login With UserToken To Game Service
        ///     It May Throw Exception
        /// </summary>
        public static async Task Login(string userToken)
        {
            if (!NetworkUtil.IsConnected()) throw new GameServiceException("Network Unreachable");
            if (Configuration == null) throw new GameServiceException("You Must Configuration First");
            if (string.IsNullOrEmpty(userToken)) throw new GameServiceException("UserToken Cant Be EmptyOrNull");
            if (IsAuthenticated()) Logout();

            UserToken = userToken;
            var auth = await ApiRequest.Authorize();
            CommandInfo = auth.CommandInfo;
            PlayToken = auth.Token;
            CurrentGame = auth.Game;
            _isAvailable = true;
            IsGuest = false;
            await Core.GSLive.GSLive.Init();
        }


        /// <summary>
        ///     Normal Login With GoogleSignInUser To Game Service
        ///     It May Throw Exception
        ///     <param name="idToken">(Not NULL)Specifies the idToken From GoogleSignInUser Class.</param>
        /// </summary>
        /// <value> return UserToken if Login Successfully </value>
        public static async Task<string> LoginWithGoogle(string idToken)
        {
            if (!NetworkUtil.IsConnected()) throw new GameServiceException("Network Unreachable");
            if (Configuration == null) throw new GameServiceException("You Must Configuration First");
            if (string.IsNullOrEmpty(idToken)) throw new GameServiceException("IdToken Cant Be EmptyOrNull");
            if (IsAuthenticated()) Logout();

            var login = await ApiRequest.LoginWithGoogle(idToken);
            UserToken = login.Token;
            var auth = await ApiRequest.Authorize();
            CommandInfo = auth.CommandInfo;
            PlayToken = auth.Token;
            CurrentGame = auth.Game;
            _isAvailable = true;
            IsGuest = false;
            await Core.GSLive.GSLive.Init();
            return UserToken;
        }
        
        
        
        /// <summary>
        ///     Normal Login With Phone Number To Game Service
        ///     You Must Call SendLoginCodeWithSms First, to get SMS Code.
        ///     It May Throw Exception
        ///
        ///     <param name="nickName">(Not NULL)Specifies Nick Name </param>
        ///     <param name="phoneNumber">(Not NULL)Specifies the Phone Number</param>
        ///     <param name="smsCode">(Not NULL)Specifies SMS Code</param>
        /// </summary>
        /// <value> return UserToken if Login Successfully </value>
        public static async Task<string> LoginWithPhoneNumber(string nickName,string phoneNumber,string smsCode)
        {
            if (!NetworkUtil.IsConnected()) throw new GameServiceException("Network Unreachable");
            if (Configuration == null) throw new GameServiceException("You Must Configuration First");
            if (string.IsNullOrEmpty(smsCode)) throw new GameServiceException("smsCode Cant Be EmptyOrNull");
            if (IsAuthenticated()) Logout();

            var login = await ApiRequest.LoginWithPhoneNumber(nickName,phoneNumber,smsCode);
            UserToken = login.Token;
            var auth = await ApiRequest.Authorize();
            CommandInfo = auth.CommandInfo;
            PlayToken = auth.Token;
            CurrentGame = auth.Game;
            _isAvailable = true;
            IsGuest = false;
            await Core.GSLive.GSLive.Init();
            return UserToken;
        }


        /// <summary>
        ///     Login To Game Service As Guest
        ///     It May Throw Exception
        /// </summary>
        public static async Task Login()
        {
            if (!NetworkUtil.IsConnected()) throw new GameServiceException("Network Unreachable");
            if (Configuration == null) throw new GameServiceException("You Must Configuration First");
            if (IsAuthenticated()) Logout();

            var login = await ApiRequest.LoginAsGuest();
            UserToken = login.Token;
            var auth = await ApiRequest.Authorize();
            PlayToken = auth.Token;
            CurrentGame = auth.Game;
            _isAvailable = true;
            IsGuest = true;
            CoreEventHandlers.SuccessfullyLogined?.Invoke(null, null);
        }

        /// <summary>
        ///     Normal SignUp To Game Service
        ///     It May Throw Exception
        /// </summary>
        /// <value> return UserToken if SignUp Successfully </value>
        public static async Task<string> SignUp(string nickName, string email, string password)
        {
            if (!NetworkUtil.IsConnected()) throw new GameServiceException("Network Unreachable");
            if (Configuration == null) throw new GameServiceException("You Must Configuration First");
            if (string.IsNullOrEmpty(nickName)) throw new GameServiceException("NickName Cant Be EmptyOrNull");
            if (string.IsNullOrEmpty(email)) throw new GameServiceException("Email Cant Be EmptyOrNull");
            if (string.IsNullOrEmpty(password)) throw new GameServiceException("Password Cant Be EmptyOrNull");
            if (IsAuthenticated()) Logout();

            var login = await ApiRequest.SignUp(nickName, email, password);
            UserToken = login.Token;
            var auth = await ApiRequest.Authorize();
            CommandInfo = auth.CommandInfo;
            PlayToken = auth.Token;
            CurrentGame = auth.Game;
            _isAvailable = true;
            IsGuest = false;
            await Core.GSLive.GSLive.Init();
            return UserToken;
        }


        /// <summary>
        ///     Check if Current User Authenticated
        /// </summary>
        /// <value> return True if Current User Authenticated Before </value>
        public static bool IsAuthenticated()
        {
            return _isAvailable;
        }


        /// <summary>
        ///     Get The Current GameService Version
        /// </summary>
        /// <value> return The Current GameService Version </value>
        public static string Version() => "5.3.0";
        


        /// <summary>
        ///     Logout From Game Service
        /// </summary>
        public static void Logout()
        {
            UserToken = null;
            CurrentGame = null;
            PlayToken = null;
            CommandInfo = null;
            _isAvailable = false;
            IsGuest = false;
            GSLive?.Dispose();
            GsWebRequest.Dispose();
        }

        #region GameServiceRegion

        private const string Tag = "FiroozehGameService";
        private static bool _isAvailable;
        public static event EventHandler<Notification> NotificationReceived;

        internal static string UserToken;
        internal static string PlayToken;
        internal static Game CurrentGame;
        internal static CommandInfo CommandInfo;
        internal static bool IsGuest;
        internal static SynchronizationContext SynchronizationContext;
        internal static GameServiceClientConfiguration Configuration { get; private set; }

        public static GSLive.GSLive GSLive { get; private set; }
        private static DownloadManager _downloadManager;

        #endregion
    }
}