using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.GSLive.Command;

namespace FiroozehGameService.Handlers.TurnBased.ResponseHandlers
{
    internal class AuthResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand
            => TB.ActionAuth;

        protected override void HandleResponse(Packet packet)
        {
            CoreEventHandlers.Authorized?.Invoke(this, packet.Token);
        }
    }
}