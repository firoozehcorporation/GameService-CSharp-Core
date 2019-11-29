using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.Enums.GSLive;
using FiroozehGameService.Models.GSLive.RT;
using Newtonsoft.Json;
using Packet = FiroozehGameService.Models.GSLive.RT.Packet;

namespace FiroozehGameService.Handlers.RealTime.ResponseHandlers
{
    internal class StatusResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand => RT.ActionStatus;

        protected override void HandleResponse(Packet packet)
        {
            var statusPayload = JsonConvert.DeserializeObject<StatusPayload>(packet.Payload);
            if (statusPayload.Status)
                CoreEventHandlers.OnAuth?.Invoke(this,statusPayload.Message);
            else 
                CoreEventHandlers.OnError?.Invoke(this,new ErrorEvent
                {
                    Type = GSLiveType.RealTime,
                    Error = statusPayload.Message
                });
        }
      
    }
}
