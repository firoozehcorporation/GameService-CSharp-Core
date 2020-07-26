using System;
using System.Threading;
using System.Threading.Tasks;
using FiroozehGameService.Handlers;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.Enums.GSLive;
using FiroozehGameService.Models.EventArgs;
using FiroozehGameService.Models.GSLive.Command;
using FiroozehGameService.Utils;
using FiroozehGameService.Utils.Serializer;
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
                OperationCancellationToken = new CancellationTokenSource();
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
            Task.Run(() =>
            {
                var neededBuffer = new byte[payloadsize];
                Buffer.BlockCopy(payload,0,neededBuffer,0,payloadsize);
                
                var queueData = GsSerializer.Object.GetQueueData(neededBuffer);
                foreach (var data in queueData)
                {
                    OnDataReceived(new SocketDataReceived
                    {
                        Packet = PacketDeserializer.Deserialize(data, 0, data.Length),
                        Time = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
                    });
                }
            }, OperationCancellationToken.Token);
        }


        private void ClientOnOnStateChanged(ClientState state)
        {
            LogUtil.Log(this, "Client_OnStateChanged : " + state);

            switch (state)
            {
                case ClientState.Connected:
                    IsAvailable = true;
                    StartQueueWorker();
                    CoreEventHandlers.GProtocolConnected?.Invoke(null, null);
                    break;
                case ClientState.InvalidConnectToken:
                case ClientState.ConnectionTimedOut:
                case ClientState.ChallengeResponseTimedOut:
                case ClientState.ConnectionRequestTimedOut:
                case ClientState.ConnectionDenied:
                    IsAvailable = false;
                    Client?.Disconnect();
                    StopQueueWorker();
                    Client = null;

                    OnClosed(new ErrorArg {Error = state.ToString()});
                    break;
                case ClientState.ConnectTokenExpired: break;
                case ClientState.Disconnected: break;
            }
        }
        

        internal override void StopReceiving()
        {
            try
            {
                Client?.Disconnect();
                StartQueueWorker();
                OperationCancellationToken?.Cancel(false);
                OperationCancellationToken?.Dispose();
            }
            catch (Exception)
            {
                // ignored
            }

            Client = null;
            IsAvailable = false;
        }

        internal override void AddToQueue(Packet packet, GProtocolSendType type)
        {
            if (Client?.State == ClientState.Connected)
            {
                packet.SendType = type;
                if(packet.Action == RT.ActionPing) packet.ClientSendTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                var buffer = PacketSerializable.Serialize(packet);
                LogUtil.Log(this,"RealTime Send Payload Len : " + buffer.Length);
                SendQueue.Enqueue(buffer);
            }
            else
            {
                LogUtil.LogError(this, "Client not Connected!");
            }
        }

        internal override void StartQueueWorker()
        {
            QueueWorkerEvent = EventCallerUtil.CreateNewEvent(1000 / RT.RealTimeLimit);
            QueueWorkerEvent.EventHandler += EventHandler;
            QueueWorkerEvent.Start();
        }

        internal override void StopQueueWorker()
        {
            QueueWorkerEvent?.Dispose();
        }

        private void EventHandler(object sender, Event e)
        {
            Task.Run(() =>
            {
                var buffer = GsSerializer.Object.GetSendQueueBuffer(SendQueue);
                Client?.Send(buffer,buffer.Length);
            }, OperationCancellationToken.Token);
        }
    }
}