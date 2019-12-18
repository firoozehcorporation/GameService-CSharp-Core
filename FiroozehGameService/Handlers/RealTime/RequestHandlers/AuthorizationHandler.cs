using FiroozehGameService.Models;
using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.Consts;
using Newtonsoft.Json;
using AuthPayload = FiroozehGameService.Models.GSLive.RT.AuthPayload;

namespace FiroozehGameService.Handlers.RealTime.RequestHandlers
{
    internal class AuthorizationHandler : BaseRequestHandler
    {
        public static string Signature =>
            "AUTHORIZATION";

        public AuthorizationHandler() {}
        
        protected override Packet DoAction(object payload)
        { 
            if (!RealTimeHandler.IsAvailable) throw new GameServiceException("GSLiveRealTime Not Available yet");
            return new Packet(
                null,
                RT.ActionAuth,
                JsonConvert.SerializeObject(
                    new AuthPayload(RealTimeHandler.CurrentRoom?.Id, RealTimeHandler.UserToken),
                    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }
            ));

        }

        protected override bool CheckAction(object payload)
            => true;
    }
}
