using System;
using System.Text;
using FiroozehGameService.Models.GSLive.RT;

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

        protected static byte[] GetBuffer(string data)
        {
            return Encoding.UTF8.GetBytes(data);
        }
    }
}