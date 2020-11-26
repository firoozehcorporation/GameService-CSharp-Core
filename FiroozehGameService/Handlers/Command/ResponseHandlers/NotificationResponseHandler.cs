using FiroozehGameService.Core;
using FiroozehGameService.Models.GSLive.Command;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.Command.ResponseHandlers
{
    internal class NotificationResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand
            => Models.Consts.Command.ActionNotification;

        protected override void HandleResponse(Packet packet)
        {
            /*var notification = JsonConvert.DeserializeObject<Notification>(packet.Data);
            GameService.OnNotificationReceived(notification);*/
        }
    }
}