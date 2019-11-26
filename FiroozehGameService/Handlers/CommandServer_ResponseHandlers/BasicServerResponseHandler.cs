using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiroozehGameService.Handlers.CommandServer_ResponseHandlers
{
    internal class ResponseHandler<T> where T : EventArgs
    {
        public string Signature { set; get; } //redunant ... either use this or the next one
        public int ActionCommand { set; get; }

        public EventHandler<T> EventHandler { set; get; }
    }
}
