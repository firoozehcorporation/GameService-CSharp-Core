using System;
using FiroozehGameService.Models.GSLive.Command;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.Command.RequestHandlers.Chat
{
    internal class SendChannelPublicMessageHandler : BaseRequestHandler
    {
        public static string Signature
            => "SEND_PUBLIC_MESSAGE";

        private static Packet DoAction(Tuple<string, string> channelMessage)
        {
            return new Packet(
                CommandHandler.PlayerHash,
                Models.Consts.Command.ActionPublicChat,
                GetBuffer(JsonConvert.SerializeObject(
                    new Message(
                        false,
                        channelMessage.Item1,
                        null,
                        channelMessage.Item2),
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        DefaultValueHandling = DefaultValueHandling.Ignore
                    }))
            );
        }

        protected override bool CheckAction(object payload)
        {
            return payload.GetType() == typeof(Tuple<string, string>);
        }

        protected override Packet DoAction(object payload)
        {
            if (!CheckAction(payload)) throw new ArgumentException();
            return DoAction(payload as Tuple<string, string>);
        }
    }
}