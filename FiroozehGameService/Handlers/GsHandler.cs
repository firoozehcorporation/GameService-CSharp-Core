using System;
using System.IO;
using FiroozehGameService.Core;
using FiroozehGameService.Models.Command;
using System.Threading.Tasks;
using FiroozehGameService.Builder;
using FiroozehGameService.Handlers.Command;
using FiroozehGameService.Handlers.RealTime;
using FiroozehGameService.Handlers.RealTime.RequestHandlers;
using FiroozehGameService.Handlers.TurnBased;
using FiroozehGameService.Models.Enums.GSLive;

namespace FiroozehGameService.Handlers
{
    internal class GsHandler
    {
        #region GsHandlerRegion
        public CommandHandler CommandHandler { get; }
        public RealTimeHandler RealTimeHandler { get; private set; }
        public TurnBasedHandler TurnBasedHandler { get; private set; }
        #endregion
      
        internal GsHandler()
        {
            CommandHandler = new CommandHandler();
            CoreEventHandlers.GsLiveSystemStarted += OnJoinRoom;
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
                    await ConnectToRtServer(startPayload);
                    break;
                case GSLiveType.Core:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private async Task ConnectToRtServer(StartPayload payload)
        {
                if (RealTimeHandler != null && RealTimeHandler.IsAvailable)
                {
                    await RealTimeHandler.RequestAsync(LeaveRoomHandler.Signature);
                    RealTimeHandler.Dispose();
                    RealTimeHandler = null;
                }
                RealTimeHandler = new RealTimeHandler(payload);
                await RealTimeHandler.Init();            
        }

        private async Task ConnectToTbServer(StartPayload payload)
        {
            if (TurnBasedHandler != null && TurnBasedHandler.IsAvailable)
            {
                await TurnBasedHandler.RequestAsync(TurnBased.RequestHandlers.LeaveRoomHandler.Signature);
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
    }
}