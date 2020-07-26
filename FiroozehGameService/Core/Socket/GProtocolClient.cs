using System;
using System.Collections.Generic;
using System.Threading;
using FiroozehGameService.Core.Socket.PacketHelper;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.Enums.GSLive;
using FiroozehGameService.Models.EventArgs;
using FiroozehGameService.Models.GSLive.Command;
using FiroozehGameService.Utils;
using GProtocol.Public;
using Packet = FiroozehGameService.Models.GSLive.RT.Packet;

namespace FiroozehGameService.Core.Socket
{
    internal abstract class GProtocolClient
    {
        internal abstract void Init();
        internal abstract void CreateInstance();
        internal abstract void StopReceiving();
        internal abstract void AddToQueue(Packet packet, GProtocolSendType type);

        internal abstract void StartQueueWorker();
        internal abstract void StopQueueWorker();

        

        protected void OnDataReceived(SocketDataReceived arg)
        {
            DataReceived?.Invoke(this, arg);
        }

        protected void OnClosed(ErrorArg errorArg)
        {
            Error?.Invoke(this, errorArg);
        }

        #region GProtocolClient

        protected Client Client;
        protected Area Area;
        protected const GSLiveType Type = GSLiveType.RealTime;
        
        
        internal readonly Queue<byte[]> SendQueue = new Queue<byte[]>();
        internal Event QueueWorkerEvent;
        protected CancellationTokenSource OperationCancellationToken;

        
        protected readonly ISerializer PacketSerializable = new PacketSerializer();
        protected readonly IDeserializer PacketDeserializer = new PacketDeserializer();

        protected internal event EventHandler<SocketDataReceived> DataReceived;
        protected internal event EventHandler<ErrorArg> Error;

        #endregion
    }
}