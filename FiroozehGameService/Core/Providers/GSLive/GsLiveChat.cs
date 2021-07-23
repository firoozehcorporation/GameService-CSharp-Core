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
        public override void SubscribeChannel(string channelName)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveChat>(
                    DebugLocation.Chat, "SubscribeChannel");
            if (string.IsNullOrEmpty(channelName))
                throw new GameServiceException("channelName Cant Be NullOrEmpty").LogException<GsLiveChat>(
                    DebugLocation.Chat, "SubscribeChannel");
            GameService.GSLive.GetGsHandler().CommandHandler.Send(SubscribeChannelHandler.Signature, channelName);
        }


        public override void UnSubscribeChannel(string channelName)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveChat>(
                    DebugLocation.Chat, "UnSubscribeChannel");
            if (string.IsNullOrEmpty(channelName))
                throw new GameServiceException("channelName Cant Be NullOrEmpty").LogException<GsLiveChat>(
                    DebugLocation.Chat, "UnSubscribeChannel");
            GameService.GSLive.GetGsHandler().CommandHandler.Send(UnsubscribeChannelHandler.Signature, channelName);
        }


        public override void SendChannelMessage(string channelName, string message, string property = null)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveChat>(
                    DebugLocation.Chat, "SendChannelMessage");

            if (string.IsNullOrEmpty(channelName) || string.IsNullOrEmpty(message))
                throw new GameServiceException("channelName Or message Cant Be NullOrEmpty").LogException<GsLiveChat>(
                    DebugLocation.Chat, "SendChannelMessage");

            GameService.GSLive.GetGsHandler().CommandHandler.Send(SendChannelPublicMessageHandler.Signature,
                new Message(false, channelName, message, property));
        }


        public override void SendPrivateMessage(string memberId, string message, string property = null)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveChat>(
                    DebugLocation.Chat, "SendPrivateMessage");
            if (string.IsNullOrEmpty(memberId) || string.IsNullOrEmpty(message))
                throw new GameServiceException("memberId Or message Cant Be NullOrEmpty").LogException<GsLiveChat>(
                    DebugLocation.Chat, "SendPrivateMessage");

            GameService.GSLive.GetGsHandler().CommandHandler.Send(SendChannelPrivateMessageHandler.Signature,
                new Message(true, memberId, message, property));
        }

        public override void EditChannelMessage(string channelName, string messageId, string message,
            string property = null)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveChat>(
                    DebugLocation.Chat, "EditChannelMessage");

            if (string.IsNullOrEmpty(channelName) || string.IsNullOrEmpty(messageId) || string.IsNullOrEmpty(message))
                throw new GameServiceException("channelName Or messageId Or message Cant Be NullOrEmpty")
                    .LogException<GsLiveChat>(
                        DebugLocation.Chat, "EditChannelMessage");

            GameService.GSLive.GetGsHandler().CommandHandler.Send(EditChatHandler.Signature,
                new Message(false, channelName, message, property, messageId));
        }

        public override void EditPrivateMessage(string memberId, string messageId, string message,
            string property = null)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveChat>(
                    DebugLocation.Chat, "EditPrivateMessage");

            if (string.IsNullOrEmpty(memberId) || string.IsNullOrEmpty(messageId) || string.IsNullOrEmpty(message))
                throw new GameServiceException("memberId Or messageId Or message Cant Be NullOrEmpty")
                    .LogException<GsLiveChat>(
                        DebugLocation.Chat, "EditPrivateMessage");

            GameService.GSLive.GetGsHandler().CommandHandler.Send(EditChatHandler.Signature,
                new Message(true, memberId, message, property, messageId));
        }

        public override void RemoveChannelMessage(string channelName, string messageId)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveChat>(
                    DebugLocation.Chat, "RemoveChannelMessage");

            if (string.IsNullOrEmpty(messageId) || string.IsNullOrEmpty(channelName))
                throw new GameServiceException("channelName Or messageId Cant Be NullOrEmpty").LogException<GsLiveChat>(
                    DebugLocation.Chat, "RemoveChannelMessage");

            GameService.GSLive.GetGsHandler().CommandHandler
                .Send(RemoveChatHandler.Signature, new Message(false, channelName, messageId));
        }

        public override void RemoveChannelMemberMessages(string channelName, string memberId)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveChat>(
                    DebugLocation.Chat, "RemoveChannelMemberMessages");

            if (string.IsNullOrEmpty(memberId) || string.IsNullOrEmpty(channelName))
                throw new GameServiceException("channelName Or memberId Cant Be NullOrEmpty").LogException<GsLiveChat>(
                    DebugLocation.Chat, "RemoveChannelMemberMessages");

            GameService.GSLive.GetGsHandler().CommandHandler.Send(RemoveMemberChatsHandler.Signature,
                new Message(false, channelName, memberId));
        }

        public override void RemoveChannelMessages(string channelName)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveChat>(
                    DebugLocation.Chat, "RemoveChannelMessages");

            if (string.IsNullOrEmpty(channelName))
                throw new GameServiceException("channelName Cant Be NullOrEmpty").LogException<GsLiveChat>(
                    DebugLocation.Chat, "RemoveChannelMessages");

            GameService.GSLive.GetGsHandler().CommandHandler.Send(RemoveChatsHandler.Signature,
                new Message(false, null, channelName));
        }

        public override void RemoveAllChannelMessages()
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveChat>(
                    DebugLocation.Chat, "RemoveChannelMessages");

            GameService.GSLive.GetGsHandler().CommandHandler.Send(RemoveAllChatsHandler.Signature, new Message(false));
        }

        public override void RemovePrivateMessage(string memberId, string messageId)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveChat>(
                    DebugLocation.Chat, "RemovePrivateMessage");

            if (string.IsNullOrEmpty(memberId) || string.IsNullOrEmpty(messageId))
                throw new GameServiceException("memberId Or messageId Cant Be NullOrEmpty").LogException<GsLiveChat>(
                    DebugLocation.Chat, "RemovePrivateMessage");

            GameService.GSLive.GetGsHandler().CommandHandler.Send(RemoveChatHandler.Signature,
                new Message(true, memberId, messageId));
        }

        public override void RemovePrivateMessages(string memberId)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveChat>(
                    DebugLocation.Chat, "RemovePrivateMessages");

            if (string.IsNullOrEmpty(memberId))
                throw new GameServiceException("memberId Cant Be NullOrEmpty").LogException<GsLiveChat>(
                    DebugLocation.Chat, "RemovePrivateMessages");

            GameService.GSLive.GetGsHandler().CommandHandler.Send(RemoveChatsHandler.Signature,
                new Message(true, null, memberId));
        }

        public override void RemoveAllPrivateMessages()
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveChat>(
                    DebugLocation.Chat, "RemoveAllPrivateMessages");

            GameService.GSLive.GetGsHandler().CommandHandler.Send(RemoveAllChatsHandler.Signature, new Message(true));
        }

        public override void GetChannelsSubscribed()
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveChat>(
                    DebugLocation.Chat, "GetChannelsSubscribed");
            GameService.GSLive.GetGsHandler().CommandHandler
                .Send(GetChannelsSubscribedRequestHandler.Signature);
        }


        public override void GetChannelRecentMessages(string channelName)
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveChat>(
                    DebugLocation.Chat, "GetChannelRecentMessages");
            if (string.IsNullOrEmpty(channelName))
                throw new GameServiceException("channelName Cant Be NullOrEmpty").LogException<GsLiveChat>(
                    DebugLocation.Chat, "GetChannelRecentMessages");
            GameService.GSLive.GetGsHandler().CommandHandler.Send(
                GetChannelRecentMessagesRequestHandler.Signature,
                new RoomDetail {Id = channelName});
        }


        public override void GetChannelMembers(string channelName, int skip = 0, int limit = 10)
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
            GameService.GSLive.GetGsHandler().CommandHandler.Send(
                GetChannelsMembersRequestHandler.Signature,
                new RoomDetail {Id = channelName, Min = skip, Max = limit});
        }

        public override void GetPrivateRecentMessages()
        {
            if (GameService.IsGuest)
                throw new GameServiceException("This Function Not Working In Guest Mode").LogException<GsLiveChat>(
                    DebugLocation.Chat, "GetPrivateRecentMessages");

            GameService.GSLive.GetGsHandler().CommandHandler
                .Send(GetPrivateRecentMessagesRequestHandler.Signature);
        }
    }
}