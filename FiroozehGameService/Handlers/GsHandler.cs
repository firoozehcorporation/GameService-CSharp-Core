using System;
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
        public CommandHandler CommandHandler { get; }
        public RealTimeHandler RealTimeHandler { get; private set; }
        public TurnBasedHandler TurnBasedHandler { get; private set; }

        private GameServiceClientConfiguration Configuration
            => GameService.Configuration;

        public GsHandler()
        {
            CommandHandler = new CommandHandler();
            CoreEventHandlers.OnJoinRoom += OnJoinRoom;
        }

        private async void OnJoinRoom(object sender, StartPayload startPayload)
        {
            switch (startPayload.Room?.RoomType)
            {
                case RoomType.NotSet:
                    break;
                case RoomType.TurnBased:
                    await ConnectToTbServer(startPayload);
                    break;
                case RoomType.RealTime:
                    await ConnectToRtServer(startPayload);
                    break;
                case null:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private async Task ConnectToRtServer(StartPayload payload)
        {
            if (RealTimeHandler != null && RealTimeHandler.IsAvailable)
            {
                await RealTimeHandler.Request(LeaveRoomHandler.Signature);
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
                await TurnBasedHandler.Request(TurnBased.RequestHandlers.LeaveRoomHandler.Signature);
                TurnBasedHandler.Dispose();
                TurnBasedHandler = null;
            }
            TurnBasedHandler = new TurnBasedHandler(payload);
            await TurnBasedHandler.Init();
        }

        public async Task Init()
        {
            await CommandHandler.Init();
        }
    }
}