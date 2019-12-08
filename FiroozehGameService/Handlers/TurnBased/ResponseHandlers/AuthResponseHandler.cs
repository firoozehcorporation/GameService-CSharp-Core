using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.Consts;

namespace FiroozehGameService.Handlers.TurnBased.ResponseHandlers
{
    internal class AuthResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand 
            => TB.ActionAuth;

        protected override void HandleResponse(Packet packet)
        {
            CoreEventHandlers.Authorized?.Invoke(this,packet.Token);
        }
    }
}
