using FiroozehGameService.Models.Command;
using System;
using System.Text;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.EventArgs;
using GProtocol;
using GProtocol.Core;

namespace FiroozehGameService.Core.Socket
{
    internal class GsUdpClient : GProtocolClient
    {
        public bool IsAvailable { get; private set; }

        public GsUdpClient(Area endpoint)
        {
            if (endpoint.Protocol.ToUpper() != "UDP")
                throw new InvalidOperationException("Only UDP Protocol Supported");

            Client = new Client(endpoint.Ip + ':' + endpoint.Port)
            {
                ServerConnect = ServerConnect,
                ServerDisconnect = ServerDisconnect,
                ServerTimeout = ServerTimeout,
                PacketHandler = HandleClientPacket
            };
        }

        private void ServerConnect(Connection conn, byte[] data)
        {
            Connection = conn;
            IsAvailable = true;
        }

        private void ServerDisconnect(Connection conn, byte[] data)
        {
            OnClosed(new ErrorArg {Error = "ServerDisconnect"});
        }

        private void ServerTimeout(Connection conn, byte[] data)
        {
            OnClosed(new ErrorArg {Error = "ServerTimeout"});
        }

        private void HandleClientPacket(Connection conn, byte[] data, Connection.Channel channel)
        {
            OnDataReceived(new SocketDataReceived { Data = Encoding.UTF8.GetString(data) 
                , Type =
                    channel == Connection.Channel.Reliable ? GProtocolSendType.Reliable : GProtocolSendType.UnReliable});
        }

        internal override void Init()
        {
            Client?.Connect(null);
        }


        internal override void Send(byte[] buffer,GProtocolSendType type)
        {
            switch (type)
            {
                case GProtocolSendType.Reliable:
                    Connection?.SendReliable(buffer);
                    break;
                case GProtocolSendType.UnReliable:
                    Connection?.SendUnreliable(buffer);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
        
        internal override void StopReceiving()
        {
            try
            {
                Connection?.Disconnect(null);
                Client?.Disconnect();
                Connection = null;
                Client = null;
                IsAvailable = false;
            }
            catch (Exception)
            {
                // ignored
            }
        }
       
    }
}
