using FiroozehGameService.Core.GSLive;
using FiroozehGameService.Models;
using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.Consts;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.CommandServer_RequestHandlers
{
    internal class CreateRoomHandler : BaseHandler
    {
        public static new string Signature =>
          "CREATEROOM";

        public CreateRoomHandler(CommandHandler handler) =>
            _commandHander = handler;

        protected Packet DoAction(GSLiveOption.CreateRoomOption options)
        {
            if (!_commandHander.IsAvailable) throw new GameServiceException("GSLiveService Not Available yet");
            return new Packet(
                _commandHander.PlayerHash,
                Command.ActionCreateRoom,
                JsonConvert.SerializeObject(new RoomDetail
                {
                    Name = options.RoomName,
                    Role = options.Role,
                    Min = options.MinPlayer,
                    Max = options.MaxPlayer,
                    IsPrivate = options.IsPrivate,
                    Type = Command.ActionCreateRoom,
                    GsLiveType = (int)options.RoomType
                }));
        }

        protected override bool CheckAction(object payload)
            => payload.GetType() == typeof(GSLiveOption.CreateRoomOption);

        protected override Packet DoAction(object payload)
        {
            if (!CheckAction(payload)) throw new System.ArgumentException();
            return DoAction(payload as GSLiveOption.CreateRoomOption);
        }
    }
}
