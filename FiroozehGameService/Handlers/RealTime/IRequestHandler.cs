

using FiroozehGameService.Models.GSLive.RT;

namespace FiroozehGameService.Handlers.RealTime
{
    internal interface IRequestHandler
    {
        Packet HandleAction(object payload);
    }
}
