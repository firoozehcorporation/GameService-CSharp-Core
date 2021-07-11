// <copyright file="GsLiveChat.cs" company="Firoozeh Technology LTD">
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

using System.Threading.Tasks;
using FiroozehGameService.Handlers.Command.RequestHandlers.Chat;
using FiroozehGameService.Models;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.GSLive.Command;
using FiroozehGameService.Models.GSLive.Providers;
using FiroozehGameService.Utils;

namespace FiroozehGameService.Core.Providers.GSLive
{
    internal class GsLiveChat : GsLiveChatProvider
    {
        public override async Task SubscribeChannel(string channelName)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveChat>(
                    DebugLocation.Chat, "SubscribeChannel");
            if (string.IsNullOrEmpty(channelName))
                throw new GameServiceException("channelName Cant Be NullOrEmpty").LogException<GsLiveChat>(
                    DebugLocation.Chat, "SubscribeChannel");
            await GameService.GSLive.GetGsHandler().CommandHandler
                .RequestAsync(SubscribeChannelHandler.Signature, channelName);
        }


        public override async Task UnSubscribeChannel(string channelName)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveChat>(
                    DebugLocation.Chat, "UnSubscribeChannel");
            if (string.IsNullOrEmpty(channelName))
                throw new GameServiceException("channelName Cant Be NullOrEmpty").LogException<GsLiveChat>(
                    DebugLocation.Chat, "UnSubscribeChannel");
            await GameService.GSLive.GetGsHandler().CommandHandler
                .RequestAsync(UnsubscribeChannelHandler.Signature, channelName);
        }


        public override async Task SendChannelMessage(string channelName, string message, string property = null)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveChat>(
                    DebugLocation.Chat, "SendChannelMessage");
            if (string.IsNullOrEmpty(channelName) && string.IsNullOrEmpty(message))
                throw new GameServiceException("channelName Or message Cant Be NullOrEmpty").LogException<GsLiveChat>(
                    DebugLocation.Chat, "SendChannelMessage");

            await GameService.GSLive.GetGsHandler().CommandHandler.RequestAsync(
                SendChannelPublicMessageHandler.Signature, new Message(false, channelName, message, property));
        }


        public override async Task SendPrivateMessage(string memberId, string message, string property = null)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveChat>(
                    DebugLocation.Chat, "SendPrivateMessage");
            if (string.IsNullOrEmpty(memberId) && string.IsNullOrEmpty(message))
                throw new GameServiceException("memberId Or message Cant Be NullOrEmpty").LogException<GsLiveChat>(
                    DebugLocation.Chat, "SendPrivateMessage");

            await GameService.GSLive.GetGsHandler().CommandHandler.RequestAsync(
                SendChannelPrivateMessageHandler.Signature, new Message(true, memberId, message, property));
        }

        public override async Task RemoveChat(string chatId)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveChat>(
                    DebugLocation.Chat, "RemoveChat");

            if (string.IsNullOrEmpty(chatId))
                throw new GameServiceException("chatId Cant Be NullOrEmpty").LogException<GsLiveChat>(
                    DebugLocation.Chat, "RemoveChat");

            await GameService.GSLive.GetGsHandler().CommandHandler.RequestAsync(RemoveChatHandler.Signature, chatId);
        }

        public override async Task RemoveMemberChats(string memberId)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveChat>(
                    DebugLocation.Chat, "RemoveMemberChats");

            if (string.IsNullOrEmpty(memberId))
                throw new GameServiceException("memberId Cant Be NullOrEmpty").LogException<GsLiveChat>(
                    DebugLocation.Chat, "RemoveMemberChats");

            await GameService.GSLive.GetGsHandler().CommandHandler
                .RequestAsync(RemoveMemberChatsHandler.Signature, memberId);
        }


        public override async Task GetChannelsSubscribed()
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveChat>(
                    DebugLocation.Chat, "GetChannelsSubscribed");
            await GameService.GSLive.GetGsHandler().CommandHandler
                .RequestAsync(GetChannelsSubscribedRequestHandler.Signature);
        }


        public override async Task GetChannelRecentMessages(string channelName)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveChat>(
                    DebugLocation.Chat, "GetChannelRecentMessages");
            if (string.IsNullOrEmpty(channelName))
                throw new GameServiceException("channelName Cant Be NullOrEmpty").LogException<GsLiveChat>(
                    DebugLocation.Chat, "GetChannelRecentMessages");
            await GameService.GSLive.GetGsHandler().CommandHandler.RequestAsync(
                GetChannelRecentMessagesRequestHandler.Signature,
                new RoomDetail {Id = channelName});
        }


        public override async Task GetChannelMembers(string channelName, int skip = 0, int limit = 10)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveChat>(
                    DebugLocation.Chat, "GetChannelMembers");
            if (string.IsNullOrEmpty(channelName))
                throw new GameServiceException("channelName Cant Be NullOrEmpty").LogException<GsLiveChat>(
                    DebugLocation.Chat, "GetChannelMembers");
            if (limit <= 0 || limit > 15)
                throw new GameServiceException("invalid Limit Value").LogException<GsLiveChat>(DebugLocation.Chat,
                    "GetChannelMembers");
            if (skip < 0)
                throw new GameServiceException("invalid Skip Value").LogException<GsLiveChat>(DebugLocation.Chat,
                    "GetChannelMembers");
            await GameService.GSLive.GetGsHandler().CommandHandler.RequestAsync(
                GetChannelsMembersRequestHandler.Signature,
                new RoomDetail {Id = channelName, Min = skip, Max = limit});
        }


        public override async Task GetPendingMessages()
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveChat>(
                    DebugLocation.Chat, "GetPendingMessages");
            await GameService.GSLive.GetGsHandler().CommandHandler
                .RequestAsync(GetPendingMessagesRequestHandler.Signature);
        }
    }
}