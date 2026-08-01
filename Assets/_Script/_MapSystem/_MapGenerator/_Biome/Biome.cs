using System.Collections.Generic;
using _Script._MapSystem._Map._Tile;
using UnityEngine;

namespace _Script._MapSystem._MapGenerator._Biome
{
    public class Biome
    {
        public int Size;
        public readonly TileData Data;
        public readonly List<Vector3Int> BiomePositions;
        
        public Biome(TileData data)
        {
            Data = data;
            Size = 0;
            BiomePositions = new();
        }
        
    }
}