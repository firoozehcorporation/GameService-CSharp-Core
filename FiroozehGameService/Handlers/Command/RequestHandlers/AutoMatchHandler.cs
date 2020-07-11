using System;
using FiroozehGameService.Core.GSLive;
using FiroozehGameService.Models;
using FiroozehGameService.Models.GSLive.Command;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.Command.RequestHandlers
{
    internal class AutoMatchHandler : BaseRequestHandler
    {
        public static string Signature =>
            "AUTO_MATCH";

        private static Packet DoAction(GSLiveOption.AutoMatchOption options)
        {
            if (!CommandHandler.IsAvailable) throw new GameServiceException("GSLiveService Not Available yet");
            return new Packet(
                CommandHandler.PlayerHash,
                Models.Consts.Command.ActionAutoMatch,
                GetBuffer(JsonConvert.SerializeObject(new RoomDetail
                {
                    Role = options.Role,
                    Min = options.MinPlayer,
                    Max = options.MaxPlayer,
                    Type = Models.Consts.Command.ActionAutoMatch,
                    IsPersist = options.IsPersist,
                    GsLiveType = (int) options.GsLiveType
                }, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore
                })));
        }

        protected override bool CheckAction(object payload)
        {
            return payload.GetType() == typeof(GSLiveOption.AutoMatchOption);
        }

        protected override Packet DoAction(object payload)
        {
            if (!CheckAction(payload)) throw new ArgumentException();
            return DoAction(payload as GSLiveOption.AutoMatchOption);
        }
    }
}