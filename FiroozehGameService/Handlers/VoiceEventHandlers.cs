// <copyright file="VoiceEventHandlers.cs" company="Firoozeh Technology LTD">
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

using System;
using FiroozehGameService.Models.GSLive.Command;
using FiroozehGameService.Models.GSLive.Providers;
using FiroozehGameService.Models.GSLive.Voice;

/**
* @author Alireza Ghodrati
*/

namespace FiroozehGameService.Handlers
{
    /// <summary>
    ///     Represents VoiceEventHandlers
    /// </summary>
    internal class VoiceEventHandlers
    {
        /// <summary>
        ///     Calls When Create Voice Channel Received
        ///     <see cref="GsLiveVoiceProvider.CreateChannel" />
        /// </summary>
        internal static EventHandler<VoiceChannel> CreateChannelReceived;


        /// <summary>
        ///     Calls When Join Voice Channel Received
        ///     <see cref="GsLiveVoiceProvider.JoinChannel" />
        /// </summary>
        internal static EventHandler<JoinVoiceChannel> JoinChannelReceived;


        /// <summary>
        ///     Calls When Leave Voice Channel Received
        ///     <see cref="GsLiveVoiceProvider.LeaveChannel" />
        /// </summary>
        internal static EventHandler<LeaveVoiceChannel> LeftMemberReceived;


        /// <summary>
        ///     Calls When Destroy Voice Channel Received
        ///     <see cref="GsLiveVoiceProvider.DestroyChannel" />
        /// </summary>
        internal static EventHandler<DestroyVoiceChannel> DestroyChannelReceived;


        /// <summary>
        ///     Calls When Info Voice Channel Received
        ///     <see cref="GsLiveVoiceProvider.GetChannelInfo" />
        /// </summary>
        internal static EventHandler<VoiceChannelInfo> InfoChannelReceived;


        /// <summary>
        ///     Calls When Mute Voice Channel Received
        /// </summary>
        internal static EventHandler<MuteMemberVoiceChannel> MuteMemberReceived;


        /// <summary>
        ///     Calls When Deafen Voice Channel Received
        /// </summary>
        internal static EventHandler<DeafenMemberVoiceChannel> DeafenMemberReceived;


        /// <summary>
        ///     Calls When Kick Voice Channel Received
        ///     <see cref="GsLiveVoiceProvider.KickMember" />
        /// </summary>
        internal static EventHandler<KickMemberVoiceChannel> KickMemberReceived;

        /// <summary>
        ///     Calls When An New Error Received From Server
        /// </summary>
        public static EventHandler<ErrorEvent> ErrorReceived;


        /// <summary>
        ///     Calls When Offer Voice Channel Received
        /// </summary>
        internal static EventHandler<SdpVoiceChannel> AnswerMemberReceived;


        /// <summary>
        ///     Calls When Trickle Voice Channel Received
        /// </summary>
        internal static EventHandler<IceVoiceChannel> TrickleMemberReceived;
    }
}