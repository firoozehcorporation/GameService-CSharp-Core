using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.Consts;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.CommandServer_RequestHandlers
{
    internal class AuthorizationHandler : BaseHandler
    {
        public static new string Signature =>
            "AUTHORIZATION";

        public AuthorizationHandler(CommandHandler _handler) =>
            this._commandHander = _handler;

        protected override Packet DoAction(object payload = null)
        => new Packet(
                null,
                Command.ActionAuth,
                JsonConvert.SerializeObject(
                    new AuthPayload(CommandHandler.RoomId, CommandHandler.UserToken)));

        protected override bool CheckAction(object payload)
            => true;
    }
}
