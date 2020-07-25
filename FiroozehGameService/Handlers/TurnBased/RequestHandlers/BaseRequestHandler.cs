using System;
using System.Text;
using FiroozehGameService.Models.GSLive.Command;

namespace FiroozehGameService.Handlers.TurnBased.RequestHandlers
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