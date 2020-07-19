using System.Timers;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.Enums.GSLive;

namespace FiroozehGameService.Utils
{
    internal class HttpRequestObserver
    {
        
        private const int Reset = 1000;
        public const int MaxRequest = 3;
        private int _counter;
        private readonly Timer _timer;


        
        public HttpRequestObserver()
        {
            _timer = new Timer
            {
                Interval = Reset,
                Enabled = false
            };
            _timer.Elapsed += (sender, args) => {  _counter = 0; };
            _timer.Start();
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
        }
        
        
    }
}