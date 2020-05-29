using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.GSLive.RT;

namespace FiroozehGameService.Utils
{
    internal static class PacketUtil
    {
        
        public static bool CheckPacketSize(Packet packet)
        {
            return packet?.Payload?.Length <= RT.MaxPacketSize;
        }
    }
}