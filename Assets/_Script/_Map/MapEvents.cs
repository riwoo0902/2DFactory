using System;
using _Script._Core._EventSystem;

namespace _Script._Map
{
    public static class MapEvents
    {
        public static readonly MapLoadingEvent MapLoadingEvent = new MapLoadingEvent();
    }

    public class MapLoadingEvent : IEvent
    {
        public float Value { get; private set; } = 0;
        public bool Completed { get; private set; } = false;

        public void Init(float value, bool completed)
        {
            Value = value;
            Completed = completed;
        }
        
    }
}