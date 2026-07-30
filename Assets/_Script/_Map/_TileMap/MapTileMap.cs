using System.Collections.Generic;
using _Script._Map._Tile;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Script._Map._TileMap
{
    public class MapTileMap
    {
        private Tilemap _tilemap;
        private Dictionary<Vector3Int,AbstractTile> tiles = new();
        
        public MapTileMap(string name,Transform grid)
        {
            GameObject go = new GameObject(name,typeof(Tilemap),typeof(TilemapRenderer));
            go.transform.SetParent(grid.transform);
            _tilemap = go.GetComponent<Tilemap>();
        }
        
    }
    
    
}