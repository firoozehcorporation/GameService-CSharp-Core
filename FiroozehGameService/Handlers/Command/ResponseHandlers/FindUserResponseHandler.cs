using System.Collections.Generic;
using FiroozehGameService.Models.BasicApi;
using FiroozehGameService.Models.GSLive.Command;
using Newtonsoft.Json;
using Invite = FiroozehGameService.Models.GSLive.Invite;

namespace FiroozehGameService.Handlers.Command.ResponseHandlers
{
    internal class FindUserInboxResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand 
            => Models.Consts.Command.ActionFindUser;

        protected override void HandleResponse(Packet packet)
        {
            var users = JsonConvert.DeserializeObject<List<User>>(packet.Data);
            CommandEventHandler.FindUserReceived?.Invoke(null, users);
        }
    }
}
