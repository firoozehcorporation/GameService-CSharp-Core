



using FiroozehGameService.Models.GSLive.Command;

namespace FiroozehGameService.Handlers.TurnBased
{
    internal interface IRequestHandler
    {
        Packet HandleAction(object payload);
    }
}
