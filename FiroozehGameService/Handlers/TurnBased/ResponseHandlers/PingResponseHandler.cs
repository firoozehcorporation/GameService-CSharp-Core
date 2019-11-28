
using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.Consts;

namespace FiroozehGameService.Handlers.TurnBased.ResponseHandlers
{
    internal class PingResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand => TB.ActionPingPong;

        protected override void HandleResponse(Packet packet)
        {
           CoreEventHandlers.OnPing?.Invoke(this,null);
        }
      
    }
}
