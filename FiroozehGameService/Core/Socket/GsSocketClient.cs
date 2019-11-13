using FiroozehGameService.Core.Socket.PacketValidators;
using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.EventArgs;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FiroozehGameService.Core.Socket
{
    internal abstract class GsSocketClient
    {
        #region Fields
        const int BufferCapacity = 1024 * 10;
        protected Area _endpoint;
        protected CancellationTokenSource _operaionCancelationToken = new CancellationTokenSource();

        //TODO replace string to byteArrayStream
        protected StringBuilder _dataBuilder = new StringBuilder();

        protected byte[] _buffer = new byte[BufferCapacity];
        protected int _bufferOffset = 0;
        protected int _bufferReceivedBytes = 0;
        protected IValidator _packetValidator = new JsonDataValidator();
        #endregion

        public event EventHandler<SocketDataReceived> DataReceived;

        protected virtual void OnDataReceived(SocketDataReceived arg)
        {
            DataReceived?.Invoke(this, arg);
        }

        public abstract Task Init();

        public abstract Task Send(byte[] buffer);

        public abstract Task StartReceiving();

        public abstract void StopReceiving();
    }
}
