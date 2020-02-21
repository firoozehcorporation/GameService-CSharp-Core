using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.Enums.GSLive;
using FiroozehGameService.Models.GSLive.Command;
using Packet = FiroozehGameService.Models.GSLive.RT.Packet;

namespace FiroozehGameService.Handlers.RealTime.ResponseHandlers
{
    internal class ErrorResponseHandler :BaseResponseHandler
    {
        public static int ActionCommand 
            => RT.Error;

        protected override void HandleResponse(Packet packet,GProtocolSendType type)
        {
            CoreEventHandlers.Error?.Invoke(this,new ErrorEvent
            {
                Type = GSLiveType.RealTime,
                Error = packet.Payload
            });
        }
    }
}
