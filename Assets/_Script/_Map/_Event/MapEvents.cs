using _Script._Core._EventSystem;

namespace _Script._Map._Event
{
    public static class MapEvents
    {
        public static readonly MapLoadingEvent MapLoadingEvent = new();
    }

    public class MapLoadingEvent : IEvent
    {
        public float Value { get; private set; } = 0;

        public MapLoadingEvent Init(float value)
        {
            Value = value;
            return this;
        }
    }

    public struct MapLoadingEndEvent : IEvent
    {
        
    }
    
}