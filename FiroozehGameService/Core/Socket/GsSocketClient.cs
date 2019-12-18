using FiroozehGameService.Core.Socket.PacketValidators;
using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.EventArgs;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FiroozehGameService.Core.Socket
{
    internal abstract class GsSocketClient
    {
        #region Fields
        private const int BufferCapacity = 1024 * 128;
        protected Area Endpoint;
        protected CancellationTokenSource OperationCancellationToken;

        //TODO replace string to byteArrayStream
        protected StringBuilder DataBuilder = new StringBuilder();

        protected byte[] Buffer = new byte[BufferCapacity];
        protected const int BufferOffset = 0;
        protected int BufferReceivedBytes = 0;
        protected readonly IValidator PacketValidator = new JsonDataValidator();
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

        internal abstract void Send(byte[] buffer);
        
        internal abstract Task SendAsync(byte[] buffer);

        internal abstract Task StartReceiving();

        internal abstract void StopReceiving();
    }
    
}
