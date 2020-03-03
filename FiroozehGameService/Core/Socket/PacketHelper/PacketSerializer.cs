using System;
using System.Text;
using FiroozehGameService.Models.Enums.GSLive;
using FiroozehGameService.Models.GSLive;
using FiroozehGameService.Models.GSLive.Command;
using FiroozehGameService.Utils;
using GameServiceHelper.Utils;
using Newtonsoft.Json;

namespace FiroozehGameService.Core.Socket.PacketHelper
{
    internal class PacketSerializer : ISerializer
    {
        public byte[] Serialize(APacket packet,string pwd,GSLiveType type)
        {
            try
            {
                var json = JsonConvert.SerializeObject(packet , new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                var s = Serializer.Serialize(json,KeyTypeUtil.GetPwd(pwd,type));
                LogUtil.Log(this,"PacketSerializer > JSON : " + json +"\r\nSerialized : " + s + "\r\nPWD : " + KeyTypeUtil.GetPwd(pwd,type));
                return s;
            }
            catch (Exception e)
            {
                LogUtil.LogError(this,"PacketSerializer Err :" + e.Message);
                return null;
            }
          
        }
    }
}