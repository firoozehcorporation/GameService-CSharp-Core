using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.GSLive.Command;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.TurnBased.RequestHandlers
{
    internal class AuthorizationHandler : BaseRequestHandler
    {
        public static string Signature =>
            "AUTHORIZATION";

        public AuthorizationHandler() {}

        protected override Packet DoAction(object payload)
            => new Packet(
                null,
                TB.ActionAuth,
                JsonConvert.SerializeObject(
                    new AuthPayload(TurnBasedHandler.CurrentRoom?.Id, TurnBasedHandler.PlayToken),
                    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));


        protected override bool CheckAction(object payload)
            => true;
    }
}
