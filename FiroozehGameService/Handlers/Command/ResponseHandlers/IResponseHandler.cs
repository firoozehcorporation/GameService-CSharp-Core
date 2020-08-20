using FiroozehGameService.Models.GSLive.Command;

namespace FiroozehGameService.Handlers.Command.ResponseHandlers
{
    internal interface IResponseHandler
    {
        void HandlePacket(Packet packet);
    }
}