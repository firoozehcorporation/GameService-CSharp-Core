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
            await _client.ConnectAsync(Endpoint.IP, Endpoint.Port);
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

                    if (!PacketValidator.ValidateData(DataBuilder)) continue;
                    OnDataReceived(new SocketDataReceived {Data = DataBuilder.ToString()});
                    DataBuilder.Clear();
                }
                catch (OperationCanceledException)
                {
                    /* nothing to be afraid of :3 */
                    IsAvailable = false;
                    //OnClosed(new ErrorArg {Error = e.Message});
                    break;
                }
                catch (ObjectDisposedException)
                {
                    IsAvailable = false;
                    //OnClosed(new ErrorArg {Error = e.Message});
                    break;
                }
            }
        }

        public override async Task Send(byte[] buffer)
            => await _clientStream.WriteAsync(buffer, 0, buffer.Length);

        public override void StopReceiving()
        {
            OperationCancellationToken.Cancel(true);
            _client.Close();
            //unsafe, haven't tested yet
            _client.Dispose();
            IsAvailable = false;
        }
    }
}
