using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.EventArgs;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FiroozehGameService.Core.Socket
{
    internal class GsTcpClient : GsSocketClient
    {
        private readonly TcpClient _client;
        private NetworkStream _clientStream;
        public bool IsAvailable { get; private set; }

        public GsTcpClient(Area area)
        {
            if (area.Protocol.ToUpper() != "TCP")
                throw new InvalidOperationException("Only TCP Protocol Supported");
            
            _client = new TcpClient();
            Endpoint = area;
        }

        public override async Task Init()
        {
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
                    if (!_clientStream.DataAvailable) continue;
                    BufferReceivedBytes += await _clientStream.ReadAsync(
                        Buffer,
                        BufferOffset,
                        Buffer.Length - BufferOffset,
                        OperationCancellationToken.Token);

                    
                    DataBuilder.Append(Encoding.UTF8.GetString(Buffer, BufferOffset, BufferReceivedBytes));
                    BufferReceivedBytes = 0;
                    
                    
                    var packets = PacketValidator.ValidateDataAndReturn(DataBuilder.ToString());
                    foreach (var packet in packets)                        
                        OnDataReceived(new SocketDataReceived {Data = packet});
                    

                    DataBuilder.Clear();
                }
                catch (OperationCanceledException e)
                {
                    /* nothing to be afraid of :3 */
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
            OperationCancellationToken.Cancel(true);
            _client?.Close();
            IsAvailable = false;
        }
    }
}
