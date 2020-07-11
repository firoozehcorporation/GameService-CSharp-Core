using System.Collections.Generic;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.GSLive;
using FiroozehGameService.Models.GSLive.Command;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.TurnBased.ResponseHandlers
{
    internal class MemberDetailsResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand => TB.GetMembers;

        protected override void HandleResponse(Packet packet)
        {
            TurnBasedEventHandlers.RoomMembersDetailReceived?.Invoke(this,
                JsonConvert.DeserializeObject<List<Member>>(GetStringFromBuffer(packet.Data)));
        }
    }
}