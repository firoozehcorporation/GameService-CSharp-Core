namespace FiroozehGameService.Utils
{
    internal class ThreadManager
    {
        private static int mainThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
        internal static bool IsMainThread => System.Threading.Thread.CurrentThread.ManagedThreadId == mainThreadId;
    }
}