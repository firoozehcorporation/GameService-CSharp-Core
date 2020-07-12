using System;
using System.Timers;

namespace FiroozehGameService.Utils
{
    internal class PingUtil
    {
        private const int Interval = 1000;
        private static long _lastPing = -1;

        internal static EventHandler RequestPing;

        private readonly Timer _timer;

        internal PingUtil()
        {
            if (_timer != null) return;
            _timer = new Timer
            {
                Interval = Interval,
                Enabled = false
            };
            _timer.Elapsed += (sender, args) => { RequestPing?.Invoke(this, null); };
            _timer.Start();
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

        internal void Dispose()
        {
            try
            {
                _timer?.Dispose();
                RequestPing = null;
                _lastPing = -1;
            }
            catch (Exception e)
            {
                LogUtil.LogError(null, "PingUtil Err : " + e);
            }
        }
    }
}