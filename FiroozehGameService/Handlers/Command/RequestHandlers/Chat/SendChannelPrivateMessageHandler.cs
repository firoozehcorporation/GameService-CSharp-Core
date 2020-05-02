using System;
using FiroozehGameService.Models.GSLive.Command;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.Command.RequestHandlers.Chat
{
    internal class SendChannelPrivateMessageHandler : BaseRequestHandler
    {
        public static string Signature
            => "SEND_PRIVATE_MESSAGE";

        private static Packet DoAction(Tuple<string, string> channelMessage)
        {
            return new Packet(
                CommandHandler.PlayerHash,
                Models.Consts.Command.ActionPrivateChat,
                JsonConvert.SerializeObject(
                    new Message(
                        true,
                        channelMessage.Item1,
                        null,
                        channelMessage.Item2),
                    new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore})
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