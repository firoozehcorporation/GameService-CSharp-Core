using FiroozehGameService.Models.Command;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.Command.RequestHandlers
{
    internal class GetRoomsHandler : BaseRequestHandler
    {
        public static string Signature
            => "AVAILABLE_ROOMS";

        public GetRoomsHandler(CommandHandler handler)
            => CommandHandler = handler;

        private static Packet DoAction(RoomDetail roomOptions)
            => new Packet(
                CommandHandler.PlayerHash,
                Models.Consts.Command.ActionGetRooms,
                JsonConvert.SerializeObject(roomOptions));

        protected override bool CheckAction(object payload)
            => payload.GetType() == typeof(RoomDetail);

        protected override Packet DoAction(object payload)
        {
            if (!CheckAction(payload)) throw new System.ArgumentException();
            return DoAction(payload as RoomDetail);
        }

    }
}
