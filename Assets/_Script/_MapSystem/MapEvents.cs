using _Script._Core._EventSystem;
using _Script._MapSystem._Map;

namespace _Script._MapSystem
{
    public static class MapEvents
    {
        
    }
    
    public struct MapGenerateEvent : IEvent
    {
        public readonly MapGrid MapGrid;
        public int Seed { get; private set; }
        public MapGenerateEvent(MapGrid mapGrid,int seed)
        {
            MapGrid = mapGrid;
            Seed = seed;
        }
    }
    
    public struct MapGenerateEndEvent : IEvent
    {
        
    }
}