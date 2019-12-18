using System;
using FiroozehGameService.Models.Command;

namespace FiroozehGameService.Handlers.RealTime.RequestHandlers
{
    internal abstract class BaseRequestHandler : IRequestHandler
    {
        public virtual Packet HandleAction(object payload)
        {
            if (CheckAction(payload))
                return DoAction(payload);
            throw new ArgumentException();
        }

        protected abstract bool CheckAction(object payload);

        protected abstract Packet DoAction(object payload);
    }
}
