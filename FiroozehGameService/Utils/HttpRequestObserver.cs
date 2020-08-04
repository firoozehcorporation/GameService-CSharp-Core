using System.Timers;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.Enums.GSLive;

namespace FiroozehGameService.Utils
{
    internal class HttpRequestObserver
    {
        
        internal const int Reset = 3;
        internal const int MaxRequest = 9;
        private int _counter;
        internal bool IsDisposed;
        private readonly Timer _timer;


        
        public HttpRequestObserver()
        {
            _timer = new Timer
            {
                Interval = Reset * 1000,
                Enabled = false
            };
            _timer.Elapsed += (sender, args) => {  _counter = 0; };
            _timer.Start();
            IsDisposed = false;
        }
        
        
        public bool Increase () {
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