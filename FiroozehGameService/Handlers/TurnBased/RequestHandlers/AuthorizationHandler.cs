using FiroozehGameService.Models.Command;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.TurnBased.RequestHandlers
{
    internal class AuthorizationHandler : BaseRequestHandler
    {
        public static string Signature =>
            "AUTHORIZATION";

        public AuthorizationHandler(TurnBasedHandler handler) =>
            TurnBasedHandler = handler;

        protected override Packet DoAction(object payload)
            => new Packet(
                null,
                Models.Consts.Command.ActionAuth,
                JsonConvert.SerializeObject(
                    new AuthPayload(TurnBasedHandler.CurrentRoom?.Id, TurnBasedHandler.UserToken)));


        protected override bool CheckAction(object payload)
            => true;
    }
}
