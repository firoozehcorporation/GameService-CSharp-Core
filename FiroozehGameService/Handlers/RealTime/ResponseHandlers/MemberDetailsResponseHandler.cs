using System.Collections.Generic;
using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.GSLive;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.RealTime.ResponseHandlers
{
    internal class MemberDetailsResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand => RT.ActionMembersDetail;

        protected override void HandleResponse(Packet packet,GProtocolSendType type)
        {
           TurnBasedEventHandlers.RoomMembersDetailReceived?.Invoke(this,JsonConvert.DeserializeObject<List<Member>>(packet.Data));
        }
      
    }
}
