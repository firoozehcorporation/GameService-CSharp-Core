using FiroozehGameService.Models.Enums.GSLive.RT;

namespace FiroozehGameService.Models.Command
{
    public class MessageReceiveEvent
    {
        public GSLive.Message Message { get; set; }
        public MessageType MessageType { get; set; }
    }
}