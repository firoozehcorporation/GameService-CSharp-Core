using FiroozehGameService.Models.Command;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FiroozehGameService.Core.Socket
{
    internal class GsUdpClient : GsSocketClient
    {
        private UdpClient _client;

        public GsUdpClient(Area endpoint)
        {
            if (endpoint.Protocol.ToUpper() != "UDP")
                throw new InvalidOperationException("Only UDP Protocol Supported");

            _endpoint = endpoint;
            _client = new UdpClient(endpoint.IP, endpoint.Port);
        }

        public override async Task StartReceiving()
        {
            UdpReceiveResult packet;
            while (true)
            {
                try
                {
                    packet = await _client.ReceiveAsync();

                    OnDataReceived(new Models.EventArgs.SocketDataReceived()
                    { Message = Encoding.UTF8.GetString(packet.Buffer) });
                }
                catch (OperationCanceledException) { break; }
                catch (ObjectDisposedException) { break; }
            }
        }

        public override Task Init()
        {
            _operaionCancelationToken = null;
            _buffer = null;
            _dataBuilder = null;

            return Task.CompletedTask;
        }

        public override async Task Send(byte[] buffer)
            => await _client.SendAsync(buffer, buffer.Length, _endpoint.IP, _endpoint.Port);

        public override void StopReceiving()
        {
            _client.Close();
            _client.Dispose();
        }
    }
}
