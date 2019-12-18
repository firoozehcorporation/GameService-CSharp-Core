
using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.Enums;

namespace FiroozehGameService.Handlers.RealTime.ResponseHandlers
{
    internal interface IResponseHandler
    {
        void HandlePacket(Packet packet,GProtocolSendType type);
    }
}
