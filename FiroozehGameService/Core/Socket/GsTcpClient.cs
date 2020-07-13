using System;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FiroozehGameService.Core.Socket.ClientHelper;
using FiroozehGameService.Models.BasicApi;
using FiroozehGameService.Models.EventArgs;
using FiroozehGameService.Models.GSLive.Command;
using FiroozehGameService.Utils;

namespace FiroozehGameService.Core.Socket
{
    internal class GsTcpClient : GsSocketClient
    {
        private const short TimeOut = 5000;
        private TcpClient _client;
        //private SslStream _clientStream;
        private NetworkStream _clientStream;

        public GsTcpClient(Area area = null)
        {
            Area = area;
        }


        internal override bool Init(CommandInfo info)
        {
            try
            {
                CommandInfo = info;
                var ip = CommandInfo == null ? Area.Ip : CommandInfo.Ip;
                var cert = CommandInfo == null ? Area.Cert : CommandInfo.Cert;
                var port = CommandInfo?.Port ?? Area.Port;

                LogUtil.Log(this, "GsTcpClient -> Init Started with -> " + CommandInfo + " or " + Area);
                _client = new TcpClientWithTimeout(ip, port, TimeOut).Connect();
                LogUtil.Log(this,"GsTcpClient -> Connected,Waiting for Handshakes...");
               
                /*var certificate = new X509Certificate2(Encoding.Default.GetBytes(cert));
                X509Certificate[] x509Certificates = {certificate};
                var certsCollection = new X509CertificateCollection(x509Certificates);

                _clientStream = new SslStream(_client.GetStream(), false, ValidateServerCertificate, null);
                try
                {
                    _clientStream.AuthenticateAsClient(ip,certsCollection,SslProtocols.Tls, false);
                }
                catch (AuthenticationException e)
                {
                    LogUtil.LogError(this,"Exception: " + e.Message);
                    if (e.InnerException != null)
                        LogUtil.LogError(this,"Inner exception: " + e.InnerException.Message);
                    LogUtil.LogError(this,"Authentication failed - closing the connection.");
                    _client.Close();
                    return false;
                }
                */
                
                _clientStream = _client.GetStream();
                OperationCancellationToken = new CancellationTokenSource();
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

        private static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;

            LogUtil.LogError(null,"Certificate error: " + sslPolicyErrors);
            return false;
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