using FiroozehGameService.Models.Command;

namespace FiroozehGameService.Handlers.RealTime
{
    internal interface IRequestHandler
    {
        Packet HandleAction(object payload);
    }
}
