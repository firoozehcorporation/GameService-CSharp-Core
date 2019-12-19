using System.Collections.Generic;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.GSLive;
using FiroozehGameService.Models.GSLive.RT;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.RealTime.ResponseHandlers
{
    internal class MemberDetailsResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand => RT.ActionMembersDetail;

        protected override void HandleResponse(Packet packet,GProtocolSendType type)
        {
           RealTimeEventHandlers.RoomMembersDetailReceived?.Invoke(this,JsonConvert.DeserializeObject<List<Member>>(packet.Payload));
        }
      
    }
}
