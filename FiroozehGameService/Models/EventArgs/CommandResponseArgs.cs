using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiroozehGameService.Models.Command;

namespace FiroozehGameService.Models.EventArgs
{
    internal class CommandResponseArgs : System.EventArgs
    {
        public Packet CommandPacket { set; get; }
    }
}
