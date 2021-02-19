// <copyright file="GsLive.cs" company="Firoozeh Technology LTD">
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
using FiroozehGameService.Handlers;
using FiroozehGameService.Handlers.Command;
using FiroozehGameService.Handlers.RealTime;
using FiroozehGameService.Handlers.TurnBased;
using FiroozehGameService.Models.GSLive.Providers;

namespace FiroozehGameService.Core.Providers.GSLive
{
    internal class GsLive : GsLiveProvider
    {
        private readonly GsLiveChat _chat;
        private readonly GsHandler _handler;
        private readonly GsLiveRealTime _realTime;
        private readonly GsLiveTurnBased _turnBased;

        internal GsLive()
        {
            _handler = new GsHandler();
            _realTime = new GsLiveRealTime();
            _turnBased = new GsLiveTurnBased();
            _chat = new GsLiveChat();
        }

        internal override async Task Init()
        {
            await _handler.Init();
        }

        public override GsLiveRealTimeProvider RealTime()
        {
            return _realTime;
        }

        public override GsLiveTurnBasedProvider TurnBased()
        {
            return _turnBased;
        }

        public override GsLiveChatProvider Chat()
        {
            return _chat;
        }

        public override bool IsCommandAvailable()
        {
            return CommandHandler.IsAvailable;
        }

        public override bool IsRealTimeAvailable()
        {
            return RealTimeHandler.IsAvailable;
        }

        public override bool IsTurnBasedAvailable()
        {
            return TurnBasedHandler.IsAvailable;
        }

        internal override void Dispose()
        {
            _handler?.Dispose();
        }

        internal override GsHandler GetGsHandler()
        {
            return _handler;
        }
    }
}