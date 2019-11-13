using System.Text;

namespace FiroozehGameService.Core.Socket.PacketValidators
{
    internal class JsonDataValidator : IValidator
    {
        private static readonly byte OpenedBracketByte = Encoding.ASCII.GetBytes("{")[0];
        private static readonly byte ClosedBracketByte = Encoding.ASCII.GetBytes("}")[0];

        public bool ValidateData(StringBuilder dataBuilder)
        {
            const char openedBracket = '{';
            const char closedBracket = '}';
            var scopeLevel = 0;

            for (var i = 0; i < dataBuilder.Length; i++)
            {
                switch (dataBuilder[i])
                {
                    case openedBracket:
                        scopeLevel++;
                        break;
                    case closedBracket:
                        scopeLevel--;
                        break;
                    default:
                        break;
                }
            }

            return scopeLevel == 0;
        }

        public bool ValidateBinaryData(byte[] buffer, int offset, int length)
        {
            var continuous = false;
            var scopeLevel = 0;

            for (var i = offset; i < length && i < buffer.Length; i++)
            {
                if (i > 0)
                    continuous = buffer[i - 1] > 128; //highest bit (128)

                if (!continuous && buffer[i] == OpenedBracketByte)
                    scopeLevel++;
                else if (!continuous && buffer[i] == ClosedBracketByte)
                    scopeLevel--;
            }
            return scopeLevel == 0;
        }
    }
}
