
using FiroozehGameService.Models.GSLive.Command;

namespace FiroozehGameService.Handlers.Command.RequestHandlers
{
    internal interface IRequestHandler
    {
        Packet HandleAction(object payload);
    }
}
