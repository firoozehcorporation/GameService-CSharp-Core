using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using FiroozehGameService.Core.Socket.ClientHelper;
using FiroozehGameService.Models.Enums.GSLive;
using FiroozehGameService.Models.EventArgs;
using FiroozehGameService.Models.GSLive.Command;
using FiroozehGameService.Utils;

namespace FiroozehGameService.Core.Socket
{
    internal class GsTcpClient : GsSocketClient
    {
        private const short TimeOut = 5000;
        private TcpClient _client;
        private NetworkStream _clientStream;

        public GsTcpClient(Area area)
        {
            if (area.Protocol.ToUpper() != "TCP")
                throw new InvalidOperationException("Only TCP Protocol Supported");

            Endpoint = area;
        }


        internal override bool Init()
        {
            try
            {
                LogUtil.Log(this, "GsTcpClient -> Init Started");
                _client = new TcpClientWithTimeout(Endpoint.Ip, Endpoint.Port, TimeOut).Connect();
                OperationCancellationToken = new CancellationTokenSource();
                _clientStream = _client.GetStream();
                IsAvailable = true;
                LogUtil.Log(this, "GsTcpClient -> Init Done");
                return true;
            }
            catch (Exception e)
            {
                LogUtil.LogError(this, e.ToString());
                return false;
            }
        }


        internal override async Task StartReceiving()
        {
            LogUtil.Log(this, "GsTcpClient -> StartReceiving");
            while (IsAvailable)
                try
                {
                    BufferReceivedBytes += await _clientStream.ReadAsync(
                        Buffer,
                        BufferOffset,
                        Buffer.Length - BufferOffset,
                        OperationCancellationToken.Token);

                    /*DataBuilder.Append(Encoding.UTF8.GetString(Buffer, BufferOffset, BufferReceivedBytes));
                    var packets = PacketValidator.ValidateDataAndReturn(DataBuilder);
                    foreach (var packet in packets)
                        OnDataReceived(new SocketDataReceived
                        {
                            Packet = PacketDeserializer.Deserialize()
                        });
                    */

                    OnDataReceived(new SocketDataReceived
                    {
                        Packet = PacketDeserializer.Deserialize(Buffer, BufferOffset, BufferReceivedBytes,
                            GSLiveType.Core)
                    });
                    BufferReceivedBytes = 0;
                }
                catch (Exception e)
                {
                    LogUtil.LogError(this, "StartReceiving : " + e);
                    if (!(e is OperationCanceledException || e is ObjectDisposedException ||
                          e is ArgumentOutOfRangeException))
                        OnClosed(new ErrorArg {Error = e.ToString()});
                    break;
                }
        }


        internal override void Send(Packet packet)
        {
            Task.Run(() =>
            {
                var buffer = PacketSerializer.Serialize(packet);
                _clientStream?.Write(buffer, 0, buffer.Length);
            }, OperationCancellationToken.Token);
        }


        internal override async Task SendAsync(Packet packet)
        {
            try
            {
                var buffer = PacketSerializer.Serialize(packet);
                if (_clientStream != null) await _clientStream.WriteAsync(buffer, 0, buffer.Length);
            }
            catch (Exception e)
            {
                LogUtil.LogError(this, "Send -> " + e);
                OnClosed(new ErrorArg {Error = e.ToString()});
            }
        }


        internal override void StopReceiving()
        {
            try
            {
                DataBuilder?.Clear();
                IsAvailable = false;

                _client?.GetStream().Close();
                _client?.Close();
                _client = null;

                OperationCancellationToken?.Cancel(false);
                OperationCancellationToken?.Dispose();
                OperationCancellationToken = null;
                LogUtil.Log(this, "GsTcpClient -> StopReceiving");
            }
            catch (Exception e)
            {
                LogUtil.LogError(this, e.ToString());
            }
        }
    }
}