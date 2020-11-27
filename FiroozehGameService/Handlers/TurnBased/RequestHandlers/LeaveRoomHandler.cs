using System;
using FiroozehGameService.Models;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.GSLive.Command;
using FiroozehGameService.Models.GSLive.TB;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.TurnBased.RequestHandlers
{
    internal class LeaveRoomHandler : BaseRequestHandler
    {
        public static string Signature =>
            "LEAVE_ROOM";

        private static Packet DoAction(DataPayload payload)
        {
            return new Packet(TurnBasedHandler.PlayerHash, TB.OnLeave,
                JsonConvert.SerializeObject(payload
                    , new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        DefaultValueHandling = DefaultValueHandling.Ignore
                    })
            );
        }

        protected override Packet DoAction(object payload)
        {
            if (!TurnBasedHandler.IsAvailable) throw new GameServiceException("GSLiveTurnBased Not Available yet");
            if (!CheckAction(payload)) throw new ArgumentException();
            return DoAction(payload as DataPayload);
        }

        protected override bool CheckAction(object payload)
        {
            if (payload == null) return true;
            return payload.GetType() == typeof(DataPayload);
        }
    }
}