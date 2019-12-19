using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.GSLive.RT;

namespace FiroozehGameService.Handlers.RealTime.ResponseHandlers
{
    internal class AuthResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand 
            => RT.ActionAuth;

        protected override void HandleResponse(Packet packet,GProtocolSendType type)
        {
            CoreEventHandlers.Authorized?.Invoke(this,packet.Hash);
        }
    }
}
