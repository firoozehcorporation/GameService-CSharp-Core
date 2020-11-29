using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FiroozehGameService.Core.Socket.ClientHelper;
using FiroozehGameService.Handlers;
using FiroozehGameService.Models.BasicApi;
using FiroozehGameService.Models.Enums.GSLive;
using FiroozehGameService.Models.EventArgs;
using FiroozehGameService.Models.GSLive.Command;
using FiroozehGameService.Utils;

namespace FiroozehGameService.Core.Socket
{
    internal class GsTcpClient : GsSocketClient
    {
        private const short CommandTimeOutWait = 1000;
        private const short TurnTimeOutWait = 500;


        private TcpClient _client;

        //private SslStream _clientStream;
        private NetworkStream _clientStream;
        

        public GsTcpClient(Area area = null)
        {
            Area = area;
            CoreEventHandlers.OnTcpClientConnected += OnTcpClientConnected;
        }

        private void OnTcpClientConnected(object sender, TcpClient client)
        {
            if(Type != (GSLiveType) sender) return;
            
            _client = client;
            _clientStream = _client.GetStream();
            OperationCancellationToken = new CancellationTokenSource();
            IsAvailable = true;
            LogUtil.Log(this, "GsTcpClient -> Init Done");
            CoreEventHandlers.OnGsTcpClientConnected?.Invoke(sender,client);
        }


        internal override async Task Init(CommandInfo info)
        {
            try
            {
                CommandInfo = info;
                Type = CommandInfo == null ? GSLiveType.TurnBased : GSLiveType.Command;

                var ip = CommandInfo == null ? Area.Ip : CommandInfo.Ip;
                var port = CommandInfo?.Port ?? Area.Port;
                var timeOutWait = CommandInfo == null ? TurnTimeOutWait : CommandTimeOutWait;

                LogUtil.Log(this, "GsTcpClient -> Init Started with -> " + CommandInfo + " or " + Area);
                await new TcpClientWithTimeout(ip, port,timeOutWait).Connect(Type);
            }
            catch (Exception e)
            {
                LogUtil.LogError(this, e.ToString());
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

                    DataBuilder.Append(Encoding.UTF8.GetString(Buffer, BufferOffset, BufferReceivedBytes));
                    var packets = PacketValidator.ValidateDataAndReturn(DataBuilder);
                    foreach (var packet in packets)
                        OnDataReceived(new SocketDataReceived
                        {
                            Packet = PacketDeserializer.Deserialize(packet)
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
                OperationCancellationToken?.Cancel(false);
                OperationCancellationToken?.Dispose();

                _client?.GetStream().Close();
                _client?.Close();
                _client = null;
                
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