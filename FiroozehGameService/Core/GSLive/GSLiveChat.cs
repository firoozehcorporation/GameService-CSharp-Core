// <copyright file="GSLiveChat.cs" company="Firoozeh Technology LTD">
// Copyright (C) 2019 Firoozeh Technology LTD. All Rights Reserved.
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

using System;
using System.Threading.Tasks;
using FiroozehGameService.Handlers.Command.RequestHandlers.Chat;
using FiroozehGameService.Models;
using FiroozehGameService.Models.GSLive;

namespace FiroozehGameService.Core.GSLive
{
    /// <summary>
    ///     Represents Game Service Chat System
    /// </summary>
    public class GSLiveChat
    {
        private const string Tag = "GSLiveChat";

        /// <summary>
        ///     Subscribe In Channel With channelName.
        /// </summary>
        /// <param name="channelName">(NOTNULL)Name of Channel You want To Subscribe</param>
        public async Task SubscribeChannel(string channelName)
        {
            if (GameService.IsGuest) throw new GameServiceException("This Function Not Working In Guest Mode");
            if (string.IsNullOrEmpty(channelName)) throw new GameServiceException("channelName Cant Be EmptyOrNull");
            await GSLive.Handler.CommandHandler.RequestAsync(SubscribeChannelHandler.Signature, channelName);
        }

        /// <summary>
        ///     UnSubscribeChannel With channelName.
        /// </summary>
        /// <param name="channelName">(NOTNULL)Name of Channel You want To UnSubscribe</param>
        public async Task UnSubscribeChannel(string channelName)
        {
            if (GameService.IsGuest) throw new GameServiceException("This Function Not Working In Guest Mode");
            if (string.IsNullOrEmpty(channelName)) throw new GameServiceException("channelName Cant Be EmptyOrNull");
            await GSLive.Handler.CommandHandler.RequestAsync(UnsubscribeChannelHandler.Signature, channelName);
        }


        /// <summary>
        ///     Send Message In SubscribedChannel.
        /// </summary>
        /// <param name="channelName">(NOTNULL)Name of Channel You want To Send Message</param>
        /// <param name="message">(NOTNULL)Message Data</param>
        public async Task SendChannelMessage(string channelName, string message)
        {
            if (GameService.IsGuest) throw new GameServiceException("This Function Not Working In Guest Mode");
            if (string.IsNullOrEmpty(channelName) && string.IsNullOrEmpty(message))
                throw new GameServiceException("channelName Or message Cant Be EmptyOrNull");
            await GSLive.Handler.CommandHandler.RequestAsync(SendChannelPublicMessageHandler.Signature,
                Tuple.Create(channelName, message));
        }


        /// <summary>
        ///     Send Message To Member.
        /// </summary>
        /// <param name="memberId">(NOTNULL)ID of Member You want To Send Message</param>
        /// <param name="message">(NOTNULL)Message Data</param>
        public async Task SendPrivateMessage(string memberId, string message)
        {
            if (GameService.IsGuest) throw new GameServiceException("This Function Not Working In Guest Mode");
            if (string.IsNullOrEmpty(memberId) && string.IsNullOrEmpty(message))
                throw new GameServiceException("memberId Or message Cant Be EmptyOrNull");
            await GSLive.Handler.CommandHandler.RequestAsync(SendChannelPrivateMessageHandler.Signature,
                Tuple.Create(memberId, message));
        }


        /// <summary>
        ///     Get Channels Subscribe List
        /// </summary>
        public async Task GetChannelsSubscribed()
        {
            if (GameService.IsGuest) throw new GameServiceException("This Function Not Working In Guest Mode");
            await GSLive.Handler.CommandHandler.RequestAsync(GetChannelsSubscribedRequestHandler.Signature);
        }


        /// <summary>
        ///     Get Channel last 30 Messages
        /// </summary>
        /// <param name="channelName">(NOTNULL)Name of Channel You want To Get last 30 Messages</param>
        public async Task GetChannelRecentMessages(string channelName)
        {
            if (GameService.IsGuest) throw new GameServiceException("This Function Not Working In Guest Mode");
            if (string.IsNullOrEmpty(channelName)) throw new GameServiceException("channelName Cant Be EmptyOrNull");
            await GSLive.Handler.CommandHandler.RequestAsync(GetChannelRecentMessagesRequestHandler.Signature,
                new RoomData {Id = channelName});
        }


        /// <summary>
        ///     Get Channel Members
        /// </summary>
        /// <param name="channelName">(NOTNULL)Name of Channel You want To Get Members</param>
        /// <param name="skip">The skip value</param>
        /// <param name="limit">(Max = 15) The Limit value</param>
        public async Task GetChannelMembers(string channelName, int skip = 0, int limit = 10)
        {
            if (GameService.IsGuest) throw new GameServiceException("This Function Not Working In Guest Mode");
            if (string.IsNullOrEmpty(channelName)) throw new GameServiceException("channelName Cant Be EmptyOrNull");
            if (limit <= 0 || limit > 15) throw new GameServiceException("invalid Limit Value");
            if (skip < 0) throw new GameServiceException("invalid Skip Value");
            await GSLive.Handler.CommandHandler.RequestAsync(GetChannelsMembersRequestHandler.Signature,
                new RoomData {Id = channelName, Min = skip, Max = limit});
        }


        /// <summary>
        ///     Get Your Pending Messages
        /// </summary>
        public async Task GetPendingMessages()
        {
            if (GameService.IsGuest) throw new GameServiceException("This Function Not Working In Guest Mode");
            await GSLive.Handler.CommandHandler.RequestAsync(GetPendingMessagesRequestHandler.Signature);
        }
    }
}