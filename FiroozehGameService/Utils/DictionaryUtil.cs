using System.Collections.Generic;

namespace FiroozehGameService.Utils
{
    internal static class DictionaryUtil
    {
        internal static TV GetValue<TK, TV>(this IDictionary<TK, TV> dict, TK key, TV defaultValue = default)
        {
            return dict.TryGetValue(key, out var value) ? value : defaultValue;
        }
    }
}