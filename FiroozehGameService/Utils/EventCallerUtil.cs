using System;
using System.Collections.Generic;
using System.Timers;

namespace FiroozehGameService.Utils
{
    
    public class Event
    {
        internal Timer Timer;
        private readonly long _interval;
        public EventHandler<Event> EventHandler;

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
            
            Timer.Elapsed += (sender, args) => { EventHandler?.Invoke(this,this); };
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