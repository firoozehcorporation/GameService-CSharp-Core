using FiroozehGameService.Models.Enums.GSLive;
using FiroozehGameService.Models.GSLive;

namespace FiroozehGameService.Core.Socket.PacketHelper
{
    internal interface ISerializer
    {
        byte[] Serialize(APacket packet,string pwd,GSLiveType type);
    }
}