using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.Consts;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.CommandHandlers
{
    internal class FindUserHandler : BaseHandler<FindUserHandler>
    {
        public static new string Signature
            => "FINDUSER";

        public FindUserHandler(CommandHandler _handler)
            => this._commandHander = _handler;

        protected Packet DoAction(RoomDetail options)
            => new Packet(
                _commandHander.PlayerHash,
                Command.ActionFindUser,
                JsonConvert.SerializeObject(options)
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
