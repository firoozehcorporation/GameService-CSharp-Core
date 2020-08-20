using FiroozehGameService.Models.Enums.GSLive.Command;
using FiroozehGameService.Models.GSLive.Command;

namespace FiroozehGameService.Handlers.Command.ResponseHandlers
{
    internal class CancelAutoMatchResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand
            => Models.Consts.Command.ActionCancelAutoMatch;

        protected override void HandleResponse(Packet packet)
        {
            CommandEventHandler.AutoMatchCanceled?.Invoke(this,
                packet.Message == "leaved" ? AutoMatchCancel.Success : AutoMatchCancel.NotInQueue);
        }
    }
}