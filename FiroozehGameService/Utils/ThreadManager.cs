using System.Threading;

namespace FiroozehGameService.Utils
{
    internal class ThreadManager
    {
        private static readonly int mainThreadId = Thread.CurrentThread.ManagedThreadId;
        internal static bool IsMainThread => Thread.CurrentThread.ManagedThreadId == mainThreadId;
    }
}