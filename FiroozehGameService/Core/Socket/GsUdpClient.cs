using System;
using FiroozehGameService.Handlers;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.EventArgs;
using FiroozehGameService.Models.GSLive.Command;
using GProtocol;
using GProtocol.Core;
using Packet = FiroozehGameService.Models.GSLive.RT.Packet;

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
            CoreEventHandlers.GProtocolConnected?.Invoke(null,null);
        }

        private void ServerDisconnect(Connection conn, byte[] data)
        {
            IsAvailable = false;
            Pwd = null;
            OnClosed(new ErrorArg {Error = "ServerDisconnect"});
        }

        private void ServerTimeout(Connection conn, byte[] data)
        {
            IsAvailable = false;
            Pwd = null;
            OnClosed(new ErrorArg {Error = "ServerTimeout"});
        }

        private void HandleClientPacket(Connection conn, byte[] data, Connection.Channel channel)
        {
            OnDataReceived(new SocketDataReceived { Data = PacketDeserializer.Deserialize(data,Pwd)
                , Type =
                    channel == Connection.Channel.Reliable ? GProtocolSendType.Reliable : GProtocolSendType.UnReliable});
        }

        internal override void Init()
        {
            Client?.Connect(null);
        }


        internal override void Send(Packet packet,GProtocolSendType type)
        {
            var buffer = PacketSerializable.Serialize(packet, Pwd);
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

        internal override void UpdatePwd(string newPwd)
        {
            Pwd = newPwd;
        }

        internal override void StopReceiving()
        {
            Connection = null;
            Client = null;
            Pwd = null;
            IsAvailable = false;
            try
            {
                Connection?.Disconnect(null);
                Client?.Disconnect();
            }
            catch (Exception)
            {
                // ignored
            }
        }
       
    }
}
