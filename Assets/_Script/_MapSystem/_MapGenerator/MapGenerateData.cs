using _Script._MapSystem._Map._Tile;
using UnityEngine;

namespace _Script._MapSystem._MapGenerator
{
    [CreateAssetMenu(fileName = "MapGenerateData", menuName = "Map/MapGenerateData", order = 0)]
    public class MapGenerateData : ScriptableObject
    {
        [Header("MapSize")]
        [field:SerializeField] public int MapSizeX { get; private set; }
        [field:SerializeField] public int MapSizeY { get; private set; }
        
        [Header("BiomeSetting")]
        [field:SerializeField] public int BiomeCount { get; private set; }
        [field:SerializeField] public TileData[] BiomeTiles { get; private set; }
        [field: SerializeField] public int BiomeMinSize { get; private set; } = 100;


    }
}