// <copyright file="GameServiceErrors.cs" company="Firoozeh Technology LTD">
// Copyright (C) 2020 Firoozeh Technology LTD. All Rights Reserved.
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



using FiroozehGameService.Core;
using FiroozehGameService.Handlers;

/**
* @author Alireza Ghodrati
*/


namespace FiroozehGameService.Models.Consts
{
    /// <summary>
    /// Represents All GameService Errors
    /// </summary>
    public static class GameServiceErrors
    {
        /// <summary>
        /// Represents All GameService Errors in HTTP Requests
        /// </summary>
        public static class Http
        {
            /// <summary>
            /// Represents GameService HTTP Internal Errors
            /// </summary>
            public class Internal
            {
                public const string InternalError = "internal_error";
                public const string InvalidInput = "invalid_input";
                public const string Limited = "Limited";
            }
            
            
            /// <inheritdoc />
            /// <summary>
            /// Represents GameService Authorize HTTP Errors
            /// Errors that occur in the following Functions :
            /// <see cref="GameService.Login()"/>
            /// <see cref="GameService.Login(string)"/>
            /// <see cref="GameService.Login(string,string)"/>
            /// <see cref="GameService.SignUp(string,string,string)"/>
            /// </summary>
            public class Authorize : Internal
            {
                public const string GameNotfound = "game_notfound";
                public const string UserNotfound = "user_notfound";
                public const string MemberNotfound = "member_notfound";
                public const string UserBanned = "user_banned";
                public const string PlanLimit = "plan_limit";
                
                
                /// <summary>
                /// These errors may never happen!
                /// </summary>
                public const string TokenRequired = "token_required";
                public const string TokenExpired = "token_expired";
            }
            
            
            /// <inheritdoc />
            /// <summary>
            /// Represents GameService LoginOrSignUp HTTP Errors
            /// Errors that occur in the following Functions :
            /// <see cref="GameService.Login()"/>
            /// <see cref="GameService.Login(string)"/>
            /// <see cref="GameService.Login(string,string)"/>
            /// <see cref="GameService.SignUp(string,string,string)"/>
            /// </summary>
            public class LoginOrSignUp : Authorize
            {
                public const string WrongPassword = "wrong_password";
                public const string UsedEmail = "used_email";
            }
            
            
            /// <inheritdoc />
            /// <summary>
            /// Represents GameService Save HTTP Errors
            /// Errors that occur in the following Functions :
            /// <see cref="GameService.SaveGame(string,string,object)"/>
            /// <see cref="GameService.GetSaveGame{T}()"/>
            /// </summary>
            public class Save : Internal
            {
                public const string SaveNotfound = "save_notfound";
            }
            
            
            
            /// <inheritdoc />
            /// <summary>
            /// Represents GameService Achievement HTTP Errors
            /// Errors that occur in the following Functions :
            /// <see cref="GameService.GetAchievements()"/>
            /// <see cref="GameService.UnlockAchievement(string)"/>
            /// </summary>
            public class Achievement : Internal
            {
                public const string UnlockedBefore = "has_been_unlocked";
                public const string AchievementNotfound = "achievement_notfound";
            }
            
            
            
            /// <inheritdoc />
            /// <summary>
            /// Represents GameService Leaderboard HTTP Errors
            /// Errors that occur in the following Functions :
            /// <see cref="GameService.GetLeaderBoards()"/>
            /// <see cref="GameService.GetLeaderBoardDetails(string)"/>
            /// </summary>
            public class Leaderboard : Internal
            {
                public const string LeaderboardNotfound = "leaderboard_notfound";
                public const string ScoreNotfound = "score_notfound";
            }
            
            
            
            /// <summary>
            /// Represents GameService DownloadAssets HTTP Errors
            /// Errors that occur in the following Functions:
            /// <see cref="GameService.DownloadAsset(string,string)"/>
            /// <see cref="GameService.DownloadAsset(string)"/>
            /// And You Can Handle Them in <see cref="DownloadEventHandlers.DownloadError"/>
            /// </summary>
            public class DownloadAssets
            {
                public const string DatapackNotfound = "datapack_notfound";
            }
            
            
            
            /// <summary>
            /// Represents GameService Bucket HTTP Errors
            /// Errors that occur in the following Functions:
            /// <see cref="GameService.AddBucketItem{TBucket}"/>
            /// <see cref="GameService.GetBucketItem{TBucket}"/>
            /// <see cref="GameService.GetBucketItems{TBucket}"/>
            /// <see cref="GameService.DeleteBucketItem"/>
            /// <see cref="GameService.DeleteBucketItems"/>
            /// <see cref="GameService.UpdateBucketItem{TBucket}"/>
            /// </summary>
            public class Bucket : Internal
            {
                public const string BucketNotfound = "bucket_notfound";
                public const string PermissionDenied = "permission_denied";
                public const string PlanLimit = "plan_limit";
                public const string AccountNotfound = "account_notfound";
                public const string InvalidStructure = "invalid_structure";
            }
            
        }
        
        
        /// <summary>
        /// Represents All GameService Errors in GsLive
        /// </summary>
        public static class GSLive
        {
            public class Command
            {
                public const string PlanLimit = "plan_limit";
                public const string DbError = "DB_ERROR";
                public const string JwtError = "JWT_ERROR";
                public const string InvalidInput = "invalid_input";
                public const string ObjectNotFound = "OBJECT_NOTFOUND";
                public const string InvalidId = "INVALID_ID";
                public const string InvalidRoom = "INVALID_ROOM";
                public const string RoomFull = "room_full";
                public const string PermissionDenied = "permission_denied";
            }
            public class Chat
            {
                public const string AlreadySubscribed = "already_subscribed";
                public const string NotMemberBefore = "not_member_before";
            }
        }
    }
}