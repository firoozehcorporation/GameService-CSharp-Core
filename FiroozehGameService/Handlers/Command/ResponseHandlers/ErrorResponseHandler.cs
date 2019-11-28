using FiroozehGameService.Models.Command;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.Command.ResponseHandlers
{
    internal class ErrorResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand 
            => Models.Consts.Command.Error;

        protected override void HandleResponse(Packet packet)
        {
            CoreEventHandlers.OnError?.Invoke(null,packet.Message);
        }
    }
}
