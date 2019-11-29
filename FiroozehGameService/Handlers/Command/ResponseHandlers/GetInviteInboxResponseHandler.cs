using System.Collections.Generic;
using FiroozehGameService.Models.Command;
using Newtonsoft.Json;
using Invite = FiroozehGameService.Models.GSLive.Invite;

namespace FiroozehGameService.Handlers.Command.ResponseHandlers
{
    internal class GetInviteInboxResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand 
            => Models.Consts.Command.ActionGetInviteList;

        protected override void HandleResponse(Packet packet)
        {
            var invites = JsonConvert.DeserializeObject<List<Invite>>(packet.Data);
            CommandEventHandler.OnInviteInbox?.Invoke(null, invites);
        }
    }
}
