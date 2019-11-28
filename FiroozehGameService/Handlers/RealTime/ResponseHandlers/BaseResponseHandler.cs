using System;
using FiroozehGameService.Models.GSLive.RT;

namespace FiroozehGameService.Handlers.RealTime.ResponseHandlers
{
    internal abstract class BaseResponseHandler : IResponseHandler
    {
        private static int ActionCommand
            => throw new NotImplementedException();

        public virtual void HandlePacket(Packet packet)
        {
            if (packet.Action != ActionCommand) throw new InvalidOperationException();
            HandleResponse(packet);
        }

        protected abstract void HandleResponse(Packet packet);
    }
}
