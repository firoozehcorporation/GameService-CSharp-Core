using FiroozehGameService.Models.GSLive.Command;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.Command.RequestHandlers
{
    internal class AuthorizationHandler : BaseRequestHandler
    {
        public static string Signature =>
            "AUTHORIZATION";

        protected override Packet DoAction(object payload)
        {
            return new Packet(
                null,
                Models.Consts.Command.ActionAuth,
                GetBuffer(JsonConvert.SerializeObject(
                    new AuthPayload(CommandHandler.GameId, CommandHandler.UserToken))));
        }

        protected override bool CheckAction(object payload)
        {
            return true;
        }
    }
}