using System;
using System.Threading.Tasks;
using FiroozehGameService.Handlers.Command;
using FiroozehGameService.Handlers.RealTime;
using FiroozehGameService.Handlers.RealTime.RequestHandlers;
using FiroozehGameService.Handlers.TurnBased;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.Enums.GSLive;
using FiroozehGameService.Models.GSLive.Command;

namespace FiroozehGameService.Handlers
{
    internal class GsHandler
    {
        internal GsHandler()
        {
            CommandHandler = new CommandHandler();
            CoreEventHandlers.GsLiveSystemStarted += OnJoinRoom;
            CoreEventHandlers.Dispose += OnDispose;
        }


        private void OnDispose(object sender, EventArgs e)
        {
            if (sender.GetType() == typeof(RealTimeHandler))
                RealTimeHandler = null;
            else if (sender.GetType() == typeof(TurnBasedHandler))
                TurnBasedHandler = null;
        }

        private async void OnJoinRoom(object sender, StartPayload startPayload)
        {
            switch (startPayload.Room.GsLiveType)
            {
                case GSLiveType.NotSet:
                    break;
                case GSLiveType.TurnBased:
                    await ConnectToTbServer(startPayload);
                    break;
                case GSLiveType.RealTime:
                    ConnectToRtServer(startPayload);
                    break;
                case GSLiveType.Command:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ConnectToRtServer(StartPayload payload)
        {
            if (RealTimeHandler != null && RealTimeHandler.IsAvailable)
            {
                RealTimeHandler.Request(LeaveRoomHandler.Signature, GProtocolSendType.Reliable, isCritical: true);
                RealTimeHandler.Dispose();
                RealTimeHandler = null;
            }

            RealTimeHandler = new RealTimeHandler(payload);
            RealTimeHandler.Init();
        }

        private async Task ConnectToTbServer(StartPayload payload)
        {
            if (TurnBasedHandler != null && TurnBasedHandler.IsAvailable)
            {
                await TurnBasedHandler.RequestAsync(TurnBased.RequestHandlers.LeaveRoomHandler.Signature, isCritical: true);
                TurnBasedHandler.Dispose();
                TurnBasedHandler = null;
            }

            TurnBasedHandler = new TurnBasedHandler(payload);
            await TurnBasedHandler.Init();
        }

        internal async Task Init()
        {
            await CommandHandler.Init();
        }

        internal void Dispose()
        {
            CommandHandler?.Dispose();
            RealTimeHandler?.Dispose();
            TurnBasedHandler?.Dispose();
        }

        #region GsHandlerRegion

        public CommandHandler CommandHandler { get; }
        public RealTimeHandler RealTimeHandler { get; private set; }
        public TurnBasedHandler TurnBasedHandler { get; private set; }

        #endregion
    }
}