using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiroozehGameService.Core.Socket.PacketValidators
{
    interface IValidator
    {
        bool ValidateData(StringBuilder dataBuffer);

        bool ValidateBinaryData(byte[] buffer, int offset, int length);
    }
}
