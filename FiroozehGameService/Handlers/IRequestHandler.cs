
using FiroozehGameService.Models.Command;

namespace FiroozehGameService.Handlers
{
    internal interface IRequestHandler
    {
        Packet HandleAction(object payload);
    }
}
