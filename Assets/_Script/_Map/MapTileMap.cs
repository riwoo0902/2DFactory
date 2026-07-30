using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Script._Map
{
    public class MapTileMap
    {
        private Tilemap _tilemap;
        private Dictionary<Vector3Int,AbstractTile> tiles = new();
        
        public MapTileMap(Tilemap tileMap)
        {
            _tilemap = tileMap;
        }
        
    }
    
    
}