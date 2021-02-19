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
using FiroozehGameService.Models.BasicApi.Social.Providers;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.GSLive;
using FiroozehGameService.Models.Internal;
using FiroozehGameService.Utils;
using PartyData = FiroozehGameService.Models.BasicApi.Social.Party;

namespace FiroozehGameService.Core.Social
{
    internal class Party : PartyProvider
    {
        public override async Task<PartyData> CreateParty(SocialOptions.PartyOption option)
        {
            if (!GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException<Party>(DebugLocation.Party,
                    "CreateParty");
            if (option == null)
                throw new GameServiceException("option Cant Be Null").LogException<Party>(DebugLocation.Party,
                    "CreateParty");

            return await ApiRequest.CreateParty(option);
        }


        public override async Task<bool> JoinRequest(string partyId)
        {
            if (!GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException<Party>(DebugLocation.Party,
                    "JoinRequest");
            if (string.IsNullOrEmpty(partyId))
                throw new GameServiceException("partyId Cant Be EmptyOrNull").LogException<Party>(DebugLocation.Party,
                    "JoinRequest");

            return await ApiRequest.SendJoinRequestToParty(partyId);
        }


        public override async Task<bool> LeftParty(string partyId)
        {
            if (!GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException<Party>(DebugLocation.Party,
                    "LeftParty");
            if (string.IsNullOrEmpty(partyId))
                throw new GameServiceException("partyId Cant Be EmptyOrNull").LogException<Party>(DebugLocation.Party,
                    "LeftParty");

            return await ApiRequest.LeftParty(partyId);
        }


        public override async Task<bool> DeleteParty(string partyId)
        {
            if (!GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException<Party>(DebugLocation.Party,
                    "DeleteParty");
            if (string.IsNullOrEmpty(partyId))
                throw new GameServiceException("partyId Cant Be EmptyOrNull").LogException<Party>(DebugLocation.Party,
                    "DeleteParty");

            return await ApiRequest.DeleteParty(partyId);
        }


        public override async Task<PartyData> EditParty(string partyId, SocialOptions.PartyOption option)
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

        public override async Task<bool> SetOrUpdateRole(string partyId, string memberId, string role)
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


        public override async Task<PartyData> SetOrUpdateVariable(string partyId, KeyValuePair<string, string> variable)
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


        public override async Task<bool> SetOrUpdateMemberVariable(string partyId,
            KeyValuePair<string, string> variable)
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


        public override async Task<PartyData> DeleteVariable(string partyId, string variableKey)
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


        public override async Task<PartyData> DeleteVariables(string partyId)
        {
            if (!GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException<Party>(DebugLocation.Party,
                    "DeleteVariables");
            if (string.IsNullOrEmpty(partyId))
                throw new GameServiceException("partyId Cant Be EmptyOrNull").LogException<Party>(DebugLocation.Party,
                    "DeleteVariables");

            return await ApiRequest.DeleteVariables(partyId);
        }


        public override async Task<bool> DeleteMemberVariable(string partyId, string variableKey)
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


        public override async Task<bool> DeleteMemberVariables(string partyId)
        {
            if (!GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException<Party>(DebugLocation.Party,
                    "DeleteMemberVariables");
            if (string.IsNullOrEmpty(partyId))
                throw new GameServiceException("partyId Cant Be EmptyOrNull").LogException<Party>(DebugLocation.Party,
                    "DeleteMemberVariables");

            return await ApiRequest.DeleteMemberVariables(partyId);
        }


        public override async Task<bool> AddFriend(string partyId, string memberId)
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


        public override async Task<bool> KickMember(string partyId, string memberId)
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


        public override async Task<bool> AcceptJoinRequest(string partyId, string memberId)
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


        public override async Task<bool> RejectJoinRequest(string partyId, string memberId)
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


        public override async Task<Results<PartyData>> FindParties(string query = null, int skip = 0, int limit = 25)
        {
            if (!GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException<Party>(DebugLocation.Party,
                    "FindParties");
            return await ApiRequest.GetAllParties(new QueryData(query, skip, limit).ToQueryString());
        }


        public override async Task<Results<PartyData>> GetMyParties(int skip = 0, int limit = 25)
        {
            if (!GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException<Party>(DebugLocation.Party,
                    "GetMyParties");
            return await ApiRequest.GetMyParties(new QueryData(null, skip, limit).ToQueryString());
        }


        public override async Task<PartyInfo> GetPartyInfo(string partyId)
        {
            if (!GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException<Party>(DebugLocation.Party,
                    "GetPartyInfo");
            if (string.IsNullOrEmpty(partyId))
                throw new GameServiceException("partyId Cant Be EmptyOrNull").LogException<Party>(DebugLocation.Party,
                    "GetPartyInfo");
            return await ApiRequest.GetPartyInfo(partyId);
        }


        public override async Task<List<Member>> GetPartyJoinRequests(string partyId, int skip = 0, int limit = 25)
        {
            if (!GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException<Party>(DebugLocation.Party,
                    "GetPartyJoinRequests");
            if (string.IsNullOrEmpty(partyId))
                throw new GameServiceException("partyId Cant Be EmptyOrNull").LogException<Party>(DebugLocation.Party,
                    "GetPartyJoinRequests");

            return await ApiRequest.GetPartyPendingRequests(partyId, new QueryData(null, skip, limit).ToQueryString());
        }
    }
}