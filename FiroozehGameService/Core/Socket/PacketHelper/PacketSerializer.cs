using System;
using FiroozehGameService.Models.GSLive;
using FiroozehGameService.Utils;

namespace FiroozehGameService.Core.Socket.PacketHelper
{
    internal class PacketSerializer : ISerializer
    {
        public byte[] Serialize(APacket packet)
        {
            try
            {
                return packet.Serialize();
            }
            catch (Exception e)
            {
                LogUtil.LogError(this, "PacketSerializer Err :" + e.Message);
                return null;
            }
        }
    }
}