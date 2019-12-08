using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.GSLive.RT;
using Newtonsoft.Json;
using Packet = FiroozehGameService.Models.GSLive.RT.Packet;

namespace FiroozehGameService.Handlers.RealTime.ResponseHandlers
{
    internal class PingResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand => RT.ActionPingPong;

        protected override void HandleResponse(Packet packet)
        {
           CoreEventHandlers.Ping?.Invoke(this,null);
        }
      
    }
}
