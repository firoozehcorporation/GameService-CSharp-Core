using System.Collections.Generic;
using System.Text;

namespace FiroozehGameService.Core.Socket.PacketValidators
{
    internal class JsonDataValidator : IValidator
    {
        private static readonly byte OpenedBracketByte = Encoding.ASCII.GetBytes("{")[0];
        private static readonly byte ClosedBracketByte = Encoding.ASCII.GetBytes("}")[0];

        public IEnumerable<string> ValidateDataAndReturn(string inputData)
        {
            const char openedBracket = '{';
            const char closedBracket = '}';
            
            var list = new List<string>();
            var buff = "";
            var current = 0;


            foreach (var data in inputData)
            {
                buff += data;
                switch (data)
                {
                    case openedBracket:
                        current++;
                        break;
                    case closedBracket:
                        current--;
                        if (current == 0)
                        {
                            list.Add(buff);
                            buff = "";
                        }
                        break;
                }
            }
            return list;         
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
