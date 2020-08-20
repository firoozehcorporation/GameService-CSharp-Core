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
            _queueWorkerEvent = EventCallerUtil.CreateNewEvent(1000 / RT.RealTimeLimit);
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
            if (GsSerializer.Object.GetSendQueueBufferSize(_sendQueue) + payload.Length <= RT.MaxPacketBeforeSize
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