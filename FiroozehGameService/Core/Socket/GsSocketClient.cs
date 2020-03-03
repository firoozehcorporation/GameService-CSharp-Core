using FiroozehGameService.Models.EventArgs;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FiroozehGameService.Core.Socket.PacketHelper;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.Enums.GSLive;
using FiroozehGameService.Models.GSLive.Command;

namespace FiroozehGameService.Core.Socket
{
    internal abstract class GsSocketClient
    {
        #region Fields
        private const int BufferCapacity = 1024 * 128;
        protected Area Endpoint;
        protected string Pwd;
        protected GSLiveType Type;
        protected CancellationTokenSource OperationCancellationToken;

        protected readonly byte[] Buffer = new byte[BufferCapacity];
        protected const int BufferOffset = 0;
        protected int BufferReceivedBytes = 0;
        protected readonly IValidator PacketValidator = new JsonDataValidator();
        protected readonly IDeserializer PacketDeserializer = new PacketDeserializer();
        protected readonly ISerializer PacketSerializer = new PacketSerializer();
        #endregion

        public event EventHandler<SocketDataReceived> DataReceived;
        public event EventHandler<ErrorArg> Error;


        protected void OnDataReceived(SocketDataReceived arg)
        {
            DataReceived?.Invoke(this, arg);
        }
        
        protected void OnClosed(ErrorArg errorArg)
        {
            Error?.Invoke(this,errorArg);
        }

        internal abstract Task Init();

        internal abstract void UpdatePwd(string newPwd);
        
        internal abstract void SetType(GSLiveType type);

        internal abstract void Send(Packet packet);
        
        internal abstract Task SendAsync(Packet packet);

        internal abstract Task StartReceiving();

        internal abstract void StopReceiving();
    }
    
}
