using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.Enums.GSLive;

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
                Type = GSLiveType.TurnBased,
                Error = packet.Message
            });
        }
    }
}
