using System.Collections.Generic;
using _Script._MapSystem._Map._Tile;
using UnityEngine;

namespace _Script._MapSystem._MapGenerator._Biome
{
    public class Biome
    {
        public readonly TileData Data;
        public readonly Vector3Int CenterPos;
        
        public Biome(TileData data,Vector3Int centerPos)
        {
            Data = data;
            CenterPos = centerPos;
        }
        
    }
}