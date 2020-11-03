using System;
using System.Timers;

namespace FiroozehGameService.Utils
{
    internal static class PingUtil
    {
        
        private const int Interval = 1000;
        private static long _lastPing = -1;
        internal static EventHandler RequestPing;
        private static Timer _timer;
        

        internal static void Init()
        {
            if (_timer != null) return;
            _timer = new Timer
            {
                Interval = Interval,
                Enabled = true
            };
            _timer.Elapsed += (sender, args) => { RequestPing?.Invoke(null, null); };
        }

        internal static long GetLastPing()
        {
            return _lastPing;
        }

        internal static void SetLastPing(long ping)
        {
            _lastPing = ping;
        }

        internal static long Diff(long one, long two)
        {
            return Math.Abs(one - two);
        }
    }
}