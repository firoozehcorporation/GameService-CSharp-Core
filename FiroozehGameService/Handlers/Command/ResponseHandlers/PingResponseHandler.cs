using FiroozehGameService.Models.Command;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.Command.ResponseHandlers
{
    internal class PingResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand 
            => Models.Consts.Command.ActionPing;

        protected override void HandleResponse(Packet packet)
        {
            CoreEventHandlers.OnPing?.Invoke(this,null);
        }
    }
}
