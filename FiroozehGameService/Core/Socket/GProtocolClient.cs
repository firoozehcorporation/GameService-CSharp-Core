using System;
using FiroozehGameService.Core.Socket.PacketHelper;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.EventArgs;
using FiroozehGameService.Models.GSLive.RT;
using GProtocol;
using GProtocol.Core;

namespace FiroozehGameService.Core.Socket
{
    internal abstract class GProtocolClient
    {
        #region GProtocolClient
        protected Client Client;
        protected Connection Connection;
        protected string Pwd;
        protected readonly ISerializer PacketSerializable = new PacketSerializer();
        protected readonly IDeserializer PacketDeserializer = new PacketDeserializer();
        
        protected internal event EventHandler<SocketDataReceived> DataReceived;
        protected internal event EventHandler<ErrorArg> Error;
        #endregion

        internal abstract void Init();
        internal abstract void StopReceiving();
        internal abstract void Send(Packet packet, GProtocolSendType type);
        internal abstract void UpdatePwd(string newPwd);



        protected void OnDataReceived(SocketDataReceived arg)
        {
            DataReceived?.Invoke(this, arg);
        }
        
        protected void OnClosed(ErrorArg errorArg)
        {
            Error?.Invoke(this,errorArg);
        }
    }
}