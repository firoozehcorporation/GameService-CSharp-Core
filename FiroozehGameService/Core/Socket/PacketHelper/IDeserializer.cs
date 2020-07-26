using FiroozehGameService.Models.GSLive;

namespace FiroozehGameService.Core.Socket.PacketHelper
{
    internal interface IDeserializer
    {
        APacket Deserialize(byte[] buffer);
        APacket Deserialize(string buffer);
    }
}