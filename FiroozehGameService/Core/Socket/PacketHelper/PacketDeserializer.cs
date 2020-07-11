using System;
using System.Linq;
using FiroozehGameService.Models.Enums.GSLive;
using FiroozehGameService.Models.GSLive;
using FiroozehGameService.Models.GSLive.Command;
using FiroozehGameService.Utils;

namespace FiroozehGameService.Core.Socket.PacketHelper
{
    internal class PacketDeserializer : IDeserializer
    {
        public APacket Deserialize(byte[] buffer, int offset, int receivedBytes,GSLiveType type)
        {
            try
            {
                var seg = new ArraySegment<byte>(buffer,offset,receivedBytes);
                switch (type)
                {
                    case GSLiveType.Core:
                    case GSLiveType.TurnBased:
                        return new Packet(seg.ToArray());
                    case GSLiveType.RealTime:
                        return new Models.GSLive.RT.Packet(seg.ToArray());
                    case GSLiveType.NotSet:
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                }
            }
            catch (Exception e)
            {
                LogUtil.LogError(this,"PacketDeserializer Err : " + e.Message);
                return null;
            }
           
        }
    }
}