using _Script._Map._MapLayer._Layer._Layers._BiomeLayer;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Script._Map._MapGenerator._TileData
{
    [CreateAssetMenu(fileName = "Biome Tile Data", menuName = "Map/TileData/Biome Tile Data", order = 0)]
    public class BiomeSettingData : ScriptableObject
    {
        [field:SerializeField] public BiomeType BiomeType { get; private set; }
        [field:SerializeField] public Tile BiomeTile { get; private set; }
        [field:SerializeField] public float BiomeTemperature { get; private set; }
        
        
    }
}