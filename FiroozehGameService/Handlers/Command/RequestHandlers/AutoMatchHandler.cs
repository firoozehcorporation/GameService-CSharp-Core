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

        public AutoMatchHandler() {}

        private static Packet DoAction(GSLiveOption.AutoMatchOption options)
        {
            if (!CommandHandler.IsAvailable) throw new GameServiceException("GSLiveService Not Available yet");
            return new Packet(
                CommandHandler.PlayerHash,
                Models.Consts.Command.ActionAutoMatch,
                JsonConvert.SerializeObject(new RoomDetail
                {
                    Role = options.Role,
                    Min = options.MinPlayer,
                    Max = options.MaxPlayer,
                    Type = Models.Consts.Command.ActionAutoMatch,
                    IsPersist = options.IsPersist,
                    GsLiveType = (int)options.GsLiveType
                }));
        }

        protected override bool CheckAction(object payload)
            => payload.GetType() == typeof(GSLiveOption.AutoMatchOption);

        protected override Packet DoAction(object payload)
        {
            if (!CheckAction(payload)) throw new System.ArgumentException();
            return DoAction(payload as GSLiveOption.AutoMatchOption);
        }

    }
}
