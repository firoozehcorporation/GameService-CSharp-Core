using FiroozehGameService.Core.GSLive;
using FiroozehGameService.Models;
using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.Consts;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.CommandHandlers
{
    internal class AutoMatchHandler : BaseHandler<AutoMatchHandler>
    {
        public static new string Signature =>
          "AUTOMATCH";

        public AutoMatchHandler(CommandHandler _handler) =>
            this._commandHander = _handler;

        protected Packet DoAction(GSLiveOption.AutoMatchOption options)
        {
            if (!_commandHander.IsAvailable) throw new GameServiceException("GSLiveService Not Available yet");
            return new Packet(
                _commandHander.PlayerHash,
                Command.ActionAutoMatch,
                JsonConvert.SerializeObject(new RoomDetail()
                {
                    Role = options.Role,
                    Min = options.MinPlayer,
                    Max = options.MaxPlayer,
                    Type = Command.ActionAutoMatch,
                    GsLiveType = (int)options.RoomType
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
