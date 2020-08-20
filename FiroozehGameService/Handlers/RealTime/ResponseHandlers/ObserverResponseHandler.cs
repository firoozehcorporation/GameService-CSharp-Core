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
        public static int ActionCommand => RT.ActionObserver;

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