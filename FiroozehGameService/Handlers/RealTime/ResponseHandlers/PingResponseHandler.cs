using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.GSLive.RT;

namespace FiroozehGameService.Handlers.RealTime.ResponseHandlers
{
    internal class PingResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand => RT.ActionPing;

        protected override void HandleResponse(Packet packet, GProtocolSendType type)
        {
            CoreEventHandlers.Ping?.Invoke(this, packet);
        }
    }
}