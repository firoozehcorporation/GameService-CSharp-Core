using System.Timers;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.Enums.GSLive;

namespace FiroozehGameService.Utils
{
    internal class GsLiveSystemObserver
    {
        private readonly Timer _timer;
        private readonly GSLiveType _type;
        private int _counter;

        public GsLiveSystemObserver(GSLiveType type)
        {
            _type = type;
            _timer = new Timer
            {
                Interval = RT.RestLimit,
                Enabled = false
            };
            _timer.Elapsed += (sender, args) => { Reset(); };
            _timer.Start();
        }

        private void Reset()
        {
            _counter = 0;
        }

        public bool Increase(bool isCritical)
        {
            if (isCritical) return true;

            switch (_type)
            {
                case GSLiveType.NotSet:
                    break;
                case GSLiveType.TurnBased:
                    if (_counter <= TB.TurnBasedLimit)
                    {
                        _counter++;
                        return true;
                    }

                    break;
                case GSLiveType.RealTime:
                    if (_counter <= RT.RealTimeLimit)
                    {
                        _counter++;
                        return true;
                    }

                    break;
                case GSLiveType.Command:
                    if (_counter <= Command.TimeLimit)
                    {
                        _counter++;
                        return true;
                    }

                    break;
                default:
                    return false;
            }

            return false;
        }


        public int GetMaxRequestSupport()
        {
            switch (_type)
            {
                case GSLiveType.TurnBased:
                    return TB.TurnBasedLimit;
                case GSLiveType.RealTime:
                    return RT.RealTimeLimit;
                case GSLiveType.Command:
                    return TB.TurnBasedLimit;
                case GSLiveType.NotSet:
                    return -1;
                default:
                    return -1;
            }
        }

        public void Dispose()
        {
            _timer?.Stop();
            _timer?.Close();
        }
    }
}