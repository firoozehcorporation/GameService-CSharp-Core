

using FiroozehGameService.Models.Command;

namespace FiroozehGameService.Handlers.TurnBased.ResponseHandlers
{
    internal interface IResponseHandler
    {
        void HandlePacket(Packet packet);
    }
}
