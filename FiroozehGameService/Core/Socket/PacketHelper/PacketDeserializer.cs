using System;
using System.Linq;
using FiroozehGameService.Models.GSLive;
using FiroozehGameService.Models.GSLive.Command;
using FiroozehGameService.Utils;
using Newtonsoft.Json;

namespace FiroozehGameService.Core.Socket.PacketHelper
{
    internal class PacketDeserializer : IDeserializer
    {
        public APacket Deserialize(byte[] buffer)
        {
            try
            {
                return new Models.GSLive.RT.Packet(buffer);
            }
            catch (Exception e)
            {
                LogUtil.LogError(this,"PacketDeserializer Err : " + e.Message);
                return null;
            }
           
        }

        public APacket Deserialize(string buffer)
        {
            try
            {
                return JsonConvert.DeserializeObject<Packet>(buffer);
            }
            catch (Exception e)
            {
                LogUtil.LogError(this,"PacketDeserializer Err : " + e.Message);
                return null;
            }
        }
    }
}