using System;
using FiroozehGameService.Models.Command;

namespace FiroozehGameService.Handlers.CommandServer_RequestHandlers
{
    internal abstract class BaseHandler : IRequestHandler
    {
        protected CommandHandler CommandHandler;

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
