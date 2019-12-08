using System.Collections.Generic;
using System.Text;

namespace FiroozehGameService.Core.Socket.PacketValidators
{
    internal class JsonDataValidator : IValidator
    {
        private static readonly byte OpenedBracketByte = Encoding.ASCII.GetBytes("{")[0];
        private static readonly byte ClosedBracketByte = Encoding.ASCII.GetBytes("}")[0];
        private const char OpenedBracket = '{';
        private const char ClosedBracket = '}';

        public IEnumerable<string> ValidateDataAndReturn(StringBuilder inputData)
        {
            var scopeLevel = 0;
            int offset = 0;
            for (int i = 0; i < inputData.Length; i++)
            {
                if (inputData[i] == OpenedBracket)
                    scopeLevel++;
                else if (inputData[i] == ClosedBracket)
                {
                    scopeLevel--;
                    if (scopeLevel == 0)
                    {
                        yield return inputData.ToString(offset, i - offset + 1);
                        offset = i + 1;
                    }
                }
            }
            inputData.Remove(0, offset);
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
