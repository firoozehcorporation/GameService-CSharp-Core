using FiroozehGameService.Models;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.GSLive.RT;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.RealTime.RequestHandlers
{
    internal class AuthorizationHandler : BaseRequestHandler
    {
        public static string Signature =>
            "AUTHORIZATION";

        protected override Packet DoAction(object payload)
        {
            if (!RealTimeHandler.IsAvailable) throw new GameServiceException("GSLiveRealTime Not Available yet");
            return new Packet(
                null,
                RT.ActionAuth,
                GProtocolSendType.Reliable, GetBuffer(JsonConvert.SerializeObject(
                    new AuthPayload(RealTimeHandler.CurrentRoom?.Id, RealTimeHandler.PlayToken)
                    , new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        DefaultValueHandling = DefaultValueHandling.Ignore
                    }))
            );
        }

        protected override bool CheckAction(object payload)
        {
            return true;
        }
    }
}