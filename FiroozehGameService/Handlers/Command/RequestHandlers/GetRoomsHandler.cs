using System;
using FiroozehGameService.Models.GSLive.Command;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.Command.RequestHandlers
{
    internal class GetRoomsHandler : BaseRequestHandler
    {
        public static string Signature
            => "AVAILABLE_ROOMS";

        private static Packet DoAction(RoomDetail roomOptions)
        {
            return new Packet(
                CommandHandler.PlayerHash,
                Models.Consts.Command.ActionGetRooms,
                GetBuffer(JsonConvert.SerializeObject(roomOptions
                    , new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        DefaultValueHandling = DefaultValueHandling.Ignore
                    }))
            );
        }

        protected override bool CheckAction(object payload)
        {
            return payload.GetType() == typeof(RoomDetail);
        }

        protected override Packet DoAction(object payload)
        {
            if (!CheckAction(payload)) throw new ArgumentException();
            return DoAction(payload as RoomDetail);
        }
    }
}