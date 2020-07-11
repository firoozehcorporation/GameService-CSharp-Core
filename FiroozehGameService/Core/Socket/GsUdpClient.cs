using System;
using FiroozehGameService.Handlers;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.Enums.GSLive;
using FiroozehGameService.Models.EventArgs;
using FiroozehGameService.Models.GSLive.Command;
using FiroozehGameService.Utils;
using GProtocol.Public;
using Packet = FiroozehGameService.Models.GSLive.RT.Packet;

namespace FiroozehGameService.Core.Socket
{
    internal class GsUdpClient : GProtocolClient
    {
        public GsUdpClient(Area endpoint)
        {
            Area = endpoint;
            CreateInstance();
        }

        public bool IsAvailable { get; private set; }


        internal override void Init()
        {
            try
            {
                if (Client == null) CreateInstance();
                Client?.Connect(Convert.FromBase64String(Area.ConnectToken));
                LogUtil.Log(this, "GsUdpClient Init");
            }
            catch (Exception e)
            {
                LogUtil.Log(this, "GsUdpClient Err : " + e);
            }
        }

        internal sealed override void CreateInstance()
        {
            if (Area?.ConnectToken != null)
            {
                Client = new Client();

                Client.OnStateChanged += ClientOnOnStateChanged;
                Client.OnMessageReceived += ClientOnOnMessageReceived;

                LogUtil.Log(this, "GsUdpClient Created");
            }
            else
            {
                LogUtil.LogError(this, "Token Is NULL");
            }
        }


        private void ClientOnOnMessageReceived(byte[] payload, int payloadsize)
        {
            OnDataReceived(new SocketDataReceived
            {
                Packet = PacketDeserializer.Deserialize(payload, 0, payloadsize, GSLiveType.RealTime),
                Time = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
            });
        }


        private void ClientOnOnStateChanged(ClientState state)
        {
            LogUtil.Log(this, "Client_OnStateChanged : " + state);

            switch (state)
            {
                case ClientState.Connected:
                    IsAvailable = true;
                    CoreEventHandlers.GProtocolConnected?.Invoke(null, null);
                    break;
                case ClientState.InvalidConnectToken:
                case ClientState.ConnectionTimedOut:
                case ClientState.ChallengeResponseTimedOut:
                case ClientState.ConnectionRequestTimedOut:
                case ClientState.ConnectionDenied:
                    IsAvailable = false;
                    Client?.Disconnect();
                    Client = null;

                    OnClosed(new ErrorArg {Error = state.ToString()});
                    break;
                case ClientState.ConnectTokenExpired: break;
                case ClientState.Disconnected: break;
            }
        }


        internal override void Send(Packet packet, GProtocolSendType type)
        {
            if (Client?.State == ClientState.Connected)
            {
                packet.SendType = type;
                packet.ClientSendTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                var buffer = PacketSerializable.Serialize(packet);
                switch (type)
                {
                    case GProtocolSendType.Reliable:
                        Client?.Send(buffer, buffer.Length);
                        break;
                    case GProtocolSendType.UnReliable:
                        Client?.Send(buffer, buffer.Length);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                }
            }
            else
            {
                LogUtil.LogError(this, "Client not Connected!");
            }
        }

        internal override void StopReceiving()
        {
            try
            {
                Client?.Disconnect();
            }
            catch (Exception)
            {
                // ignored
            }

            Client = null;
            IsAvailable = false;
        }
    }
}