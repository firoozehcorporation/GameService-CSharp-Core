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
        private TcpClient _client;
        private NetworkStream _clientStream;

        // public bool IsAvailable { set; get; }

        public GsTcpClient(Area area)
        {
            if (area.Protocol.ToUpper() != "TCP")
                throw new InvalidOperationException("Only TCP Protocol Supported");

            _client = new TcpClient();
            _endpoint = area;
        }

        public override async Task Init()
        {
            await _client.ConnectAsync(_endpoint.IP, _endpoint.Port);
            _clientStream = _client.GetStream();
            //IsAvailable = true;
        }

        public override async Task StartReceiving()
        {
            while (true)
            {
                try
                {
                    _bufferReceivedBytes += await _clientStream.ReadAsync(
                        _buffer,
                        _bufferOffset,
                        _buffer.Length - _bufferOffset,
                        _operaionCancelationToken.Token);

                    _dataBuilder.Append(Encoding.UTF8.GetString(_buffer, _bufferOffset, _bufferReceivedBytes));
                    _bufferReceivedBytes = 0;

                    if (_packetValidator.ValidateData(_dataBuilder))
                    {
                        OnDataReceived(new SocketDataReceived() { Message = _dataBuilder.ToString() });
                        _dataBuilder.Clear();
                    }
                }
                catch (OperationCanceledException) {/* nothing to be afraid of :3 */ break; }
                catch (ObjectDisposedException) { break; }
            }
        }

        public override async Task Send(byte[] buffer)
            => await _clientStream.WriteAsync(buffer, 0, buffer.Length);

        public override void StopReceiving()
        {
            _operaionCancelationToken.Cancel(true);
            _client.Close();
            _client.Dispose();
        }
    }
}
