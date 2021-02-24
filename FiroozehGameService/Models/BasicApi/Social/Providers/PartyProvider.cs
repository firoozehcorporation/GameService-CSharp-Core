// <copyright file="PartyProvider.cs" company="Firoozeh Technology LTD">
// Copyright (C) 2021 Firoozeh Technology LTD. All Rights Reserved.
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
using FiroozehGameService.Core.Social;
using FiroozehGameService.Models.GSLive;
using PartyData = FiroozehGameService.Models.BasicApi.Social.Party;

namespace FiroozehGameService.Models.BasicApi.Social.Providers
{
    /// <summary>
    ///     Represents Party in GameService Social System Provider
    /// </summary>
    public abstract class PartyProvider
    {
        /// <summary>
        ///     Create Party With Option Like : Name , description , logo
        /// </summary>
        /// <param name="option">(NOTNULL)Create Party Option</param>
        public abstract Task<PartyData> CreateParty(SocialOptions.PartyOption option);


        /// <summary>
        ///     Send Join Request to Party With PartyID
        /// </summary>
        /// <param name="partyId">(NOTNULL)the Party id</param>
        public abstract Task<bool> JoinRequest(string partyId);


        /// <summary>
        ///     Leave The Party With PartyID
        /// </summary>
        /// <param name="partyId">(NOTNULL)the Party id</param>
        public abstract Task<bool> LeaveParty(string partyId);


        /// <summary>
        ///     Delete The Party With PartyID
        ///     NOTE : Only Creator or Admins Can Call This Function
        /// </summary>
        /// <param name="partyId">(NOTNULL)the Party id</param>
        public abstract Task<bool> DeleteParty(string partyId);


        /// <summary>
        ///     Edit Party Data With Option Like : Name , description , logo
        ///     NOTE : Only Creator or Admins Can Call This Function
        /// </summary>
        /// <param name="partyId">(NOTNULL)The Party id</param>
        /// <param name="option">(NOTNULL)Create Party Option</param>
        public abstract Task<PartyData> EditParty(string partyId, SocialOptions.PartyOption option);


        /// <summary>
        ///     SetOrUpdate Party Member Role
        ///     NOTE : Only Creator or Admins Can Call This Function
        /// </summary>
        /// <param name="partyId">(NOTNULL)The Party id</param>
        /// <param name="memberId">(NOTNULL)The Member id</param>
        /// <param name="role">(NOTNULL)(MIN = 5 , MAX = 32 characters)The Role</param>
        public abstract Task<bool> SetOrUpdateRole(string partyId, string memberId, string role);


        /// <summary>
        ///     SetOrUpdate Party Variable
        ///     NOTE : Only Creator or Admins Can Call This Function
        /// </summary>
        /// <param name="partyId">(NOTNULL)The Party id</param>
        /// <param name="variable">
        ///     (NOTNULL)The Variable ->
        ///     Name : (MIN = 5 , MAX = 32) , Value : (MIN = 1 ,MAX = 128) Characters
        /// </param>
        public abstract Task<PartyData> SetOrUpdateVariable(string partyId, KeyValuePair<string, string> variable);


        /// <summary>
        ///     SetOrUpdate Party Member Variable
        ///     The Current Member Can Set Variable in Party
        /// </summary>
        /// <param name="partyId">(NOTNULL)The Party id</param>
        /// <param name="variable">
        ///     (NOTNULL)The Variable ->
        ///     Name : (MIN = 5 , MAX = 32) , Value : (MIN = 1 ,MAX = 128) Characters
        /// </param>
        public abstract Task<bool> SetOrUpdateMemberVariable(string partyId, KeyValuePair<string, string> variable);

        /// <summary>
        ///     Get Current Member Variable with Variable Key
        /// </summary>
        /// <param name="partyId">(NOTNULL)The Party id</param>
        /// <param name="variableKey">(NOTNULL)The Variable Key</param>
        public abstract Task<string> GetMemberVariable(string partyId, string variableKey);


        /// <summary>
        ///     Get Current Member Variables
        /// </summary>
        /// <param name="partyId">(NOTNULL)The Party id</param>
        public abstract Task<Dictionary<string, string>> GetMemberVariables(string partyId);


        /// <summary>
        ///     Delete Party Variable with Variable Key
        ///     NOTE : Only Creator or Admins Can Call This Function
        /// </summary>
        /// <param name="partyId">(NOTNULL)The Party id</param>
        /// <param name="variableKey">(NOTNULL)The Variable Key</param>
        public abstract Task<PartyData> DeleteVariable(string partyId, string variableKey);


        /// <summary>
        ///     Delete Party Variables
        ///     NOTE : Only Creator or Admins Can Call This Function
        /// </summary>
        /// <param name="partyId">(NOTNULL)The Party id</param>
        public abstract Task<PartyData> DeleteVariables(string partyId);


        /// <summary>
        ///     Delete Party Current Member Variable with Variable Key
        ///     The Current Member Can Delete Own Variable in Party
        /// </summary>
        /// <param name="partyId">(NOTNULL)The Party id</param>
        /// <param name="variableKey">(NOTNULL)The Variable Key</param>
        public abstract Task<bool> DeleteMemberVariable(string partyId, string variableKey);


        /// <summary>
        ///     Delete Party Current Member Variables
        ///     The Current Member Can Delete Own Variables in Party
        /// </summary>
        /// <param name="partyId">(NOTNULL)The Party id</param>
        public abstract Task<bool> DeleteMemberVariables(string partyId);


        /// <summary>
        ///     Delete Party Current Member Variable with Variable Key By Admins Or Creator
        ///     NOTE : Only Admins Or Creator Can Call This Function
        /// </summary>
        /// <param name="partyId">(NOTNULL)The Party id</param>
        /// <param name="memberId">(NOTNULL)The Member id</param>
        /// <param name="variableKey">(NOTNULL)The Variable Key</param>
        public abstract Task<bool> DeleteMemberVariable(string partyId, string memberId, string variableKey);


        /// <summary>
        ///     Delete Party Current Member Variables with Variable Key By Admins Or Creator
        ///     NOTE : Only Admins Or Creator Can Call This Function
        /// </summary>
        /// <param name="partyId">(NOTNULL)The Party id</param>
        /// <param name="memberId">(NOTNULL)The Member id</param>
        public abstract Task<bool> DeleteMemberVariables(string partyId, string memberId);


        /// <summary>
        ///     Add a Friend To Party
        ///     NOTE : Only Creator or Admins Can Call This Function
        /// </summary>
        /// <param name="partyId">(NOTNULL)The Party id</param>
        /// <param name="memberId">(NOTNULL)The Friend Member id</param>
        public abstract Task<bool> AddFriend(string partyId, string memberId);


        /// <summary>
        ///     Kick a Member From Party
        ///     NOTE : Only Creator or Admins Can Call This Function
        /// </summary>
        /// <param name="partyId">(NOTNULL)The Party id</param>
        /// <param name="memberId">(NOTNULL)The Member id</param>
        public abstract Task<bool> KickMember(string partyId, string memberId);


        /// <summary>
        ///     Accept Join a Member To The Party
        ///     NOTE : Only Creator or Admins Can Call This Function
        /// </summary>
        /// <param name="partyId">(NOTNULL)The Party id</param>
        /// <param name="memberId">(NOTNULL)The Member id</param>
        public abstract Task<bool> AcceptJoinRequest(string partyId, string memberId);


        /// <summary>
        ///     Reject Join Request To The Party
        ///     NOTE : Only Creator or Admins Can Call This Function
        /// </summary>
        /// <param name="partyId">(NOTNULL)The Party id</param>
        /// <param name="memberId">(NOTNULL)The Member id</param>
        public abstract Task<bool> RejectJoinRequest(string partyId, string memberId);


        /// <summary>
        ///     Find All Parties With Specific Party Name Query
        /// </summary>
        /// <param name="query">(NULLABLE) Party Name Query . NOTE : if set query null , return all Parties</param>
        /// <param name="skip">The Result Skips</param>
        /// <param name="limit">(MAX = 25)The Result Limits</param>
        public abstract Task<Results<PartyData>> FindParties(string query = null, int skip = 0, int limit = 25);


        /// <summary>
        ///     Get Current Member Parties With Specific skip & limit
        /// </summary>
        /// <param name="skip">The Result Skips</param>
        /// <param name="limit">(Max = 25) The Result Limits</param>
        public abstract Task<Results<PartyData>> GetMyParties(int skip = 0, int limit = 25);


        /// <summary>
        ///     Get Party Info With Specific partyId
        ///     NOTE : Only Party Members Can Get PartyInfo
        /// </summary>
        /// <param name="partyId">(NOTNULL)The Party id</param>
        public abstract Task<PartyInfo> GetPartyInfo(string partyId);


        /// <summary>
        ///     Get Party Join Requests With Specific skip & limit
        /// </summary>
        /// <param name="partyId">(NOTNULL)The Party id</param>
        /// <param name="skip">The Result Skips</param>
        /// <param name="limit">(Max = 25) The Result Limits</param>
        public abstract Task<List<Member>> GetPartyJoinRequests(string partyId, int skip = 0, int limit = 25);
    }
}