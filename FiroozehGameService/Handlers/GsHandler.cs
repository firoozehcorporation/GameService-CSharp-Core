using System.Threading.Tasks;
using FiroozehGameService.Builder;
using FiroozehGameService.Core;
using FiroozehGameService.Core.GSLive;
using FiroozehGameService.Models;

namespace FiroozehGameService.Handlers
{
    public class GsHandler
    {
        private readonly CommandHandler _commandHandler;
       

        private GameServiceClientConfiguration Configuration
            => GameService.Configuration;

       
        public GsHandler()
        {
            _commandHandler = new CommandHandler();
        }

        public async Task<bool> Init()
        {
          return await _commandHandler.Init();
        }

      
    }
}