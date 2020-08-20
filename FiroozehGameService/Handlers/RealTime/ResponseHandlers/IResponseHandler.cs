using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.GSLive.RT;

namespace FiroozehGameService.Handlers.RealTime.ResponseHandlers
{
    internal interface IResponseHandler
    {
        void HandlePacket(Packet packet, GProtocolSendType type);
    }
}