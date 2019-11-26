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
        public CommandHandler CommandHandler { get; private set; }
        private RTHandler realTimeHandler;
        private TBHandler turnBasedHandler;

        private StartPayload _turnBasedStartPayload;
        private StartPayload _realTimeStartPayload;

        private GameServiceClientConfiguration Configuration
            => GameService.Configuration;
        
        public GsHandler()
        { CommandHandler = new CommandHandler();}

        private async Task ConnectToRTServer(StartPayload payload)
        {
            if (realTimeHandler != null && realTimeHandler.IsAvailable)
            {
                await realTimeHandler.LeaveRoom();
                realTimeHandler.Dispose();
                realTimeHandler = null;
            }
            realTimeHandler = new RTHandler(payload.Area);
            await realTimeHandler.Init();
        }

        private async Task ConnectToTBServer(StartPayload payload)
        {
            if (turnBasedHandler != null && turnBasedHandler.IsAvailable)
            {
                await turnBasedHandler.LeaveRoom();
                turnBasedHandler.Dispose();
                turnBasedHandler = null;
            }
            turnBasedHandler = new TBHandler(payload.Area);
            await turnBasedHandler.Init();
        }

        public async Task Init()
        {
            await CommandHandler.Init();
        }
    }
}