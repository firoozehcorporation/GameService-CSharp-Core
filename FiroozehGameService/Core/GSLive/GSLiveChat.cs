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
using FiroozehGameService.Handlers.Command.RequestHandlers;

namespace FiroozehGameService.Core.GSLive
{
    /// <summary>
    /// Represents Game Service Chat System
    /// </summary>
    public class GSLiveChat
    {
        private const string Tag = "GSLiveChat";
                
        /// <summary>
        /// Subscribe In Channel With channelName.
        /// </summary>
        /// <param name="channelName">(NOTNULL)Name of Channel You want To Subscribe</param>
        public async Task SubscribeChannel(string channelName)
        {
           await GSLive.Handler.CommandHandler.RequestAsync(SubscribeChannelHandler.Signature,channelName);     
        }
        
        /// <summary>
        /// UnSubscribeChannel With channelName.
        /// </summary>
        /// <param name="channelName">(NOTNULL)Name of Channel You want To UnSubscribe</param>
        public async Task UnSubscribeChannel(string channelName)
        {
            await GSLive.Handler.CommandHandler.RequestAsync(UnsubscribeChannelHandler.Signature,channelName);     
        }
        
        /// <summary>
        /// Send Message In SubscribedChannel.
        /// </summary>
        /// <param name="channelName">(NOTNULL)Name of Channel You want To Send Message</param>
        /// <param name="message">(NOTNULL)Message Data</param>

        public async Task SendChannelMessage(string channelName,string message)
        {
           await GSLive.Handler.CommandHandler.RequestAsync(SendChannelMessageHandler.Signature,Tuple.Create(channelName,message));     
        }
    }
}