// <copyright file="SocialOptions.cs" company="Firoozeh Technology LTD">
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

using FiroozehGameService.Models;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Utils;

namespace FiroozehGameService.Core.Social
{
    /// <summary>
    ///     Represents GSLive GSLiveOption
    /// </summary>
    public class SocialOptions
    {
        /// <summary>
        ///     Represents Social PartyOption
        /// </summary>
        public class PartyOption
        {
            /// <summary>
            ///     Specifies the Party Options
            ///     <param name="name">(MIN = 5 , MAX = 32 characters) Specifies the Party Name</param>
            ///     <param name="description">(NULLABLE)(MIN = 3 , MAX = 120 characters) Specifies the Party Description</param>
            ///     <param name="maxMember">(MIN = 1 , MAX = 16) Specifies the Party Max Player</param>
            ///     <param name="logo">(NULLABLE) Specifies the Party Logo (BytesBuffer) </param>
            /// </summary>
            public PartyOption(string name, string description = null,int maxMember = 16, byte[] logo = null)
            {
                if (string.IsNullOrEmpty(name))
                    throw new GameServiceException("party name Cant Be EmptyOrNull").LogException<SocialOptions>(
                        DebugLocation.Social, "Constructor");
                if (name.Length < 5 || name.Length > 32)
                    throw new GameServiceException("invalid party name").LogException<SocialOptions>(
                        DebugLocation.Social, "Constructor");
                if (!string.IsNullOrEmpty(description) && (description.Length < 3 || description.Length > 120))
                    throw new GameServiceException("invalid party description").LogException<SocialOptions>(
                        DebugLocation.Social, "Constructor");
                
                if (maxMember < 1 || maxMember > 16)
                    throw new GameServiceException("invalid party maxMember").LogException<SocialOptions>(
                        DebugLocation.Social, "Constructor");

                Name = name;
                Description = description;
                Logo = logo;
                MaxMember = maxMember;
            }

            internal string Name { get; }
            internal string Description { get; }
            
            internal int MaxMember { get; }
            
            internal byte[] Logo { get; }
        }
    }
}