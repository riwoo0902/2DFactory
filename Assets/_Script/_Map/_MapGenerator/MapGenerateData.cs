using System;
using System.Collections.Generic;
using _Script._Map._MapGenerator._TileData;
using UnityEngine;

namespace _Script._Map._MapGenerator
{
    [CreateAssetMenu(fileName = "Map Generate Data", menuName = "Map/Generate Data", order = 0)]
    public class MapGenerateData : ScriptableObject
    {
        [field:SerializeField] public int Seed { get; private set; } = 10000;
        [field:SerializeField] public Vector2 MapSize { get; private set; }
        
        [Header("LayerData")]
        [field:SerializeField] public BiomeGenerateData BiomeGenerateData { get; private set; }
        
        
    }
    
    [Serializable]
    public class BiomeGenerateData
    {
        public List<BiomeSettingData> BiomeTileData { get; private set; }
        
    }
}