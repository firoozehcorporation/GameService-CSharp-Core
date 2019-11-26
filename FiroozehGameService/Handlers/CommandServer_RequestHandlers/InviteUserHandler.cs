using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.Consts;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.CommandServer_RequestHandlers
{
    internal class InviteUserHandler : BaseHandler
    {
        public static new string Signature
            => "INVITEUSER";

        public InviteUserHandler(CommandHandler _handler)
            => this._commandHander = _handler;

        protected Packet DoAction(RoomDetail inviteOptions)
            => new Packet(
                _commandHander.PlayerHash,
                Command.ActionInviteUser,
                JsonConvert.SerializeObject(inviteOptions)
                );

        protected override bool CheckAction(object payload)
           => payload.GetType() == typeof(RoomDetail);

        protected override Packet DoAction(object payload)
        {
            if (!CheckAction(payload)) throw new System.ArgumentException();
            return DoAction(payload as RoomDetail);
        }
    }
}
