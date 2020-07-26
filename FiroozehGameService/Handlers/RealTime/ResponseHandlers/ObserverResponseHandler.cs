using System;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.Enums.GSLive.RT;
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
                var (ownerId,payloads) = GsSerializer.Object.GetObserver(packet.Payload);
                foreach (var payload in payloads)
                {
                    var dataPayload = new DataPayload(payload);
                    GsSerializer.OnNewEventHandler?.Invoke(null,
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