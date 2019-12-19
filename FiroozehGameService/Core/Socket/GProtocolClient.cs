using System;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.EventArgs;
using GProtocol;
using GProtocol.Core;

namespace FiroozehGameService.Core.Socket
{
    internal abstract class GProtocolClient
    {
        #region GProtocolClient
        protected Client Client;
        protected Connection Connection;
        protected internal event EventHandler<SocketDataReceived> DataReceived;
        protected internal event EventHandler<ErrorArg> Error;
        #endregion

        internal abstract void Init();
        internal abstract void StopReceiving();
        internal abstract void Send(byte[] buffer, GProtocolSendType type);
      


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