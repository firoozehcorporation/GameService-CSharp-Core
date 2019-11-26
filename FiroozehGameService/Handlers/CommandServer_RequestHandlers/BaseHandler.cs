using System;
using FiroozehGameService.Models.Command;

namespace FiroozehGameService.Handlers
{
    internal abstract class BaseHandler<T> : IRequestHandler where T : class
    {
        public static string Signature 
            => throw new NotImplementedException();
        protected CommandHandler _commandHander;

        public virtual void HandleAction(object payload)
        {
            if (CheckAction(payload))
                DoAction(payload);
            else
                throw new ArgumentException();
        }

        protected abstract bool CheckAction(object payload);

        protected abstract Packet DoAction(object payload);
    }
}
