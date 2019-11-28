
using FiroozehGameService.Models.Command;

namespace FiroozehGameService.Handlers.Command
{
    internal interface IRequestHandler
    {
        Packet HandleAction(object payload);
    }
}
