using FiroozehGameService.Models.GSLive.Command;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.Command.RequestHandlers
{
    internal class GetRoomsHandler : BaseRequestHandler
    {
        public static string Signature
            => "AVAILABLE_ROOMS";

        public GetRoomsHandler(){}

        private static Packet DoAction(RoomDetail roomOptions)
            => new Packet(
                CommandHandler.PlayerHash,
                Models.Consts.Command.ActionGetRooms,
                JsonConvert.SerializeObject(roomOptions, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));

        protected override bool CheckAction(object payload)
            => payload.GetType() == typeof(RoomDetail);

        protected override Packet DoAction(object payload)
        {
            if (!CheckAction(payload)) throw new System.ArgumentException();
            return DoAction(payload as RoomDetail);
        }

    }
}
