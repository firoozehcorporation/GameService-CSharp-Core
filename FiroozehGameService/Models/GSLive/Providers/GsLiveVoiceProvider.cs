// <copyright file="GsLiveVoiceProvider.cs" company="Firoozeh Technology LTD">
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


using FiroozehGameService.Core.GSLive;

namespace FiroozehGameService.Models.GSLive.Providers
{
    /// <summary>
    ///     Represents Game Service Voice System
    /// </summary>
    internal abstract class GsLiveVoiceProvider
    {
        /// <summary>
        ///     Create Voice Channel With Options
        /// </summary>
        /// <param name="option">(NOTNULL)Voice Option</param>
        internal abstract void CreateChannel(GSLiveMediaOption.VoiceOption option);


        /// <summary>
        ///     Join Voice Channel With channel Unique Key
        /// </summary>
        /// <param name="channelKey">(NOTNULL)the channel Unique Key</param>
        internal abstract void JoinChannel(string channelKey);


        /// <summary>
        ///     Leave Voice Channel With channel Id
        ///     NOTE : You must Joined a Voice Channel Before
        /// </summary>
        /// <param name="channelId">(NOTNULL)the channel Id</param>
        internal abstract void LeaveChannel(string channelId);


        /// <summary>
        ///     Kick a Member from channel
        ///     NOTE : You must Joined a Voice Channel Before, and You Must be Creator
        /// </summary>
        /// <param name="channelId">(NOTNULL)the channel Id</param>
        /// <param name="memberId">(NOTNULL)the member id you want to kick it</param>
        /// <param name="permanent">kick permanently</param>
        internal abstract void KickMember(string channelId, string memberId, bool permanent);


        /// <summary>
        ///     Destroy Voice Channel
        ///     NOTE : You must Joined a Voice Channel Before, and You Must be Creator
        /// </summary>
        /// <param name="channelId">(NOTNULL)the channel Id</param>
        internal abstract void DestroyChannel(string channelId);


        /// <summary>
        ///     Get Voice Channel Info
        /// </summary>
        /// <param name="channelKey">(NOTNULL)the channel Unique Key</param>
        internal abstract void GetChannelInfo(string channelKey);


        /// <summary>
        ///     Mute Current Player
        ///     NOTE : You must Joined a Voice Channel Before
        /// </summary>
        /// <param name="channelId">(NOTNULL)the channel Id</param>
        /// <param name="isMuted">mute status</param>
        internal abstract void MuteSelf(string channelId, bool isMuted);


        /// <summary>
        ///     Deafen Current Player
        ///     NOTE : You must Joined a Voice Channel Before
        /// </summary>
        /// <param name="channelId">(NOTNULL)the channel Id</param>
        /// <param name="isDeafened">deafen status</param>
        internal abstract void DeafenSelf(string channelId, bool isDeafened);


        /// <summary>
        ///     Trickle Current Player
        ///     NOTE : You must Joined a Voice Channel Before
        /// </summary>
        /// <param name="channelId">(NOTNULL)the channel Id</param>
        /// <param name="ice">ice data</param>
        internal abstract void Trickle(string channelId, string ice);


        /// <summary>
        ///     Offer Current Player
        ///     NOTE : You must Joined a Voice Channel Before
        /// </summary>
        /// <param name="channelId">(NOTNULL)the channel Id</param>
        /// <param name="sdp">sdp data</param>
        internal abstract void Offer(string channelId, string sdp);
    }
}