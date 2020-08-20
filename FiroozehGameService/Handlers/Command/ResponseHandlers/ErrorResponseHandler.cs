using FiroozehGameService.Models.Enums.GSLive;
using FiroozehGameService.Models.GSLive.Command;

namespace FiroozehGameService.Handlers.Command.ResponseHandlers
{
    internal class ErrorResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand
            => Models.Consts.Command.Error;

        protected override void HandleResponse(Packet packet)
        {
            CoreEventHandlers.Error?.Invoke(this, new ErrorEvent
            {
                Type = GSLiveType.Core,
                Error = packet.Message
            });
        }
    }
}