using FiroozehGameService.Models.GSLive.Command;

namespace FiroozehGameService.Handlers.Command.ResponseHandlers
{
    internal class AuthResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand
            => Models.Consts.Command.ActionAuth;

        protected override void HandleResponse(Packet packet)
        {
            CoreEventHandlers.Authorized?.Invoke(this, packet.Token);
        }
    }
}