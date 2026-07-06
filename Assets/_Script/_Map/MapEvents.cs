using _Script._Core._EventSystem;
using _Script._Map._MapLayer;

namespace _Script._Map
{
    public struct MapSettingEndEvent : IEvent
    {
        public IMap Map { get; private set; }
        
        public MapSettingEndEvent(IMap map)
        {
            Map = map;
        }
        
    }
}