using FiroozehGameService.Models.Command;

namespace FiroozehGameService.Models.EventArgs
{
    internal class CommandResponseArgs : System.EventArgs
    {
        public Packet CommandPacket { set; get; }
    }
}
