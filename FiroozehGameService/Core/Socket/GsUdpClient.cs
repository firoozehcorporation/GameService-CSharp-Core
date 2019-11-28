using FiroozehGameService.Models.Command;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using FiroozehGameService.Models.EventArgs;

namespace FiroozehGameService.Core.Socket
{
    internal class GsUdpClient : GsSocketClient
    {
        private readonly UdpClient _client;
        public bool IsAvailable { get; private set; }

        public GsUdpClient(Area endpoint)
        {
            if (endpoint.Protocol.ToUpper() != "UDP")
                throw new InvalidOperationException("Only UDP Protocol Supported");

            Endpoint = endpoint;
            _client = new UdpClient(endpoint.IP, endpoint.Port);
            IsAvailable = true;
        }

        public override async Task StartReceiving()
        {
            while (true)
            {
                try
                {
                    var packet = await _client.ReceiveAsync();

                    OnDataReceived(new SocketDataReceived { Data = Encoding.UTF8.GetString(packet.Buffer) });
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

        public override Task Init()
        {
            OpraitonCancelationToken = null;
            Buffer = null;
            DataBuilder = null;

            return Task.CompletedTask;
        }

        public override async Task Send(byte[] buffer)
            => await _client.SendAsync(buffer, buffer.Length, Endpoint.IP, Endpoint.Port);

        public override void StopReceiving()
        {
            _client.Close();
            _client.Dispose();
            IsAvailable = false;
        }
    }
}
