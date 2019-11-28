using FiroozehGameService.Core;
using FiroozehGameService.Core.Socket;
using FiroozehGameService.Handlers.CommandServer_ResponseHandlers;
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

namespace FiroozehGameService.Handlers
{
    internal class GsHandler
    {
        public CommandHandler CommandHandler { get; }
        public RTHandler RealTimeHandler { get; private set; }
        public TBHandler TurnBasedHandler { get; private set; }

        private GameServiceClientConfiguration Configuration
            => GameService.Configuration;
        
        public GsHandler()
        { CommandHandler = new CommandHandler();}

        private async Task ConnectToRTServer(StartPayload payload)
        {
            if (RealTimeHandler != null && RealTimeHandler.IsAvailable)
            {
                await RealTimeHandler.LeaveRoom();
                RealTimeHandler.Dispose();
                RealTimeHandler = null;
            }
            RealTimeHandler = new RTHandler(payload.Area);
            await RealTimeHandler.Init();
        }

        private async Task ConnectToTBServer(StartPayload payload)
        {
            if (TurnBasedHandler != null && TurnBasedHandler.IsAvailable)
            {
                await TurnBasedHandler.LeaveRoom();
                TurnBasedHandler.Dispose();
                TurnBasedHandler = null;
            }
            TurnBasedHandler = new TBHandler(payload.Area);
            await TurnBasedHandler.Init();
        }

        public async Task Init()
        {
            await CommandHandler.Init();
        }
    }
}