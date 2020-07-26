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
                var tuples = GsSerializer.Object.GetObservers(packet.Payload);
                foreach (var (ownerId , payload) in tuples)
                {
                    var dataPayload = new DataPayload(payload);
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
                LogUtil.LogError(this, e.Message);
            }
        }
    }
}