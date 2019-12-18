using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.Enums;

namespace FiroozehGameService.Handlers.RealTime.ResponseHandlers
{
    internal class AuthResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand 
            => RT.ActionAuth;

        protected override void HandleResponse(Packet packet,GProtocolSendType type)
        {
            CoreEventHandlers.Authorized?.Invoke(this,packet.Token);
        }
    }
}
