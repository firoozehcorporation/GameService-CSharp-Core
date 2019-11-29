using FiroozehGameService.Models.GSLive.RT;

namespace FiroozehGameService.Handlers.RealTime.ResponseHandlers
{
    internal abstract class BaseResponseHandler : IResponseHandler
    {
        public virtual void HandlePacket(Packet packet)
        {
            HandleResponse(packet);
        }

        protected abstract void HandleResponse(Packet packet);
    }
}
