using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.Consts;
using Newtonsoft.Json;


namespace FiroozehGameService.Handlers.CommandHandlers
{
    internal class AcceptInviteHandler : BaseHandler<AcceptInviteHandler>
    {
        public static new string Signature
            => "ACCEPTINVITE";

        public AcceptInviteHandler(CommandHandler _handler)
            => this._commandHander = _handler;

        protected Packet DoAction(RoomDetail inviteOptions)
            => new Packet(
                _commandHander.PlayerHash,
                Command.ActionAcceptInvite,
                JsonConvert.SerializeObject(inviteOptions));

        protected override bool CheckAction(object payload)
            => payload.GetType() == typeof(RoomDetail);

        protected override Packet DoAction(object payload)
        {
            if (!CheckAction(payload)) throw new System.ArgumentException();
            return DoAction(payload as RoomDetail);
        }

    }
}
