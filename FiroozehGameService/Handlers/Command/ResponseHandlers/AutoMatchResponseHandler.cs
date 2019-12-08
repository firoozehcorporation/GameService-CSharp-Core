using System.Collections.Generic;
using FiroozehGameService.Models.BasicApi;
using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.Enums.GSLive;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.Command.ResponseHandlers
{
    internal class AutoMatchResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand 
            => Models.Consts.Command.ActionAutoMatch;

        protected override void HandleResponse(Packet packet)
        {
            if (packet.Message == "waiting_queue")
            {
                CommandEventHandler.AutoMatchUpdated?.Invoke(null,new AutoMatchEvent
                {
                    Status = AutoMatchStatus.OnWaiting,
                    Players = new List<User>()
                });
            }
            else
            {
                CommandEventHandler.AutoMatchUpdated?.Invoke(null,new AutoMatchEvent
                {
                    Status = AutoMatchStatus.OnUserJoined,
                    Players = JsonConvert.DeserializeObject<List<User>>(packet.Data)
                });
            }
        }
    }
}
