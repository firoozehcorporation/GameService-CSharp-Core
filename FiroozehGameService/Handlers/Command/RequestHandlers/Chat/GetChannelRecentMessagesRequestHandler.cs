using System;
using FiroozehGameService.Models.GSLive.Command;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.Command.RequestHandlers.Chat
{
    internal class GetChannelRecentMessagesRequestHandler : BaseRequestHandler
    {
        public static string Signature
            => "GET_CHANNEL_RECENT_MESSAGES";

        private static Packet DoAction(RoomDetail room)
        {
            return new Packet(
                CommandHandler.PlayerHash,
                Models.Consts.Command.ActionGetLastChats,
                JsonConvert.SerializeObject(room,
                    new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore})
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