// <copyright file="GSLiveMediaOption.cs" company="Firoozeh Technology LTD">
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

using FiroozehGameService.Models;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Utils;

namespace FiroozehGameService.Core.GSLive
{
    /// <summary>
    ///     Represents GSLive Media Option
    /// </summary>
    internal class GSLiveMediaOption
    {
        /// <summary>
        ///     Represents GSLiveMedia VoiceOption
        /// </summary>
        public class VoiceOption
        {
            internal VoiceOption()
            {
            }

            /// <summary>
            ///     Specifies the VoiceChannel Options
            /// </summary>
            /// <param name="uniqueKey">(NOTNULL)Specifies the VoiceChannel Unique Key</param>
            /// <param name="channelName">(NOTNULL)Specifies the VoiceChannel Name</param>
            /// <param name="channelDescription">Specifies the VoiceChannel Description</param>
            internal VoiceOption(string uniqueKey, string channelName, string channelDescription = null)
            {
                if (string.IsNullOrEmpty(uniqueKey))
                    throw new GameServiceException("uniqueKey Cant Be NullOrEmpty")
                        .LogException<VoiceOption>(DebugLocation.Voice, "Constructor");

                if (string.IsNullOrEmpty(channelName))
                    throw new GameServiceException("channelName Cant Be NullOrEmpty").LogException<VoiceOption>(
                        DebugLocation.Voice, "Constructor");


                if (uniqueKey.Length < VoiceConst.MinChannelKeyLength ||
                    uniqueKey.Length > VoiceConst.MaxChannelKeyLength)
                    throw new GameServiceException("uniqueKey must between " + VoiceConst.MinChannelKeyLength +
                                                   " and " + VoiceConst.MaxChannelKeyLength + " Characters.")
                        .LogException<VoiceOption>(DebugLocation.Voice, "Constructor");

                if (channelName.Length < VoiceConst.MinChannelNameLength ||
                    channelName.Length > VoiceConst.MaxChannelNameLength)
                    throw new GameServiceException("channelName must between " + VoiceConst.MinChannelNameLength +
                                                   " and " + VoiceConst.MaxChannelNameLength + " Characters.")
                        .LogException<VoiceOption>(DebugLocation.Voice, "Constructor");

                if (channelDescription != null && (channelDescription.Length < VoiceConst.MinChannelDescLength ||
                                                   channelDescription.Length > VoiceConst.MaxChannelDescLength))
                    throw new GameServiceException("channelDescription must between " +
                                                   VoiceConst.MinChannelDescLength + " and " +
                                                   VoiceConst.MaxChannelDescLength + " Characters.")
                        .LogException<VoiceOption>(DebugLocation.Voice, "Constructor");

                UniqueKey = uniqueKey;
                Name = channelName;
                Desc = channelDescription;
            }

            internal string UniqueKey { get; }
            internal string Name { get; }
            internal string Desc { get; }
        }
    }
}