using FiroozehGameService.Models.Command;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.Command.RequestHandlers
{
    internal class JoinRoomHandler : BaseRequestHandler
    {
        public static new string Signature
            => "JOINROOM";

        public JoinRoomHandler(CommandHandler _handler)
            => this.CommandHandler = _handler;

        protected Packet DoAction(RoomDetail room)
            => new Packet(
                CommandHandler.PlayerHash,
                Models.Consts.Command.ActionJoinRoom,
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
