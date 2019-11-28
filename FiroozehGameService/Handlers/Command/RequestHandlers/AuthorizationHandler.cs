using FiroozehGameService.Models.Command;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.Command.RequestHandlers
{
    internal class AuthorizationHandler : BaseRequestHandler
    {
        public static string Signature =>
            "AUTHORIZATION";

        public AuthorizationHandler(CommandHandler handler) =>
            CommandHandler = handler;

        protected override Packet DoAction(object payload)
        => new Packet(
                null,
                Models.Consts.Command.ActionAuth,
                JsonConvert.SerializeObject(
                    new AuthPayload(CommandHandler.RoomId, CommandHandler.UserToken)));

        protected override bool CheckAction(object payload)
            => true;
    }
}
