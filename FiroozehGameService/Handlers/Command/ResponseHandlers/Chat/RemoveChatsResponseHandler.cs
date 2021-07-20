// <copyright file="RemoveChatsResponseHandler.cs" company="Firoozeh Technology LTD">
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

using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.GSLive.Command;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.Command.ResponseHandlers.Chat
{
    internal class RemoveChatsResponseHandler : BaseResponseHandler
    {
        internal static int ActionCommand => CommandConst.ActionRemoveMessages;

        protected override void HandleResponse(Packet packet)
        {
            var message = JsonConvert.DeserializeObject<Message>(packet.Data);

            if (message.IsPrivate) ChatEventHandlers.OnPrivateChatsRemoved?.Invoke(this, message.Data);
            else ChatEventHandlers.OnChannelChatsRemoved?.Invoke(this, message.Data);
        }
    }
}