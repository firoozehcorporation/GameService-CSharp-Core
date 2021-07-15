// <copyright file="ErrorResponseHandler.cs" company="Firoozeh Technology LTD">
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

using FiroozehGameService.Core.Providers.GSLive;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.Enums.GSLive;
using FiroozehGameService.Models.GSLive.Command;

namespace FiroozehGameService.Handlers.TurnBased.ResponseHandlers
{
    internal class ErrorResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand
            => TurnBasedConst.Errors;

        protected override void HandleResponse(Packet packet)
        {
            if (GsLiveTurnBased.InAutoMatch) GsLiveTurnBased.InAutoMatch = false;

            CoreEventHandlers.Error?.Invoke(this, new ErrorEvent
            {
                Type = GSLiveType.TurnBased,
                Error = packet.Message
            });
        }
    }
}