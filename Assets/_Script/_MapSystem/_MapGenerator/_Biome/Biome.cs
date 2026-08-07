using System.Collections.Generic;
using _Script._MapSystem._Map._Tile;
using UnityEngine;

namespace _Script._MapSystem._MapGenerator._Biome
{
    public class Biome
    {
        public int Size;
        public readonly TileData Data;
        public readonly Vector3Int CenterPos;
        
        public Biome(TileData data,Vector3Int centerPos)
        {
            Data = data;
            Size = 0;
            CenterPos = centerPos;
        }
        
    }
}