using System;
using FiroozehGameService.Models.Command;

namespace FiroozehGameService.Handlers.Command.ResponseHandlers
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
