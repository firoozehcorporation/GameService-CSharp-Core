using System.Collections.Generic;
using System.Text;

namespace FiroozehGameService.Core.Socket.PacketHelper
{
    internal interface IValidator
    {
        IEnumerable<string> ValidateDataAndReturn(StringBuilder data); 
    }
}
