using System.Collections.Generic;
using System.Linq;
using _Script._MapSystem._Map._Tile;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Script._MapSystem._Map._TileMap
{
    public class MapTileMap
    {
        private readonly Dictionary<Vector3Int, AbstractTile> _tiles;
        
        private readonly Tilemap _tilemap;
        private readonly Dictionary<Vector3Int,TileBase> _tileChangeStack;
        
        public MapTileMap(string name,Transform grid)
        {
            _tiles = new();
            GameObject go = new GameObject(name,typeof(Tilemap),typeof(TilemapRenderer));
            go.transform.SetParent(grid.transform);
            _tilemap = go.GetComponent<Tilemap>();
            
            _tileChangeStack = new();
        }
        
        public bool TryGetTile(Vector3Int position, out AbstractTile tile)
        {
            return _tiles.TryGetValue(position,out tile);
        }
        
        public bool HasTile(Vector3Int position) => _tiles.ContainsKey(position);

        public void SetTile(Vector3Int position, AbstractTile tile)
        {
            _tiles[position] = tile;
            _tileChangeStack[position] = tile.TileData.Tile;
        }

        public void Flush()
        {
            Vector3Int[] posArr = _tileChangeStack.Keys.ToArray();
            TileBase[] tiles =  _tileChangeStack.Values.ToArray();
            _tilemap.SetTiles(posArr,tiles);
        }

        public void Clear()
        {
            _tiles.Clear();
            _tileChangeStack.Clear();
            _tilemap.ClearAllTiles();
        }
        
    }
    
    
}