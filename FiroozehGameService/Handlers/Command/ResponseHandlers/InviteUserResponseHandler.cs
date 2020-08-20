using FiroozehGameService.Models.GSLive.Command;

namespace FiroozehGameService.Handlers.Command.ResponseHandlers
{
    internal class InviteUserResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand
            => Models.Consts.Command.ActionInviteUser;

        protected override void HandleResponse(Packet packet)
        {
            CommandEventHandler.InvitationSent?.Invoke(null, null);
        }
    }
}