using FiroozehGameService.Core;
using FiroozehGameService.Core.Socket;
using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.EventArgs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FiroozehGameService.Builder;
using FiroozehGameService.Handlers.Command;
using FiroozehGameService.Handlers.RealTime;
using FiroozehGameService.Handlers.RealTime.RequestHandlers;
using FiroozehGameService.Handlers.TurnBased;

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
        { CommandHandler = new CommandHandler();}

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
                await TurnBasedHandler.Request(LeaveRoomHandler.Signature);
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