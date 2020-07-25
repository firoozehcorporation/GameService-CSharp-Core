using System.Text;
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
            var memberId = Encoding.UTF8.GetString(packet.Payload);
            
            CoreEventHandlers.OnMemberId?.Invoke(this,memberId);
            CoreEventHandlers.Authorized?.Invoke(this,packet.Hash);
        }
    }
}
