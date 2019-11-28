using FiroozehGameService.Models.Command;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.Command.RequestHandlers
{
    internal class InviteUserHandler : BaseRequestHandler
    {
        public static new string Signature
            => "INVITEUSER";

        public InviteUserHandler(CommandHandler _handler)
            => this.CommandHandler = _handler;

        protected Packet DoAction(RoomDetail inviteOptions)
            => new Packet(
                CommandHandler.PlayerHash,
                Models.Consts.Command.ActionInviteUser,
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
