// <copyright file="Social.cs" company="Firoozeh Technology LTD">
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
using FiroozehGameService.Models.BasicApi.Social;
using FiroozehGameService.Models.BasicApi.Social.Providers;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Utils;

namespace FiroozehGameService.Core.Social
{
    internal class Social : SocialProvider
    {
        private readonly Friend _friend;
        private readonly Party _party;

        internal Social()
        {
            _party = new Party();
            _friend = new Friend();
        }

        public override FriendProvider Friend()
        {
            return _friend;
        }

        public override PartyProvider Party()
        {
            return _party;
        }


        public override async Task<List<Event>> GetAllEvents()
        {
            if (!GameService.IsAuthenticated())
                throw new GameServiceException("GameService Not Available").LogException(typeof(Social),
                    DebugLocation.Social, "GetAllEvents");
            return await ApiRequest.GetAllEvents();
        }
    }
}