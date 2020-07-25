using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.Enums.GSLive;
using FiroozehGameService.Models.GSLive;
using FiroozehGameService.Models.GSLive.Command;
using Newtonsoft.Json;
using Packet = FiroozehGameService.Models.GSLive.RT.Packet;

namespace FiroozehGameService.Handlers.RealTime.ResponseHandlers
{
    internal class JoinRoomResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand => RT.ActionJoin;

        protected override void HandleResponse(Packet packet, GProtocolSendType type)
        {
            var joinData = JsonConvert.DeserializeObject<JoinData>(GetStringFromBuffer(packet.Payload));
            RealTimeEventHandlers.JoinedRoom?.Invoke(this, new JoinEvent
            {
                Type = GSLiveType.RealTime,
                JoinData = joinData
            });
        }
    }
}