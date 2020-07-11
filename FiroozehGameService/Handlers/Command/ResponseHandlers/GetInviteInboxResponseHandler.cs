using System.Collections.Generic;
using FiroozehGameService.Models.GSLive;
using FiroozehGameService.Models.GSLive.Command;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.Command.ResponseHandlers
{
    internal class GetInviteInboxResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand
            => Models.Consts.Command.ActionGetInviteList;

        protected override void HandleResponse(Packet packet)
        {
            var invites = JsonConvert.DeserializeObject<List<Invite>>(GetStringFromBuffer(packet.Data));
            CommandEventHandler.InviteInboxReceived?.Invoke(null, invites);
        }
    }
}