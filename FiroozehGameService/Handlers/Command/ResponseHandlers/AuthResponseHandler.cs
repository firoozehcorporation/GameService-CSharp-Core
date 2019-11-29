using FiroozehGameService.Models.Command;

namespace FiroozehGameService.Handlers.Command.ResponseHandlers
{
    internal class AuthResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand 
            => Models.Consts.Command.ActionAuth;

        protected override void HandleResponse(Packet packet)
        {
            CoreEventHandlers.OnAuth?.Invoke(this,packet.Token);
        }
    }
}
