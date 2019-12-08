using System.Collections.Generic;
using FiroozehGameService.Models.Command;
using Newtonsoft.Json;
using Invite = FiroozehGameService.Models.GSLive.Invite;

namespace FiroozehGameService.Handlers.Command.ResponseHandlers
{
    internal class InviteReceivedInboxResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand 
            => Models.Consts.Command.ActionOnInvite;

        protected override void HandleResponse(Packet packet)
        {
            var invite = JsonConvert.DeserializeObject<Invite>(packet.Data);
            CommandEventHandler.NewInviteReceived?.Invoke(null, invite);
        }
    }
}
