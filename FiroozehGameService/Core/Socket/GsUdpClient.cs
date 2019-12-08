using FiroozehGameService.Models.Command;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using FiroozehGameService.Models.EventArgs;

namespace FiroozehGameService.Core.Socket
{
    internal class GsUdpClient : GsSocketClient
    {
        private UdpClient _client;
        public bool IsAvailable { get; private set; }
        private IPEndPoint _endPoint;

        public GsUdpClient(Area endpoint)
        {
            if (endpoint.Protocol.ToUpper() != "UDP")
                throw new InvalidOperationException("Only UDP Protocol Supported");

            Endpoint = endpoint;
            _endPoint = new IPEndPoint(IPAddress.Parse(endpoint.Ip),endpoint.Port);
            _client = new UdpClient(endpoint.Ip, endpoint.Port);
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
            Buffer = null;
            DataBuilder = null;

            return Task.CompletedTask;
        }
        
        public override void Send(byte[] buffer)
           => Task.Run(() => { _client?.Send(buffer, buffer.Length); },OperationCancellationToken.Token);
        
        public override async Task SendAsync(byte[] buffer)
           => await _client.SendAsync(buffer, buffer.Length);
        


        public override void StopReceiving()
        {
            try
            {
                OperationCancellationToken?.Cancel(true);
                _client?.Close();
                _client = null;
                IsAvailable = false;
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
