using FiroozehGameService.Core;
using FiroozehGameService.Models;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.GSLive.RT;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.RealTime.RequestHandlers
{
    internal class GetMemberHandler : BaseRequestHandler
    {
        public static string Signature =>
            "GET_MEMBERS";

        public GetMemberHandler() {}

        protected override Packet DoAction(object payload)
        { 
            if (!RealTimeHandler.IsAvailable) throw new GameServiceException("GSLiveRealTime Not Available yet");
            return new Packet(RT.ActionData
                , JsonConvert.SerializeObject(
                    new DataPayload(RT.OnMembersDetail, RealTimeHandler.CurrentRoom?.Id, RealTimeHandler.PlayerHash)
                    ,new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
        }

        protected override bool CheckAction(object payload)
            => true;
    }
}
