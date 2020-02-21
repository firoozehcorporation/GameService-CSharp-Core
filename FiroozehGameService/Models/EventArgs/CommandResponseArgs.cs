using FiroozehGameService.Models.GSLive.Command;

namespace FiroozehGameService.Models.EventArgs
{
    internal class CommandResponseArgs : System.EventArgs
    {
        public Packet CommandPacket { set; get; }
    }
}
