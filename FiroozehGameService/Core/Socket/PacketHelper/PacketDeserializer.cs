using System;
using System.Linq;
using FiroozehGameService.Models.GSLive;
using FiroozehGameService.Models.GSLive.RT;
using FiroozehGameService.Utils;
using Newtonsoft.Json;

namespace FiroozehGameService.Core.Socket.PacketHelper
{
    internal class PacketDeserializer : IDeserializer
    {
        public APacket Deserialize(byte[] buffer, int offset, int receivedBytes)
        {
            try
            {
                var seg = new ArraySegment<byte>(buffer, offset, receivedBytes);
                return new Packet(seg.ToArray());
            }
            catch (Exception e)
            {
                LogUtil.LogError(this, "PacketDeserializer Err : " + e.Message);
                return null;
            }
        }

        public APacket Deserialize(string buffer)
        {
            try
            {
                return JsonConvert.DeserializeObject<Models.GSLive.Command.Packet>(buffer);
            }
            catch (Exception e)
            {
                LogUtil.LogError(this, "PacketDeserializer Err : " + e.Message);
                return null;
            }
        }
    }
}