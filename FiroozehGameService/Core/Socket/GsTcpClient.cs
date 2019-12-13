using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.EventArgs;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FiroozehGameService.Core.Socket
{
    internal class GsTcpClient : GsSocketClient
    {
        private TcpClient _client;
        private NetworkStream _clientStream;
        public bool IsAvailable { get; private set; }

        public GsTcpClient(Area area)
        {
            if (area.Protocol.ToUpper() != "TCP")
                throw new InvalidOperationException("Only TCP Protocol Supported");
            
            Endpoint = area;
        }

        public override async Task Init()
        {
            _client = new TcpClient();
            OperationCancellationToken = new CancellationTokenSource();
            await _client.ConnectAsync(Endpoint.Ip, Endpoint.Port);
            _clientStream = _client.GetStream();
            IsAvailable = true;
        }

       

        public override async Task StartReceiving()
        {
            while (true)
            {
                try
                {
                    BufferReceivedBytes += await _clientStream.ReadAsync(
                        Buffer,
                        BufferOffset,
                        Buffer.Length - BufferOffset,
                        OperationCancellationToken.Token);

                    DataBuilder.Append(Encoding.UTF8.GetString(Buffer, BufferOffset, BufferReceivedBytes));
                    BufferReceivedBytes = 0;
                    
                    var packets = PacketValidator.ValidateDataAndReturn(DataBuilder);
                    foreach (var packet in packets)                        
                        OnDataReceived(new SocketDataReceived {Data = packet}); 
                }
                catch (OperationCanceledException e)
                {
                    IsAvailable = false;
                    OnClosed(new ErrorArg {Error = e.Message});
                    break;
                }
                catch (ObjectDisposedException e)
                {
                    IsAvailable = false;
                    OnClosed(new ErrorArg {Error = e.Message});
                    break;
                }
            }
        }
        

        public override void Send(byte[] buffer)
            => Task.Run(() =>
            {
                _clientStream?.Write(buffer, 0, buffer.Length);
            },OperationCancellationToken.Token);
        
        
        public override async Task SendAsync(byte[] buffer)
            => await _clientStream.WriteAsync(buffer, 0, buffer.Length);
        


        public override void StopReceiving()
        {
            try
            {
                OperationCancellationToken?.Cancel(true);
                OperationCancellationToken?.Dispose();
                _client?.Close();
                IsAvailable = false;
            }
            catch
            {
                // ignored
            }
        }
    }
}
