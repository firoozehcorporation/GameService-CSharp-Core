// <copyright file="Party.cs" company="Firoozeh Technology LTD">
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


using System.Collections.Generic;
using System.Threading.Tasks;
using FiroozehGameService.Core.ApiWebRequest;
using FiroozehGameService.Models;
using FiroozehGameService.Models.BasicApi;
using FiroozehGameService.Models.BasicApi.Social;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.Internal;
using FiroozehGameService.Utils;
using PartyData = FiroozehGameService.Models.BasicApi.Social.Party;

namespace FiroozehGameService.Core.Social
{
    /// <summary>
    ///     Represents Party in GameService Social System
    /// </summary>
    public class Party
    {
        internal Party()
        {
        }


        /// <summary>
        ///     Create Party With Option Like : Name , description , logo
        /// </summary>
        /// <param name="option">(NOTNULL)Create Party Option</param>
        public async Task<PartyData> CreateParty(SocialOptions.PartyOption option)
        {
            if (!GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException<Party>(DebugLocation.Party,
                    "CreateParty");
            if (option == null)
                throw new GameServiceException("option Cant Be Null").LogException<Party>(DebugLocation.Party,
                    "CreateParty");

            return await ApiRequest.CreateParty(option);
        }


        /// <summary>
        ///     Send Join Request to Party With PartyID
        /// </summary>
        /// <param name="partyId">(NOTNULL)the Party id</param>
        public async Task<bool> JoinRequest(string partyId)
        {
            if (!GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException<Party>(DebugLocation.Party,
                    "JoinRequest");
            if (string.IsNullOrEmpty(partyId))
                throw new GameServiceException("partyId Cant Be EmptyOrNull").LogException<Party>(DebugLocation.Party,
                    "JoinRequest");

            return await ApiRequest.SendJoinRequestToParty(partyId);
        }


        /// <summary>
        ///     Left The Party With PartyID
        /// </summary>
        /// <param name="partyId">(NOTNULL)the Party id</param>
        public async Task<bool> LeftParty(string partyId)
        {
            if (!GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException<Party>(DebugLocation.Party,
                    "LeftParty");
            if (string.IsNullOrEmpty(partyId))
                throw new GameServiceException("partyId Cant Be EmptyOrNull").LogException<Party>(DebugLocation.Party,
                    "LeftParty");

            return await ApiRequest.LeftParty(partyId);
        }


        /// <summary>
        ///     Edit Party Data With Option Like : Name , description , logo
        ///     NOTE : Only Creator or Admins Can Call This Function
        /// </summary>
        /// <param name="partyId">(NOTNULL)The Party id</param>
        /// <param name="option">(NOTNULL)Create Party Option</param>
        public async Task<PartyData> EditParty(string partyId, SocialOptions.PartyOption option)
        {
            if (!GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException<Party>(DebugLocation.Party,
                    "EditParty");
            if (string.IsNullOrEmpty(partyId))
                throw new GameServiceException("partyId Cant Be EmptyOrNull").LogException<Party>(DebugLocation.Party,
                    "EditParty");
            if (option == null)
                throw new GameServiceException("option Cant Be Null").LogException<Party>(DebugLocation.Party,
                    "EditParty");

            return await ApiRequest.EditParty(partyId, option);
        }


        /// <summary>
        ///     SetOrUpdate Party Member Role
        ///     NOTE : Only Creator or Admins Can Call This Function
        /// </summary>
        /// <param name="partyId">(NOTNULL)The Party id</param>
        /// <param name="memberId">(NOTNULL)The Member id</param>
        /// <param name="role">(NOTNULL)(MIN = 5 , MAX = 32 characters)The Role</param>
        public async Task<PartyData> SetOrUpdateRole(string partyId, string memberId, string role)
        {
            if (!GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException<Party>(DebugLocation.Party,
                    "SetOrUpdateRole");
            if (string.IsNullOrEmpty(partyId))
                throw new GameServiceException("partyId Cant Be EmptyOrNull").LogException<Party>(DebugLocation.Party,
                    "SetOrUpdateRole");
            if (string.IsNullOrEmpty(memberId))
                throw new GameServiceException("memberId Cant Be EmptyOrNull").LogException<Party>(DebugLocation.Party,
                    "SetOrUpdateRole");
            if (string.IsNullOrEmpty(role))
                throw new GameServiceException("role Cant Be EmptyOrNull").LogException<Party>(DebugLocation.Party,
                    "SetOrUpdateRole");
            if (role.Length < 5 || role.Length > 32)
                throw new GameServiceException("invalid role").LogException<Party>(DebugLocation.Party,
                    "SetOrUpdateRole");

            return await ApiRequest.SetOrUpdateRole(partyId, memberId, role);
        }


        /// <summary>
        ///     SetOrUpdate Party Variable
        ///     NOTE : Only Creator or Admins Can Call This Function
        /// </summary>
        /// <param name="partyId">(NOTNULL)The Party id</param>
        /// <param name="variable">
        ///     (NOTNULL)The Variable ->
        ///     Name : (MIN = 5 , MAX = 32) , Value : (MIN = 1 ,MAX = 128) Characters
        /// </param>
        public async Task<PartyData> SetOrUpdateVariable(string partyId, KeyValuePair<string, string> variable)
        {
            if (!GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException<Party>(DebugLocation.Party,
                    "SetOrUpdateVariable");
            if (string.IsNullOrEmpty(partyId))
                throw new GameServiceException("partyId Cant Be EmptyOrNull").LogException<Party>(DebugLocation.Party,
                    "SetOrUpdateVariable");
            if (variable.Key.Length < 5 || variable.Key.Length > 32)
                throw new GameServiceException("invalid variable Key").LogException<Party>(DebugLocation.Party,
                    "SetOrUpdateVariable");
            if (variable.Value.Length < 1 || variable.Value.Length > 128)
                throw new GameServiceException("invalid variable Value").LogException<Party>(DebugLocation.Party,
                    "SetOrUpdateVariable");

            return await ApiRequest.SetOrUpdateVariable(partyId, variable);
        }


        /// <summary>
        ///     SetOrUpdate Party Member Variable
        ///     The Current Member Can Set Variable in Party
        /// </summary>
        /// <param name="partyId">(NOTNULL)The Party id</param>
        /// <param name="variable">
        ///     (NOTNULL)The Variable ->
        ///     Name : (MIN = 5 , MAX = 32) , Value : (MIN = 1 ,MAX = 128) Characters
        /// </param>
        public async Task<PartyMember> SetOrUpdateMemberVariable(string partyId, KeyValuePair<string, string> variable)
        {
            if (!GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException<Party>(DebugLocation.Party,
                    "SetOrUpdateVariable");
            if (string.IsNullOrEmpty(partyId))
                throw new GameServiceException("partyId Cant Be EmptyOrNull").LogException<Party>(DebugLocation.Party,
                    "SetOrUpdateVariable");
            if (variable.Key.Length < 5 || variable.Key.Length > 32)
                throw new GameServiceException("invalid variable Key").LogException<Party>(DebugLocation.Party,
                    "SetOrUpdateVariable");
            if (variable.Value.Length < 1 || variable.Value.Length > 128)
                throw new GameServiceException("invalid variable Value").LogException<Party>(DebugLocation.Party,
                    "SetOrUpdateVariable");

            return await ApiRequest.SetOrUpdateMemberVariable(partyId, variable);
        }


        /// <summary>
        ///     Delete Party Variable with Variable Key
        ///     NOTE : Only Creator or Admins Can Call This Function
        /// </summary>
        /// <param name="partyId">(NOTNULL)The Party id</param>
        /// <param name="variableKey">(NOTNULL)The Variable Key</param>
        public async Task<bool> DeleteVariable(string partyId, string variableKey)
        {
            if (!GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException<Party>(DebugLocation.Party,
                    "DeleteVariable");
            if (string.IsNullOrEmpty(partyId))
                throw new GameServiceException("partyId Cant Be EmptyOrNull").LogException<Party>(DebugLocation.Party,
                    "DeleteVariable");
            if (string.IsNullOrEmpty(variableKey))
                throw new GameServiceException("variableKey Cant Be EmptyOrNull").LogException<Party>(
                    DebugLocation.Party, "DeleteVariable");

            return await ApiRequest.DeleteVariable(partyId, variableKey);
        }


        /// <summary>
        ///     Delete Party Variables
        ///     NOTE : Only Creator or Admins Can Call This Function
        /// </summary>
        /// <param name="partyId">(NOTNULL)The Party id</param>
        public async Task<bool> DeleteVariables(string partyId)
        {
            if (!GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException<Party>(DebugLocation.Party,
                    "DeleteVariables");
            if (string.IsNullOrEmpty(partyId))
                throw new GameServiceException("partyId Cant Be EmptyOrNull").LogException<Party>(DebugLocation.Party,
                    "DeleteVariables");

            return await ApiRequest.DeleteVariables(partyId);
        }


        /// <summary>
        ///     Delete Party Member Variable with Variable Key
        ///     The Current Member Can Delete Own Variable in Party
        /// </summary>
        /// <param name="partyId">(NOTNULL)The Party id</param>
        /// <param name="variableKey">(NOTNULL)The Variable Key</param>
        public async Task<bool> DeleteMemberVariable(string partyId, string variableKey)
        {
            if (!GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException<Party>(DebugLocation.Party,
                    "DeleteMemberVariable");
            if (string.IsNullOrEmpty(partyId))
                throw new GameServiceException("partyId Cant Be EmptyOrNull").LogException<Party>(DebugLocation.Party,
                    "DeleteMemberVariable");
            if (string.IsNullOrEmpty(variableKey))
                throw new GameServiceException("variableKey Cant Be EmptyOrNull").LogException<Party>(
                    DebugLocation.Party, "DeleteMemberVariable");

            return await ApiRequest.DeleteMemberVariable(partyId, variableKey);
        }


        /// <summary>
        ///     Delete Party Member Variables
        ///     The Current Member Can Delete Own Variables in Party
        /// </summary>
        /// <param name="partyId">(NOTNULL)The Party id</param>
        public async Task<bool> DeleteMemberVariables(string partyId)
        {
            if (!GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException<Party>(DebugLocation.Party,
                    "DeleteMemberVariables");
            if (string.IsNullOrEmpty(partyId))
                throw new GameServiceException("partyId Cant Be EmptyOrNull").LogException<Party>(DebugLocation.Party,
                    "DeleteMemberVariables");

            return await ApiRequest.DeleteMemberVariables(partyId);
        }


        /// <summary>
        ///     Add a Friend To Party
        ///     NOTE : Only Creator or Admins Can Call This Function
        /// </summary>
        /// <param name="partyId">(NOTNULL)The Party id</param>
        /// <param name="memberId">(NOTNULL)The Friend Member id</param>
        public async Task<bool> AddFriend(string partyId, string memberId)
        {
            if (!GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException<Party>(DebugLocation.Party,
                    "AddFriend");
            if (string.IsNullOrEmpty(partyId))
                throw new GameServiceException("partyId Cant Be EmptyOrNull").LogException<Party>(DebugLocation.Party,
                    "AddFriend");
            if (string.IsNullOrEmpty(memberId))
                throw new GameServiceException("memberId Cant Be EmptyOrNull").LogException<Party>(DebugLocation.Party,
                    "AddFriend");

            return await ApiRequest.AddFriendToParty(partyId, memberId);
        }


        /// <summary>
        ///     Kick a Member From Party
        ///     NOTE : Only Creator or Admins Can Call This Function
        /// </summary>
        /// <param name="partyId">(NOTNULL)The Party id</param>
        /// <param name="memberId">(NOTNULL)The Member id</param>
        public async Task<bool> KickMember(string partyId, string memberId)
        {
            if (!GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException<Party>(DebugLocation.Party,
                    "KickMember");
            if (string.IsNullOrEmpty(partyId))
                throw new GameServiceException("partyId Cant Be EmptyOrNull").LogException<Party>(DebugLocation.Party,
                    "KickMember");
            if (string.IsNullOrEmpty(memberId))
                throw new GameServiceException("memberId Cant Be EmptyOrNull").LogException<Party>(DebugLocation.Party,
                    "KickMember");

            return await ApiRequest.KickMember(partyId, memberId);
        }


        /// <summary>
        ///     Accept Join a Member To The Party
        ///     NOTE : Only Creator or Admins Can Call This Function
        /// </summary>
        /// <param name="partyId">(NOTNULL)The Party id</param>
        /// <param name="memberId">(NOTNULL)The Member id</param>
        public async Task<bool> AcceptJoinRequest(string partyId, string memberId)
        {
            if (!GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException<Party>(DebugLocation.Party,
                    "AcceptJoinRequest");
            if (string.IsNullOrEmpty(partyId))
                throw new GameServiceException("partyId Cant Be EmptyOrNull").LogException<Party>(DebugLocation.Party,
                    "AcceptJoinRequest");
            if (string.IsNullOrEmpty(memberId))
                throw new GameServiceException("memberId Cant Be EmptyOrNull").LogException<Party>(DebugLocation.Party,
                    "AcceptJoinRequest");

            return await ApiRequest.AcceptJoinToParty(partyId, memberId);
        }


        /// <summary>
        ///     Reject Join Request To The Party
        ///     NOTE : Only Creator or Admins Can Call This Function
        /// </summary>
        /// <param name="partyId">(NOTNULL)The Party id</param>
        /// <param name="memberId">(NOTNULL)The Member id</param>
        public async Task<bool> RejectJoinRequest(string partyId, string memberId)
        {
            if (!GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException<Party>(DebugLocation.Party,
                    "RejectJoinRequest");
            if (string.IsNullOrEmpty(partyId))
                throw new GameServiceException("partyId Cant Be EmptyOrNull").LogException<Party>(DebugLocation.Party,
                    "RejectJoinRequest");
            if (string.IsNullOrEmpty(memberId))
                throw new GameServiceException("memberId Cant Be EmptyOrNull").LogException<Party>(DebugLocation.Party,
                    "RejectJoinRequest");

            return await ApiRequest.RejectJoinToParty(partyId, memberId);
        }


        /// <summary>
        ///     Find All Parties With Specific Party Name Query
        /// </summary>
        /// <param name="query">(NULLABLE) Party Name Query . NOTE : if set query null , return all Parties</param>
        /// <param name="skip">The Result Skips</param>
        /// <param name="limit">(MAX = 25)The Result Limits</param>
        public async Task<Results<PartyData>> FindParties(string query = null, int skip = 0, int limit = 25)
        {
            if (!GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException<Party>(DebugLocation.Party,
                    "FindParties");
            return await ApiRequest.GetAllParties(new QueryData(query, skip, limit).ToQueryString());
        }


        /// <summary>
        ///     Get Current Member Parties With Specific skip & limit
        /// </summary>
        /// <param name="skip">The Result Skips</param>
        /// <param name="limit">(Max = 25) The Result Limits</param>
        public async Task<Results<PartyData>> GetMyParties(int skip = 0, int limit = 25)
        {
            if (!GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException<Party>(DebugLocation.Party,
                    "GetMyParties");
            return await ApiRequest.GetMyParties(new QueryData(null, skip, limit).ToQueryString());
        }


        /// <summary>
        ///     Get Party Info With Specific partyId
        ///     NOTE : Only Party Members Can Get PartyInfo
        /// </summary>
        /// <param name="partyId">(NOTNULL)The Party id</param>
        public async Task<PartyInfo> GetPartyInfo(string partyId)
        {
            if (!GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException<Party>(DebugLocation.Party,
                    "GetPartyInfo");
            if (string.IsNullOrEmpty(partyId))
                throw new GameServiceException("partyId Cant Be EmptyOrNull").LogException<Party>(DebugLocation.Party,
                    "GetPartyInfo");
            return await ApiRequest.GetPartyInfo(partyId);
        }
    }
}