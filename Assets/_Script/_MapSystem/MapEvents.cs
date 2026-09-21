using _Script._Core._EventSystem;
using _Script._MapSystem._Map;
using _Script._MapSystem._MapGenerator;

namespace _Script._MapSystem
{
    public readonly struct MapGenerateEvent : IEvent
    {
        public readonly MapGrid MapGrid;
        public readonly MapGenerateData MapGenerateData;
        public readonly int Seed;
        public MapGenerateEvent(MapGrid mapGrid,MapGenerateData mapGenerateData,int seed)
        {
            MapGrid = mapGrid;
            MapGenerateData = mapGenerateData;
            Seed = seed;
        }
    }
    
    public readonly struct MapGenerateEndEvent : IEvent
    {
        public readonly MapGrid MapGrid;
        public MapGenerateEndEvent(MapGrid mapGrid)
        {
            MapGrid = mapGrid;
        }
    }
}