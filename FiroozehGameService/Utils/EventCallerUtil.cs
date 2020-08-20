using System;
using System.Timers;
using FiroozehGameService.Core;

namespace FiroozehGameService.Utils
{
    public class Event
    {
        private readonly long _interval;
        public EventHandler<Event> EventHandler;
        internal Timer Timer;

        internal Event(long interval)
        {
            _interval = interval;
        }

        public void Start()
        {
            Timer = new Timer
            {
                Interval = _interval,
                Enabled = false
            };

            Timer.Elapsed += (sender, args) =>
            {
                GameService.SynchronizationContext?.Send(
                    delegate { EventHandler?.Invoke(this, this); }, null);
            };
            Timer.Start();
        }

        public void Dispose()
        {
            Timer?.Stop();
            Timer?.Close();
        }
    }


    public static class EventCallerUtil
    {
        public static Event CreateNewEvent(long interval)
        {
            var newEvent = new Event(interval);
            return newEvent;
        }
    }
}