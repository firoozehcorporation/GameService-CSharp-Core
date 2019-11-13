using System.Text;

namespace FiroozehGameService.Core.Socket.PacketValidators
{
    internal class JsonDataValidator : IValidator
    {
        private static byte OpenedBracketByte = Encoding.ASCII.GetBytes("{")[0];
        private static byte ClosedBracketByte = Encoding.ASCII.GetBytes("}")[0];

        public bool ValidateData(StringBuilder _dataBuiler)
        {
            char openedBracket = '{';
            char closedBracket = '}';
            int scopeLevel = 0;

            for (int i = 0; i < _dataBuiler.Length; i++)
            {
                if (_dataBuiler[i] == openedBracket)
                    scopeLevel++;
                else if (_dataBuiler[i] == closedBracket)
                    scopeLevel--;
            }

            return scopeLevel == 0;
        }

        public bool ValidateBinaryData(byte[] buffer, int offset, int length)
        {
            bool continuouse = false;
            int scopeLevel = 0;

            for (int i = offset; i < length && i < buffer.Length; i++)
            {
                if (i > 0)
                    continuouse = buffer[i - 1] > 128; //highest bit (128)

                if (!continuouse && buffer[i] == OpenedBracketByte)
                    scopeLevel++;
                else if (!continuouse && buffer[i] == ClosedBracketByte)
                    scopeLevel--;
            }
            return scopeLevel == 0;
        }
    }
}
