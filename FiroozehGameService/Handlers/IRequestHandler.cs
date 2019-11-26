using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiroozehGameService.Handlers
{
    public interface IRequestHandler
    {
        void HandleAction(object payload);
    }
}
