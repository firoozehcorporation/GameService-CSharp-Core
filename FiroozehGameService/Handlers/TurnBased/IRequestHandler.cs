



using FiroozehGameService.Models.Command;

namespace FiroozehGameService.Handlers.TurnBased
{
    internal interface IRequestHandler
    {
        Packet HandleAction(object payload);
    }
}
