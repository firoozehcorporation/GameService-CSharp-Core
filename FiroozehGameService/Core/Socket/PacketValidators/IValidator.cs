using System.Text;

namespace FiroozehGameService.Core.Socket.PacketValidators
{
    internal interface IValidator
    {
        bool ValidateData(StringBuilder dataBuilder);

        bool ValidateBinaryData(byte[] buffer, int offset, int length);
    }
}
