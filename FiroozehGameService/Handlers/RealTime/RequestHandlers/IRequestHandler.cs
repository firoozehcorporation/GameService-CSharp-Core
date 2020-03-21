
using FiroozehGameService.Models.GSLive.RT;

namespace FiroozehGameService.Handlers.RealTime.RequestHandlers
{
    internal interface IRequestHandler
    {
        Packet HandleAction(object payload);
    }
}
