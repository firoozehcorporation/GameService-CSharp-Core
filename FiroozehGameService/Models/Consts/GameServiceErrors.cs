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


/**
* @author Alireza Ghodrati
*/


using FiroozehGameService.Core.Providers.GSLive;
using FiroozehGameService.Handlers;

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
                public const string Timeout = "request_timeout";
            }


            /// <inheritdoc />
            /// <summary>
            ///     Represents GameService Authorize HTTP Errors
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
            /// </summary>
            public class LoginOrSignUp : Authorize
            {
                public const string WrongPassword = "wrong_password";
                public const string UsedEmail = "used_email";
            }


            /// <inheritdoc />
            /// <summary>
            ///     Represents GameService GetLastLoginMemberInfo HTTP Errors
            /// </summary>
            public class GetLastLoginMemberInfo : Authorize
            {
                public const string UserNotFound = "user_notfound";
                public const string MemberNotfound = "member_notfound";
                public const string GameNotfound = "game_notfound";
            }


            /// <inheritdoc />
            /// <summary>
            ///     Represents GameService Save HTTP Errors
            ///     Errors that occur in the following Functions :
            /// </summary>
            public class Save : Internal
            {
                public const string SaveNotfound = "save_notfound";
                public const string SaveLimit = "save_limited";
            }


            /// <inheritdoc />
            /// <summary>
            ///     Represents GameService Achievement HTTP Errors
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
            /// </summary>
            public class Leaderboard : Internal
            {
                public const string LeaderboardNotfound = "leaderboard_notfound";
                public const string ScoreNotfound = "score_notfound";
                public const string GameNotfound = "game_notfound";
            }


            /// <summary>
            ///     Represents GameService DownloadAssets HTTP Errors
            ///     And You Can Handle Them in <see cref="DownloadEventHandlers.DownloadError" />
            /// </summary>
            public class DownloadAssets
            {
                public const string DataPackNotfound = "datapack_notfound";
                public const string GameNotfound = "game_notfound";
            }


            /// <summary>
            ///     Represents GameService Table HTTP Errors
            /// </summary>
            public class Table : Internal
            {
                public const string TableNotfound = "bucket_notfound";
                public const string PermissionDenied = "permission_denied";
                public const string PlanLimit = "plan_limit";
                public const string AccountNotfound = "account_notfound";
                public const string InvalidStructure = "invalid_structure";

                public const string objectNotfound = "object_notfound";
                public const string colNotfound = "col_notfound";
                public const string operationLimited = "operation_limited";
            }


            /// <summary>
            ///     Represents GameService EditCurrentPlayer HTTP Errors
            /// </summary>
            public class EditCurrentPlayer
            {
                public const string UserNotFound = "user_notfound";
            }

            /// <summary>
            ///     Represents GameService ChangePassword HTTP Errors
            /// </summary>
            public class ChangePassword
            {
                public const string UserNotFound = "user_notfound";
                public const string SamePassword = "same_password";
                public const string WrongPassword = "wrong_password";
            }

            /// <summary>
            ///     Represents GameService RevokeActiveDevice HTTP Errors
            /// </summary>
            public class RevokeActiveDevice
            {
                public const string DeviceNotfound = "device_notfound";
            }

            /// <summary>
            ///     Represents GameService CloudComputing HTTP Errors
            /// </summary>
            public class CloudComputing : Internal
            {
                public const string InvalidRequest = "invalid_request";
                public const string FunctionNotPublic = "function_not_public";
                public const string FunctionTimeout = "function_timeout";
                public const string InvalidStructure = "invalid_structure";
                public const string ApplicationNotFound = "no_running_application";
            }

            /// <summary>
            ///     Represents GameService MemberTag HTTP Errors
            /// </summary>
            public class MemberTag
            {
                public const string TagNotfound = "tag_notfound";
            }
        }

        /// <summary>
        ///     Represents All GameService Errors in Social Requests
        /// </summary>
        public static class Social
        {
            /// <summary>
            ///     Represents All GameService Friend Errors in Social Requests
            /// </summary>
            public static class Friend
            {
                /// <summary>
                ///     Represents GameService SendFriendRequest HTTP Errors
                ///     Errors that occur in the following Functions:
                ///     <see cref="Core.Social.Friend.SendFriendRequest" />
                /// </summary>
                public class SendFriendRequest
                {
                    public const string IsFriendBefore = "is_friend_before";
                }

                /// <summary>
                ///     Represents GameService AcceptFriendRequest HTTP Errors
                ///     Errors that occur in the following Functions:
                ///     <see cref="Core.Social.Friend.AcceptFriendRequest" />
                /// </summary>
                public class AcceptFriendRequest
                {
                    public const string NotFriendBefore = "not_friend_before";
                }

                /// <summary>
                ///     Represents GameService DeleteFriend HTTP Errors
                ///     Errors that occur in the following Functions:
                ///     <see cref="Core.Social.Friend.DeleteFriend" />
                /// </summary>
                public class DeleteFriend
                {
                    public const string NotFriendBefore = "not_friend_before";
                }
            }


            /// <summary>
            ///     Represents All GameService Party Errors in Social Requests
            /// </summary>
            public static class Party
            {
                /// <summary>
                ///     Represents GameService GetPartyInfo HTTP Errors
                ///     Errors that occur in the following Functions:
                ///     <see cref="Core.Social.Party.GetPartyInfo" />
                /// </summary>
                public class GetPartyInfo
                {
                    public const string MemberNotfound = "member_notfound";
                }

                /// <summary>
                ///     Represents GameService EditParty HTTP Errors
                ///     Errors that occur in the following Functions:
                ///     <see cref="Core.Social.Party.EditParty" />
                /// </summary>
                public class EditParty
                {
                    public const string PermissionDenied = "permission_denid";
                }

                /// <summary>
                ///     Represents GameService AddFriend HTTP Errors
                ///     Errors that occur in the following Functions:
                ///     <see cref="Core.Social.Party.AddFriend" />
                /// </summary>
                public class AddFriend
                {
                    public const string PermissionDenied = "permission_denid";
                    public const string PartyIsFull = "party_isfull";
                    public const string IsMemberBefore = "is_member_before";
                    public const string NotFriendBefore = "not_friend_before";
                }

                /// <summary>
                ///     Represents GameService JoinRequest HTTP Errors
                ///     Errors that occur in the following Functions:
                ///     <see cref="Core.Social.Party.JoinRequest" />
                /// </summary>
                public class JoinRequest
                {
                    public const string PartyNotfound = "party_notfound";
                    public const string IsMemberBefore = "is_member_before";
                }

                /// <summary>
                ///     Represents GameService AcceptJoinRequest HTTP Errors
                ///     Errors that occur in the following Functions:
                ///     <see cref="Core.Social.Party.AcceptJoinRequest" />
                /// </summary>
                public class AcceptJoinRequest
                {
                    public const string PartyNotfound = "party_notfound";
                    public const string MemberNotfound = "member_notfound";
                    public const string PermissionDenied = "permission_denid";
                }

                /// <summary>
                ///     Represents GameService RejectJoinRequest HTTP Errors
                ///     Errors that occur in the following Functions:
                ///     <see cref="Core.Social.Party.RejectJoinRequest" />
                /// </summary>
                public class RejectJoinRequest
                {
                    public const string PartyNotfound = "party_notfound";
                    public const string MemberNotfound = "member_notfound";
                    public const string PermissionDenied = "permission_denid";
                }

                /// <summary>
                ///     Represents GameService LeftParty HTTP Errors
                ///     Errors that occur in the following Functions:
                ///     <see cref="Core.Social.Party.LeftParty" />
                /// </summary>
                public class LeftParty
                {
                    public const string PartyNotfound = "party_notfound";
                    public const string IsMemberBefore = "is_member_before";
                }

                /// <summary>
                ///     Represents GameService KickMember HTTP Errors
                ///     Errors that occur in the following Functions:
                ///     <see cref="Core.Social.Party.KickMember" />
                /// </summary>
                public class KickMember
                {
                    public const string PermissionDenied = "permission_denid";
                }

                /// <summary>
                ///     Represents GameService SetOrUpdateRole HTTP Errors
                ///     Errors that occur in the following Functions:
                ///     <see cref="Core.Social.Party.SetOrUpdateRole(string, string, string)" />
                /// </summary>
                public class SetOrUpdateRole
                {
                    public const string PermissionDenied = "permission_denid";
                }

                /// <summary>
                ///     Represents GameService SetOrUpdateMemberVariable HTTP Errors
                ///     Errors that occur in the following Functions:
                ///     <see cref="Core.Social.Party.SetOrUpdateMemberVariable" />
                /// </summary>
                public class SetOrUpdateMemberVariable
                {
                    public const string MemberNotfound = "member_notfound";
                }

                /// <summary>
                ///     Represents GameService DeleteVariable HTTP Errors
                ///     Errors that occur in the following Functions:
                ///     <see cref="Core.Social.Party.DeleteVariable" />
                /// </summary>
                public class DeleteMemberVariable
                {
                    public const string MemberNotfound = "member_notfound";
                }

                /// <summary>
                ///     Represents GameService SetOrUpdateVariable HTTP Errors
                ///     Errors that occur in the following Functions:
                ///     <see cref="Core.Social.Party.SetOrUpdateVariable" />
                /// </summary>
                public class SetOrUpdateVariable
                {
                    public const string PermissionDenied = "permission_denid";
                }

                /// <summary>
                ///     Represents GameService DeleteVariable HTTP Errors
                ///     Errors that occur in the following Functions:
                ///     <see cref="Core.Social.Party.DeleteVariable" />
                /// </summary>
                public class DeleteVariable
                {
                    public const string PermissionDenied = "permission_denid";
                }

                /// <summary>
                ///     Represents GameService DeleteVariables HTTP Errors
                ///     Errors that occur in the following Functions:
                ///     <see cref="Core.Social.Party.DeleteVariables" />
                /// </summary>
                public class DeleteVariables
                {
                    public const string PermissionDenied = "permission_denid";
                }

                /// <summary>
                ///     Represents GameService DeleteMemberVariables HTTP Errors
                ///     Errors that occur in the following Functions:
                ///     <see cref="Core.Social.Party.DeleteMemberVariables" />
                /// </summary>
                public class DeleteMemberVariables
                {
                    public const string MemberNotfound = "member_notfound";
                }

                /// <summary>
                ///     Represents GameService GetMemberVariables HTTP Errors
                ///     Errors that occur in the following Functions:
                ///     <see cref="Core.Social.Party.DeleteMemberVariables" />
                /// </summary>
                public class GetMemberVariables
                {
                    public const string VariableKeyNotfound = "variableKey_notfound";
                }
            }
        }

        /// <summary>
        ///     Represents All GameService Errors in GSLive Requests
        /// </summary>
        public static class GSLive
        {
            /// <summary>
            ///     Represents All GameService Internal Errors in GSLive
            /// </summary>
            public static class Internal
            {
                public const string GameNotfound = "game_notfound";
                public const string UserNotfound = "user_notfound";
                public const string MemberNotfound = "member_notfound";
                public const string UserBanned = "user_banned";
                public const string PlanLimit = "plan_limit";
                public const string GameNotActive = "game_notactive";
                public const string JWTError = "JWT_ERROR";
                public const string NotFound = "DB_ERROR";
                public const string InvalidID = "INVALID_ID";
                public const string ObjectNotfound = "OBJECT_NOTFOUND";
                public const string NotMemberOfRoom = "not_member";
            }

            /// <summary>
            ///     Represents All GameService Errors in Push Event Requests
            /// </summary>
            public static class PushEvent
            {
                /// <summary>
                ///     Represents GameService Push Event By Id Errors
                /// </summary>
                public class PushEventById
                {
                    public const string InvalidInput = "invalid_input";
                }

                /// <summary>
                ///     Represents GameService Push Event By Tag Errors
                /// </summary>
                public class PushEventByTag
                {
                    public const string InvalidInput = "invalid_input";
                }
            }

            /// <summary>
            ///     Represents All GameService Errors in Chat Requests
            /// </summary>
            public static class Chat
            {
                /// <summary>
                ///     Represents GameService SubscribeChannel Errors
                ///     Errors that occur in the following Functions:
                ///     <see cref="SubscribeChannel" />
                /// </summary>
                public class SubscribeChannel
                {
                    public const string InvalidID = "INVALID_ID";
                }

                /// <summary>
                ///     Represents GameService SendChannelMessage Errors
                ///     Errors that occur in the following Functions:
                ///     <see cref="SendChannelMessage" />
                /// </summary>
                public class SendChannelMessage
                {
                    public const string NotMemberBefore = "not_member_before";
                }

                /// <summary>
                ///     Represents GameService UnSubscribeChannel Errors
                ///     Errors that occur in the following Functions:
                ///     <see cref="UnSubscribeChannel" />
                /// </summary>
                public class UnSubscribeChannel
                {
                    public const string NotMemberBefore = "not_member_before";
                }

                /// <summary>
                ///     Represents GameService Remove Message Functions Errors
                ///     Errors that occur in the following Functions:
                /// </summary>
                public class RemoveMessage
                {
                    public const string MessageNotFound = "message_notfound";
                    public const string MemberNotfound = "member_notfound";
                }
            }


            /// <summary>
            ///     Represents All GameService Errors in Voice Requests
            /// </summary>
            internal static class Voice
            {
                /// <summary>
                ///     Represents GameService CreateVoiceChannel Error
                /// </summary>
                internal class CreateChannel
                {
                    internal const string VoiceChannelAlreadyExist = "VOICE_CHANNEL_EXIST";
                }

                /// <summary>
                ///     Represents GameService JoinVoiceChannel Error
                /// </summary>
                internal class JoinChannel
                {
                    internal const string VoiceChannelNotFound = "VOICE_CHANNEL_NOTFOUND";
                    internal const string VoiceChannelPermanentlyKicked = "VOICE_CHANNEL_PERMANENTLY_KICKED";
                    internal const string VoiceChannelIsFull = "VOICE_CHANNEL_IS_FULL";
                }

                /// <summary>
                ///     Represents GameService LeaveVoiceChannel Error
                /// </summary>
                internal class LeaveChannel
                {
                    internal const string VoiceChannelNotFound = "VOICE_CHANNEL_NOTFOUND";
                }

                /// <summary>
                ///     Represents GameService KickMember Error
                /// </summary>
                internal class KickMember
                {
                    internal const string VoiceChannelNotFound = "VOICE_CHANNEL_NOTFOUND";
                }


                /// <summary>
                ///     Represents GameService DestroyVoiceChannel Error
                /// </summary>
                internal class DestroyChannel
                {
                    internal const string VoiceChannelNotFound = "VOICE_CHANNEL_NOTFOUND";
                }

                /// <summary>
                ///     Represents GameService MuteMember Error
                /// </summary>
                internal class MuteMember
                {
                    internal const string VoiceChannelNotFound = "VOICE_CHANNEL_NOTFOUND";
                }

                /// <summary>
                ///     Represents GameService MuteMember Error
                /// </summary>
                internal class DeafenMember
                {
                    internal const string VoiceChannelNotFound = "VOICE_CHANNEL_NOTFOUND";
                }
            }

            /// <summary>
            ///     Represents All GameService Errors in TurnBase Requests
            /// </summary>
            public static class TurnBase
            {
                /// <summary>
                ///     Represents GameService AutoMatch Errors
                ///     Errors that occur in the following Functions:
                ///     <see cref="GsLiveTurnBased.AutoMatch" />
                /// </summary>
                public class AutoMatch
                {
                    public const string PlanLimit = "plan_limit";
                    public const string InvalidID = "INVALID_ID";
                }

                /// <summary>
                ///     Represents GameService CancelAutoMatch Errors
                ///     Errors that occur in the following Functions:
                ///     <see cref="GsLiveTurnBased.CancelAutoMatch" />
                /// </summary>
                public class CancelAutoMatch
                {
                    public const string NotInWaitingQueue = "not_in_q";
                }

                /// <summary>
                ///     Represents GameService CreateRoom Errors
                ///     Errors that occur in the following Functions:
                ///     <see cref="GsLiveTurnBased.CreateRoom" />
                /// </summary>
                public class CreateRoom
                {
                    public const string PlanLimit = "plan_limit";
                    public const string InvalidID = "INVALID_ID";
                }

                /// <summary>
                ///     Represents GameService GetAvailableRooms Errors
                ///     Errors that occur in the following Functions:
                ///     <see cref="GsLiveTurnBased.GetAvailableRooms" />
                /// </summary>
                public class GetAvailableRooms
                {
                    public const string InvalidID = "INVALID_ID";
                }

                /// <summary>
                ///     Represents GameService JoinRoom Errors
                ///     Errors that occur in the following Functions:
                ///     <see cref="GsLiveTurnBased.JoinRoom" />
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
                ///     <see cref="GsLiveTurnBased.InviteUser" />
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
                ///     <see cref="GsLiveTurnBased.GetInviteInbox" />
                /// </summary>
                public class GetInviteInbox
                {
                    public const string InvalidID = "INVALID_ID";
                }

                /// <summary>
                ///     Represents GameService AcceptInvite Errors
                ///     Errors that occur in the following Functions:
                ///     <see cref="GsLiveTurnBased.AcceptInvite" />
                /// </summary>
                public class AcceptInvite
                {
                    public const string InvalidID = "INVALID_ID";
                }

                /// <summary>
                ///     Represents GameService SetProperty Errors
                ///     Errors that occur in the following Functions:
                ///     <see cref="GsLiveTurnBased.SetOrUpdateProperty" />
                /// </summary>
                public class SetProperty
                {
                    public const string PropertyReached = "property_reached";
                }
            }

            /// <summary>
            ///     Represents All GameService Errors in Realtime Requests
            /// </summary>
            public static class Realtime
            {
                /// <summary>
                ///     Represents GameService AutoMatch Errors
                ///     Errors that occur in the following Functions:
                ///     <see cref="GsLiveRealTime.AutoMatch" />
                /// </summary>
                public class AutoMatch
                {
                    public const string PlanLimit = "plan_limit";
                    public const string InvalidID = "INVALID_ID";
                }

                /// <summary>
                ///     Represents GameService CancelAutoMatch Errors
                ///     Errors that occur in the following Functions:
                ///     <see cref="GsLiveRealTime.CancelAutoMatch" />
                /// </summary>
                public class CancelAutoMatch
                {
                    public const string NotInWaitingQueue = "not_in_q";
                }

                /// <summary>
                ///     Represents GameService CreateRoom Errors
                ///     Errors that occur in the following Functions:
                ///     <see cref="GsLiveRealTime.CreateRoom" />
                /// </summary>
                public class CreateRoom
                {
                    public const string PlanLimit = "plan_limit";
                    public const string InvalidID = "INVALID_ID";
                }

                /// <summary>
                ///     Represents GameService GetAvailableRooms Errors
                ///     Errors that occur in the following Functions:
                ///     <see cref="GsLiveRealTime.GetAvailableRooms" />
                /// </summary>
                public class GetAvailableRooms
                {
                    public const string InvalidID = "INVALID_ID";
                }

                /// <summary>
                ///     Represents GameService JoinRoom Errors
                ///     Errors that occur in the following Functions:
                ///     <see cref="GsLiveRealTime.JoinRoom" />
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
                ///     <see cref="GsLiveRealTime.InviteUser" />
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
                ///     <see cref="GsLiveRealTime.GetInviteInbox" />
                /// </summary>
                public class GetInviteInbox
                {
                    public const string InvalidID = "INVALID_ID";
                }

                /// <summary>
                ///     Represents GameService AcceptInvite Errors
                ///     Errors that occur in the following Functions:
                ///     <see cref="GsLiveRealTime.AcceptInvite" />
                /// </summary>
                public class AcceptInvite
                {
                    public const string InvalidID = "INVALID_ID";
                }

                /// <summary>
                ///     Represents GameService Instantitate Errors
                ///     Errors that occur in the following Functions:
                ///     <see cref="Instantitate" />
                /// </summary>
                public class Instantitate
                {
                    public const string CapacityError = "capacity_error";
                }

                /// <summary>
                ///     Represents GameService RunFunction Errors
                ///     Errors that occur in the following Functions:
                ///     <see cref="RunFunction" />
                /// </summary>
                public class RunFunction
                {
                    public const string CapacityError = "capacity_error";
                }

                /// <summary>
                ///     Represents GameService SetProperty Errors
                ///     Errors that occur in the following Functions:
                ///     <see cref="SetProperty" />
                /// </summary>
                public class SetProperty
                {
                    public const string CapacityError = "capacity_error";
                }
            }
        }
    }
}