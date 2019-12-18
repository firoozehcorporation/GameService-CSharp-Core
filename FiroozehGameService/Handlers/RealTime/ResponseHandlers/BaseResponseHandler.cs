using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.Enums;

namespace FiroozehGameService.Handlers.RealTime.ResponseHandlers
{
    internal abstract class BaseResponseHandler : IResponseHandler
    {
        public virtual void HandlePacket(Packet packet,GProtocolSendType type)
        {
            HandleResponse(packet,type);
        }

        protected abstract void HandleResponse(Packet packet,GProtocolSendType type);
    }
}
