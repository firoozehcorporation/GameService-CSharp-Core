using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.Enums.GSLive;

namespace FiroozehGameService.Handlers.TurnBased.ResponseHandlers
{
    internal class ErrorResponseHandler :BaseResponseHandler
    {
        public static int ActionCommand 
            => TB.Errors;

        protected override void HandleResponse(Packet packet)
        {
            CoreEventHandlers.OnError?.Invoke(this,new ErrorEvent
            {
                Type = GSLiveType.TurnBased,
                Error = packet.Message
            });
        }
    }
}
