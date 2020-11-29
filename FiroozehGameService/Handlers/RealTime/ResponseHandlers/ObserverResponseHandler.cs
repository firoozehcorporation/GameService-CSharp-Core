// <copyright file="ObserverResponseHandler.cs" company="Firoozeh Technology LTD">
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
    internal class ObserverResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand => RealTimeConst.ActionObserver;

        protected override void HandleResponse(Packet packet, GProtocolSendType type)
        {
            try
            {
                var (ownerId, payloads) = GsSerializer.Object.GetObserver(packet.Payload);

                while (payloads.Count > 0)
                {
                    var dataPayload = new DataPayload(payloads.Dequeue());
                    GsSerializer.OnNewEventHandler?.Invoke(this,
                        new EventData
                        {
                            Caller = dataPayload.ExtraData,
                            Data = dataPayload.Payload,
                            SenderId = ownerId
                        });
                }
            }
            catch (Exception e)
            {
                LogUtil.LogError(this, e.Message + "\r\n" + e.InnerException);
            }
        }
    }
}