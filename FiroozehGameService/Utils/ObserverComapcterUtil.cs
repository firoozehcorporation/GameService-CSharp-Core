// <copyright file="ObserverCompacterUtil.cs" company="Firoozeh Technology LTD">
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
using System.Collections.Generic;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.GSLive.RT;
using FiroozehGameService.Utils.Serializer;

namespace FiroozehGameService.Utils
{
    internal static class ObserverCompacterUtil
    {
        private const short MaxQueueSize = 255;
        private static Queue<byte[]> _sendQueue;
        private static Event _queueWorkerEvent;

        public static EventHandler<byte[]> SendObserverEventHandler;

        internal static void Init()
        {
            _sendQueue = new Queue<byte[]>();
            _queueWorkerEvent = EventCallerUtil.CreateNewEvent(1000 / RealTimeConst.RealTimeLimit);
            _queueWorkerEvent.EventHandler += EventHandler;
            _queueWorkerEvent.Start();
        }


        internal static void Dispose()
        {
            _sendQueue?.Clear();
            _queueWorkerEvent?.Dispose();
        }

        internal static void AddToQueue(DataPayload dataPayload)
        {
            var payload = dataPayload.Serialize();
            if (GsSerializer.Object.GetSendQueueBufferSize(_sendQueue) + payload.Length <= RealTimeConst.MaxPacketBeforeSize
                && _sendQueue.Count <= MaxQueueSize)
                _sendQueue?.Enqueue(payload);
            else
                LogUtil.LogError(null, "Send Queue is Full!");
        }


        private static void EventHandler(object sender, Event e)
        {
            if (_sendQueue.Count <= 0) return;
            SendObserverEventHandler?.Invoke(null, GsSerializer.Object.GetSendQueueBuffer(_sendQueue));
        }
    }
}