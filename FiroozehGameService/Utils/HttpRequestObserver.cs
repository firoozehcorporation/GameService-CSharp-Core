using System.Timers;

namespace FiroozehGameService.Utils
{
    internal class HttpRequestObserver
    {
        internal const int Reset = 3;
        internal const int MaxRequest = 15;
        private readonly Timer _timer;
        private int _counter;
        internal bool IsDisposed;


        public HttpRequestObserver()
        {
            _timer = new Timer
            {
                Interval = Reset * 1000,
                Enabled = false
            };
            _timer.Elapsed += (sender, args) => { _counter = 0; };
            _timer.Start();
            IsDisposed = false;
        }


        public bool Increase()
        {
            if (_counter > MaxRequest) return false;
            _counter++;
            return true;
        }


        public void Dispose()
        {
            _timer?.Stop();
            _timer?.Close();
            IsDisposed = true;
        }
    }
}