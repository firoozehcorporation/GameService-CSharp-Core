using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.Consts;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.CommandHandlers
{
    internal class JoinRoomHandler : BaseHandler<JoinRoomHandler>
    {
        public static new string Signature
            => "JOINROOM";

        public JoinRoomHandler(CommandHandler _handler)
            => this._commandHander = _handler;

        protected Packet DoAction(RoomDetail room)
            => new Packet(
                _commandHander.PlayerHash,
                Command.ActionJoinRoom,
                JsonConvert.SerializeObject(room)
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
