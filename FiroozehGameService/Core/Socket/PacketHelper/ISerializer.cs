using FiroozehGameService.Models.Command;

namespace FiroozehGameService.Core.Socket.PacketHelper
{
    internal interface ISerializer
    {
        byte[] Serialize(Packet packet,string pwd);
    }
}