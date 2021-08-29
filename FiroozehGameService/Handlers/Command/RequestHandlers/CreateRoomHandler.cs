// <copyright file="CreateRoomHandler.cs" company="Firoozeh Technology LTD">
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
using FiroozehGameService.Core.GSLive;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.GSLive.Command;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.Command.RequestHandlers
{
    internal class CreateRoomHandler : BaseRequestHandler
    {
        public static string Signature =>
            "CREATE_ROOM";

        private static Packet DoAction(GSLiveOption.CreateRoomOption options)
        {
            return new Packet(
                CommandHandler.PlayerHash,
                CommandConst.ActionCreateRoom,
                JsonConvert.SerializeObject(new RoomDetail
                {
                    Name = options.RoomName,
                    Role = options.Role,
                    Extra = options.Extra,
                    Max = options.MaxPlayer,
                    IsPrivate = options.IsPrivate,
                    IsPersist = options.IsPersist,
                    RoomPassword = options.RoomPassword,
                    Type = CommandConst.ActionCreateRoom,
                    GsLiveType = (int) options.GsLiveType
                }, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore
                }));
        }

        protected override bool CheckAction(object payload)
        {
            return payload.GetType() == typeof(GSLiveOption.CreateRoomOption);
        }

        protected override Packet DoAction(object payload)
        {
            if (!CheckAction(payload)) throw new ArgumentException();
            return DoAction(payload as GSLiveOption.CreateRoomOption);
        }
    }
}