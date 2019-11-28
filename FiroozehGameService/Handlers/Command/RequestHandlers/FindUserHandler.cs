using FiroozehGameService.Models.Command;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.Command.RequestHandlers
{
    internal class FindUserHandler : BaseRequestHandler
    {
        public static new string Signature
            => "FINDUSER";

        public FindUserHandler(CommandHandler _handler)
            => this.CommandHandler = _handler;

        protected Packet DoAction(RoomDetail options)
            => new Packet(
                CommandHandler.PlayerHash,
                Models.Consts.Command.ActionFindUser,
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
