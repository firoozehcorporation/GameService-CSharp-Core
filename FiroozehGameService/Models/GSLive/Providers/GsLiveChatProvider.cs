// <copyright file="GsLiveChatProvider.cs" company="Firoozeh Technology LTD">
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


namespace FiroozehGameService.Models.GSLive.Providers
{
    /// <summary>
    ///     Represents Game Service Chat System
    /// </summary>
    public abstract class GsLiveChatProvider
    {
        /// <summary>
        ///     Subscribe In Channel With channelName.
        /// </summary>
        /// <param name="channelName">(NOTNULL)Name of Channel You want To Subscribe</param>
        public abstract void SubscribeChannel(string channelName);


        /// <summary>
        ///     UnSubscribeChannel With channelName.
        /// </summary>
        /// <param name="channelName">(NOTNULL)Name of Channel You want To UnSubscribe</param>
        public abstract void UnSubscribeChannel(string channelName);


        /// <summary>
        ///     Send Message In Subscribed Channel.
        /// </summary>
        /// <param name="channelName">(NOTNULL)Name of Channel You want To Send Message</param>
        /// <param name="message">(NOTNULL)Message Data</param>
        /// <param name="property">(NULLABLE) Message Extra Property</param>
        public abstract void SendChannelMessage(string channelName, string message, string property = null);


        /// <summary>
        ///     Send Message To Member.
        /// </summary>
        /// <param name="memberId">(NOTNULL)ID of Member You want To Send Message</param>
        /// <param name="message">(NOTNULL)Message Data</param>
        /// <param name="property">(NULLABLE) Message Extra Property</param>
        public abstract void SendPrivateMessage(string memberId, string message, string property = null);


        /// <summary>
        ///     Remove a Chat In Public Channel
        /// </summary>
        /// <param name="channelName">(NOTNULL)The Channel Name You want To Remove Message in it</param>
        /// <param name="messageId">(NOTNULL)The message Id You Want To Remove It</param>
        public abstract void RemoveChannelMessage(string channelName, string messageId);


        /// <summary>
        ///     Remove a Private Chat
        /// </summary>
        /// <param name="memberId">(NOTNULL)The memberId You want To Remove Message that sent to it before</param>
        /// <param name="messageId">(NOTNULL)The message Id You Want To Remove It</param>
        public abstract void RemovePrivateMessage(string memberId, string messageId);


        /// <summary>
        ///     Remove all a Member Messages in Public Channel
        /// </summary>
        /// <param name="channelName">(NOTNULL)The Channel Name You want To Remove Messages in it</param>
        /// <param name="memberId">(NOTNULL)The Member Id You Want To Remove all Member Messages in Public Channel</param>
        public abstract void RemoveMemberMessages(string channelName, string memberId);


        /// <summary>
        ///     Get Channels Subscribe List
        /// </summary>
        public abstract void GetChannelsSubscribed();


        /// <summary>
        ///     Get Channel last 30 Messages
        /// </summary>
        /// <param name="channelName">(NOTNULL)Name of Channel You want To Get last 30 Messages</param>
        public abstract void GetChannelRecentMessages(string channelName);


        /// <summary>
        ///     Get Channel Members
        /// </summary>
        /// <param name="channelName">(NOTNULL)Name of Channel You want To Get Members</param>
        /// <param name="skip">The skip value</param>
        /// <param name="limit">(Max = 15) The Limit value</param>
        public abstract void GetChannelMembers(string channelName, int skip = 0, int limit = 10);


        /// <summary>
        ///     Get Your Pending Messages
        /// </summary>
        public abstract void GetPendingMessages();
    }
}