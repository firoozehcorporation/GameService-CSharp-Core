using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.GSLive.Command;

namespace FiroozehGameService.Handlers.TurnBased.ResponseHandlers
{
    internal class PingResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand => TB.ActionPingPong;

        protected override void HandleResponse(Packet packet)
        {
           CoreEventHandlers.Ping?.Invoke(this,null);
        }
      
    }
}
