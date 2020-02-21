using System.Collections.Generic;
using FiroozehGameService.Models.BasicApi;
using FiroozehGameService.Models.GSLive.Command;
using Newtonsoft.Json;
using Invite = FiroozehGameService.Models.GSLive.Invite;

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
