using FiroozehGameService.Models.GSLive.Command;

namespace FiroozehGameService.Handlers.Command.ResponseHandlers
{
    internal class PingResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand
            => Models.Consts.Command.ActionPing;

        protected override void HandleResponse(Packet packet)
        {
            CoreEventHandlers.Ping?.Invoke(this, null);
        }
    }
}