﻿// <copyright file="NewEventResponseHandler.cs" company="Firoozeh Technology LTD">
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
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.GSLive.RT;
using FiroozehGameService.Utils;
using FiroozehGameService.Utils.Serializer;

namespace FiroozehGameService.Handlers.RealTime.ResponseHandlers
{
    internal class NewEventResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand => RealTimeConst.ActionEvent;

        protected override void HandleResponse(Packet packet, GProtocolSendType type)
        {
            try
            {
                var dataPayload = new DataPayload(packet.Payload);
                GsSerializer.OnNewEventHandler?.Invoke(this,
                    new EventData
                    {
                        Caller = dataPayload.ExtraData,
                        Data = dataPayload.Payload,
                        SenderId = dataPayload.SenderId,
                        ReceiverId = dataPayload.ReceiverId
                    });
            }
            catch (Exception e)
            {
                e.LogException<NewEventResponseHandler>(DebugLocation.RealTime,"HandleResponse");
            }
        }
    }
}