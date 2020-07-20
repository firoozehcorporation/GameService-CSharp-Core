using System;
using System.Collections.Generic;
using System.Timers;
using FiroozehGameService.Models;

namespace FiroozehGameService.Utils
{
    
    public class Event
    {
        internal Timer Timer;
        internal short Id;
        internal long Interval;
        public EventHandler<Event> EventHandler;

        internal Event(short id,long interval)
        {
            Id = id;
            Interval = interval;
        }

        internal void Start()
        {
            Timer = new Timer
            {
                Interval = Interval,
                Enabled = false
            };
            
            Timer.Elapsed += (sender, args) => { EventHandler?.Invoke(this,this); };
            Timer.Start();
        }

        internal void Dispose()
        {
            Timer?.Stop();
            Timer?.Close();
        }
        
    }
    
    
    public static class EventCallerUtil
    {
        private static Dictionary<short,Event> _events;
        public static EventHandler<Event> OnNewEvent;
        
        public static Event AddEvent(short id,long interval)
        {
            if(_events != null && _events.ContainsKey(id))
                throw new GameServiceException("id Must be unique");
            
            var newEvent = new Event(id, interval);
            if (_events == null) _events = new Dictionary<short, Event>();
            
            newEvent.EventHandler += delegate(object sender, Event newE)
            {
                OnNewEvent?.Invoke(sender,newE);
            };
            
            _events.Add(id,newEvent);
            return newEvent;
        }

        public static void RemoveEvent(short id)
        {
            if (_events.TryGetValue(id, out var eventObj))
                eventObj?.Dispose();
        }
        
    }
}