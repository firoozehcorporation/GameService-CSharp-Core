﻿// <copyright file="AuthorizationHandler.cs" company="Firoozeh Technology LTD">
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
using FiroozehGameService.Core;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.GSLive.Command;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.Command.RequestHandlers
{
    internal class AuthorizationHandler : BaseRequestHandler
    {
        public static string Signature =>
            "AUTHORIZATION";

        protected override Packet DoAction(object payload)
        {
            string turnBasedConnectionType;
            switch (GameService.Configuration.TurnBasedConnectionType)
            {
                case ConnectionType.NotSet:
                    turnBasedConnectionType = "not-set";
                    break;
                case ConnectionType.Native:
                    turnBasedConnectionType = "tcp-sec";
                    break;
                case ConnectionType.WebSocket:
                    turnBasedConnectionType = "wss";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return new Packet(
                null,
                CommandConst.ActionAuth,
                JsonConvert.SerializeObject(
                    new AuthPayload(CommandHandler.GameId,
                        CommandHandler.UserToken,
                        turnBasedConnectionType,
                        "gprotocol"
                    ), new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    })
            );
        }

        protected override bool CheckAction(object payload)
        {
            return true;
        }
    }
}