// <copyright file="Ownership.cs" company="Firoozeh Technology LTD">
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

using System;
using FiroozehGameService.Core;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.Enums.DBaaS;
using FiroozehGameService.Utils;

/**
* @author Alireza Ghodrati
*/


namespace FiroozehGameService.Models.BasicApi.DBaaS.Options
{
    /// <summary>
    ///     Represents OwnershipOptionData Model In Game Service Basic API
    /// </summary>
    [Serializable]
    public class Ownership : TableOption
    {
        private ItemOwnershipTypes _ownershipTypes;
        private string _ownerUserId;

        /// <summary>
        ///     the Ownership TableOption
        /// </summary>
        /// <param name="ownershipTypes">type of ownership</param>
        /// <param name="ownerUserId">user id of TableOwnershipTypes.Another type</param>
        public Ownership(ItemOwnershipTypes ownershipTypes, string ownerUserId = null)
        {
            if (!GameService.IsAuthenticated())
                throw new GameServiceException("Ownership TableOption Not Working In Global Mode")
                    .LogException<Ownership>(DebugLocation.Internal, "Constructor");

            _ownerUserId = ownershipTypes == ItemOwnershipTypes.Another && string.IsNullOrEmpty(ownerUserId)
                ? throw new GameServiceException("OwnerUserId Cant Be EmptyOrNull When OwnershipType is Another")
                    .LogException<Ownership>(DebugLocation.Internal, "Constructor")
                : _ownerUserId = ownerUserId;

            _ownershipTypes = ownershipTypes;
        }

        internal override string GetParsedData()
        {
            var owner = _ownershipTypes == ItemOwnershipTypes.Me ? "me" : _ownerUserId;
            return "&owner=" + owner;
        }
    }
}