using System.Collections.Generic;
using FiroozehGameService.Models.GSLive;
using FiroozehGameService.Models.GSLive.Command;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.Command.ResponseHandlers
{
    internal class FindMemberResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand
            => Models.Consts.Command.ActionFindMember;

        protected override void HandleResponse(Packet packet)
        {
            var members = JsonConvert.DeserializeObject<List<Member>>(GetStringFromBuffer(packet.Data));
            CommandEventHandler.FindMemberReceived?.Invoke(null, members);
        }
    }
}