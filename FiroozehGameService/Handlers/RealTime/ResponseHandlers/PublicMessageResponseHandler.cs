// <copyright file="PublicMessageResponseHandler.cs" company="Firoozeh Technology LTD">
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
using FiroozehGameService.Models.Enums.GSLive.RT;
using FiroozehGameService.Models.GSLive.RT;
using FiroozehGameService.Utils;

namespace FiroozehGameService.Handlers.RealTime.ResponseHandlers
{
    internal class PublicMessageResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand => RealTimeConst.ActionPublicMessage;

        protected override void HandleResponse(Packet packet, GProtocolSendType type)
        {
            try
            {
                var dataPayload = new DataPayload(packet.Payload);
                RealTimeEventHandlers.NewMessageReceived?.Invoke(this, new MessageReceiveEvent
                {
                    MessageInfo = new MessageInfo
                    {
                        MessageType = MessageType.Public,
                        SendType = type,
                        ClientReceiveTime = packet.ClientReceiveTime,
                        RoundTripTime = PingUtil.GetLastPing()
                    },
                    Message = new Message
                    {
                        Data = dataPayload.Payload,
                        ReceiverId = dataPayload.ReceiverId,
                        SenderId = dataPayload.SenderId
                    }
                });
            }
            catch (Exception e)
            {
                LogUtil.LogError(this, e.Message);
            }
        }
    }
}