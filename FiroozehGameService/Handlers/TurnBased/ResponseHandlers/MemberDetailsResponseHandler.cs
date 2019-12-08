
using System.Collections.Generic;
using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.GSLive;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.TurnBased.ResponseHandlers
{
    internal class MemberDetailsResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand => TB.GetUsers;

        protected override void HandleResponse(Packet packet)
        {
           TurnBasedEventHandlers.RoomMembersDetailReceived?.Invoke(this,JsonConvert.DeserializeObject<List<Member>>(packet.Data));
        }
      
    }
}
