using System;
using FiroozehGameService.Models.Command;
using FiroozehGameService.Utils;
using GameServiceHelper.Utils;
using Newtonsoft.Json;

namespace FiroozehGameService.Core.Socket.PacketHelper
{
    internal class PacketSerializer : ISerializer
    {
        public byte[] Serialize(Packet packet,string pwd)
        {
            try
            {
                var json = JsonConvert.SerializeObject(packet , new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                LogUtil.Log(this,"PacketSerializer > " + json);
                return Serializer.Serialize(json,pwd);
            }
            catch (Exception e)
            {
                LogUtil.LogError(this,"PacketSerializer Err :" + e.Message);
                return null;
            }
          
        }
    }
}