using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiroozehGameService.Models.EventArgs
{
    class SocketDataReceived : System.EventArgs
    {
        public string Message { set; get; }
    }
}
