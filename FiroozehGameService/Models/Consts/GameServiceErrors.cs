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
using FiroozehGameService.Models.BasicApi;

/**
* @author Alireza Ghodrati
*/


namespace FiroozehGameService.Models.Consts
{
    /// <summary>
    ///     Represents All GameService Errors
    /// </summary>
    public static class GameServiceErrors
    {
        /// <summary>
        ///     Represents All GameService Errors in HTTP Requests
        /// </summary>
        public static class Http
        {
            /// <summary>
            ///     Represents GameService HTTP Internal Errors
            /// </summary>
            public class Internal
            {
                public const string InternalError = "internal_error";
                public const string InvalidInput = "invalid_input";
                public const string Limited = "Limited";
            }


            /// <inheritdoc />
            /// <summary>
            ///     Represents GameService Authorize HTTP Errors
            ///     Errors that occur in the following Functions :
            ///     <see cref="GameService.Login()" />
            ///     <see cref="GameService.Login(string)" />
            ///     <see cref="GameService.Login(string,string)" />
            ///     <see cref="GameService.SignUp(string,string,string)" />
            /// </summary>
            public class Authorize : Internal
            {
                public const string GameNotfound = "game_notfound";
                public const string UserNotfound = "user_notfound";
                public const string MemberNotfound = "member_notfound";
                public const string UserBanned = "user_banned";
                public const string PlanLimit = "plan_limit";


                /// <summary>
                ///     These errors may never happen!
                /// </summary>
                public const string TokenRequired = "token_required";

                public const string TokenExpired = "token_expired";
            }


            /// <inheritdoc />
            /// <summary>
            ///     Represents GameService LoginOrSignUp HTTP Errors
            ///     Errors that occur in the following Functions :
            ///     <see cref="GameService.Login()" />
            ///     <see cref="GameService.Login(string)" />
            ///     <see cref="GameService.Login(string,string)" />
            ///     <see cref="GameService.SignUp(string,string,string)" />
            /// </summary>
            public class LoginOrSignUp : Authorize
            {
                public const string WrongPassword = "wrong_password";
                public const string UsedEmail = "used_email";
            }



            /// <inheritdoc />
            /// <summary>
            ///     Represents GameService LastLoginInfo HTTP Errors
            ///     Errors that occur in the following Functions :
            ///     <see cref="GameService.LastLoginInfo()" />
            /// </summary>
            public class LastLoginInfo : Authorize
            {
                public const string UserNotFound = "user_notfound";
                public const string MemberNotfound = "member_notfound";
                public const string GameNotfound = "game_notfound";
            }


            /// <inheritdoc />
            /// <summary>
            ///     Represents GameService Save HTTP Errors
            ///     Errors that occur in the following Functions :
            ///     <see cref="GameService.SaveGame(string,object)" />
            ///     <see cref="GameService.GetSaveGame{T}()" />
            ///     <see cref="GameService.RemoveSave()" />
            /// </summary>
            public class Save : Internal
            {
                public const string SaveNotfound = "save_notfound";
            }


            /// <inheritdoc />
            /// <summary>
            ///     Represents GameService Achievement HTTP Errors
            ///     Errors that occur in the following Functions :
            ///     <see cref="GameService.GetAchievements()" />
            ///     <see cref="GameService.UnlockAchievement(string)" />
            /// </summary>
            public class Achievement : Internal
            {
                public const string UnlockedBefore = "has_been_unlocked";
                public const string AchievementNotfound = "achievement_notfound";
                public const string GameNotfound = "game_notfound";
            }


            /// <inheritdoc />
            /// <summary>
            ///     Represents GameService Leaderboard HTTP Errors
            ///     Errors that occur in the following Functions :
            ///     <see cref="GameService.GetLeaderBoards()" />
            ///     <see cref="GameService.GetLeaderBoardDetails(string,int)" />
            ///     <see cref="GameService.GetMyScore()" />
            ///     <see cref="GameService.SubmitScore(string,int)" />
            /// </summary>
            public class Leaderboard : Internal
            {
                public const string LeaderboardNotfound = "leaderboard_notfound";
                public const string ScoreNotfound = "score_notfound";
                public const string GameNotfound = "game_notfound";
            }


            /// <summary>
            ///     Represents GameService DownloadAssets HTTP Errors
            ///     Errors that occur in the following Functions:
            ///     <see cref="GameService.DownloadAsset(string,string)" />
            ///     <see cref="GameService.DownloadAsset(string)" />
            ///     And You Can Handle Them in <see cref="DownloadEventHandlers.DownloadError" />
            /// </summary>
            public class DownloadAssets
            {
                public const string DatapackNotfound = "datapack_notfound";
                public const string GameNotfound = "game_notfound";
            }


            /// <summary>
            ///     Represents GameService Bucket HTTP Errors
            ///     Errors that occur in the following Functions:
            ///     <see cref="GameService.AddBucketItem{TBucket}" />
            ///     <see cref="GameService.GetBucketItem{TBucket}" />
            ///     <see cref="GameService.GetBucketItems{TBucket}" />
            ///     <see cref="GameService.DeleteBucketItem" />
            ///     <see cref="GameService.DeleteBucketItems" />
            ///     <see cref="GameService.UpdateBucketItem{TBucket}" />
            /// </summary>
            public class Bucket : Internal
            {
                public const string BucketNotfound = "bucket_notfound";
                public const string PermissionDenied = "permission_denied";
                public const string PlanLimit = "plan_limit";
                public const string AccountNotfound = "account_notfound";
                public const string InvalidStructure = "invalid_structure";
            }


            /// <summary>
            ///     Represents GameService EditCurrentPlayer HTTP Errors
            ///     Errors that occur in the following Functions:
            ///     <see cref="GameService.EditCurrentPlayerProfile(EditUserProfile)" />
            /// </summary>
            public class EditCurrentPlayer
            {
                public const string UserNotFound = "user_notfound";
            }
        }

        /// <summary>
        ///     Represents All GameService Errors in Command Requests
        /// </summary>
        public static class Command{
            
            /// <summary>
            ///     Represents GameService Command Internal Errors
            /// </summary>
            public class Internal
            {
                public const string NotFound = "DB_ERROR";
                public const string InvalidInput = "INVALID_INPUT";
                public const string Limited = "Limited";
            }

            /// <inheritdoc />
            /// <summary>
            ///     Represents GameService Authorize Command Errors
            ///     Errors that occur in initial connection
            /// </summary>
            public class Authorize : Internal
            {
                public const string GameNotfound = "game_notfound";
                public const string UserNotfound = "user_notfound";
                public const string MemberNotfound = "member_notfound";
                public const string UserBanned = "user_banned";
                public const string PlanLimit = "plan_limit";
                public const string GameNotActive = "game_notactive";
            }

            /// <summary>
            ///     Represents GameService AutoMatch Errors
            ///     Errors that occur in the following Functions:
            ///     <see cref="GameService.AutoMatch(object)" />
            /// </summary>
            public class AutoMatch
            {
                public const string PlanLimit = "plan_limit";
                public const string InvalidID = "INVALID_ID";
            }

            /// <summary>
            ///     Represents GameService CancelAutoMatch Errors
            ///     Errors that occur in the following Functions:
            ///     <see cref="GameService.CancelAutoMatch()" />
            /// </summary>
            public class CancelAutoMatch
            {
                public const string NotInWaitingQueue = "not_in_q";
            }

            /// <summary>
            ///     Represents GameService CreateRoom Errors
            ///     Errors that occur in the following Functions:
            ///     <see cref="GameService.CreateRoom()" />
            /// </summary>
            public class CreateRoom
            {
                public const string PlanLimit = "plan_limit";
                public const string InvalidID = "INVALID_ID";
            }

            /// <summary>
            ///     Represents GameService GetAvailableRooms Errors
            ///     Errors that occur in the following Functions:
            ///     <see cref="GameService.GetAvailableRooms(string)" />
            /// </summary>
            public class GetAvailableRooms
            {
                public const string InvalidID = "INVALID_ID";
            }

            /// <summary>
            ///     Represents GameService JoinRoom Errors
            ///     Errors that occur in the following Functions:
            ///     <see cref="GameService.JoinRoom(string)" />
            /// </summary>
            public class JoinRoom
            {
                public const string InvalidID = "INVALID_ID";
                public const string WrongPassword = "wrong_password";
                public const string RoomIsFull = "room_full";
            }

            /// <summary>
            ///     Represents GameService InviteUser Errors
            ///     Errors that occur in the following Functions:
            ///     <see cref="GameService.InviteUser(string, string)" />
            /// </summary>
            public class InviteUser
            {
                public const string InvalidID = "INVALID_ID";
                public const string RoomIsFull = "room_full";
                public const string PermissionDenied = "permission_denid";
                
            }
            
            /// <summary>
            ///     Represents GameService GetInviteInbox Errors
            ///     Errors that occur in the following Functions:
            ///     <see cref="GameService.GetInviteInbox()" />
            /// </summary>
            public class GetInviteInbox
            {
                public const string InvalidID = "INVALID_ID";                
            }
            
            /// <summary>
            ///     Represents GameService AcceptInvite Errors
            ///     Errors that occur in the following Functions:
            ///     <see cref="GameService.AcceptInvite(string)" />
            /// </summary>
            public class AcceptInvite
            {
                public const string InvalidID = "INVALID_ID";                
            }

            /// <summary>
            ///     Represents GameService SubscribeChannel Errors
            ///     Errors that occur in the following Functions:
            ///     <see cref="GameService.SubscribeChannel(string)" />
            /// </summary>
            public class SubscribeChannel
            {
                public const string InvalidID = "INVALID_ID";                
            }

            /// <summary>
            ///     Represents GameService SendChannelMessage Errors
            ///     Errors that occur in the following Functions:
            ///     <see cref="GameService.SendChannelMessage(string, string)" />
            /// </summary>
            public class SendChannelMessage
            {
                public const string NotMemberBefore = "not_member_before";                
            }

            /// <summary>
            ///     Represents GameService UnSubscribeChannel Errors
            ///     Errors that occur in the following Functions:
            ///     <see cref="GameService.UnSubscribeChannel(string)" />
            /// </summary>
            public class UnSubscribeChannel
            {
                public const string NotMemberBefore = "not_member_before";                
            }
        }  

        /// <summary>
        ///     Represents All GameService Errors in TurnBase Requests
        /// </summary>
        public static class TurnBase{
            /// <summary>
            ///     Represents GameService Turnbase Internal Errors
            /// </summary>
            public class Internal
            {
                public const string NotFound = "DB_ERROR";
                public const string InvalidInput = "INVALID_INPUT";
                public const string Limited = "Limited";
                public const string PermissionDenied = "permission_denid";
            }

            /// <inheritdoc />
            /// <summary>
            ///     Represents GameService Authorize TurnBase Errors
            ///     Errors that occur in initial connection
            /// </summary>
            public class Authorize : Internal
            {
                public const string JWTError = "JWT_ERROR";
                public const string NotFound = "DB_ERROR";
                public const string InvalidID = "INVALID_ID";
                public const string ObjectNotfound = "OBJECT_NOTFOUND";
                public const string NotMemberOfRoom = "not_member";
            }

            /// <summary>
            ///     Represents GameService ModifyValue Errors
            ///     Errors that occur in the following Functions:
            ///     <see cref="GameService.ModifyValue(string)" />
            /// </summary>
            public class ModifyValue
            {
                public const string PropertyReached = "property_reached";                
            }
        }      

        /// <summary>
        ///     Represents All GameService Errors in Realtime Requests
        /// </summary>
        public static class Realtime{
/// <summary>
            ///     Represents GameService Realtime Internal Errors
            /// </summary>
            public class Internal
            {
                public const string NotFound = "DB_ERROR";
                public const string InvalidInput = "INVALID_INPUT";
                public const string Limited = "Limited";
                public const string PermissionDenied = "permission_denid";
            }

            /// <inheritdoc />
            /// <summary>
            ///     Represents GameService Authorize Realtime Errors
            ///     Errors that occur in initial connection
            /// </summary>
            public class Authorize : Internal
            {
                public const string JWTError = "JWT_ERROR";
                public const string NotFound = "DB_ERROR";
                public const string InvalidRoom = "INVALID_ROOM";
                public const string InvalidID = "INVALID_ID";
                public const string ObjectNotfound = "OBJECT_NOTFOUND";
                public const string NotMemberOfRoom = "not_member";
            }

            /// <summary>
            ///     Represents GameService Instantitate Errors
            ///     Errors that occur in the following Functions:
            ///     <see cref="GameService.Instantitate(string)" />
            /// </summary>
            public class Instantitate
            {
                public const string CapacityError = "capacity_error";                
            }
            
            /// <summary>
            ///     Represents GameService RunFunction Errors
            ///     Errors that occur in the following Functions:
            ///     <see cref="GameService.RunFunction(string, Object, []Object)" />
            /// </summary>
            public class RunFunction
            {
                public const string CapacityError = "capacity_error";                
            }

            /// <summary>
            ///     Represents GameService SetProperty Errors
            ///     Errors that occur in the following Functions:
            ///     <see cref="GameService.SetProperty(Object)" />
            /// </summary>
            public class SetProperty
            {
                public const string CapacityError = "capacity_error";                
            }

        }
    }
}