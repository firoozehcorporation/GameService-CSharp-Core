using System.Collections.Generic;
using System.Text;

namespace FiroozehGameService.Core.Socket.PacketHelper
{
    internal class JsonDataValidator : IValidator
    {
        private const char OpenedBracket = '{';
        private const char ClosedBracket = '}';

        public IEnumerable<string> ValidateDataAndReturn(StringBuilder inputData)
        {
            var scopeLevel = 0;
            var offset = 0;
            for (var i = 0; i < inputData.Length; i++)
                switch (inputData[i])
                {
                    case OpenedBracket:
                        scopeLevel++;
                        break;
                    case ClosedBracket:
                    {
                        scopeLevel--;
                        if (scopeLevel == 0)
                        {
                            yield return inputData.ToString(offset, i - offset + 1);
                            offset = i + 1;
                        }

                        break;
                    }
                }

            inputData.Remove(0, offset);
        }
    }
}