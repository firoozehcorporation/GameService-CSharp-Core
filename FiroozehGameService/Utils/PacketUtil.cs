using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.GSLive.RT;

namespace FiroozehGameService.Utils
{
    internal static class PacketUtil
    {
        public static bool CheckPacketSize(Packet packet)
        {
            if (packet.Payload != null)
                return packet.Payload.Length <= RT.MaxPacketSize;
            return true;
        }
    }
}