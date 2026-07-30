using System.Collections.Generic;
using _Script._MapSystem._Map._Tile;
using UnityEditor;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Script._MapSystem._Map._TileMap
{
    public class MapTileMap
    {
        private readonly Dictionary<Vector3Int, AbstractTile> _tiles;
        
        private readonly Tilemap _tilemap;
        private readonly Queue<TileMapChangeData> _tileChangeStack;
        private struct TileMapChangeData
        {
            public Tile Tile;
            public Vector3Int Pos;

            public TileMapChangeData(Vector3Int pos,Tile tile)
            {
                Tile = tile;
                Pos = pos;
            }
        }
        
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
            _tileChangeStack.Enqueue(new TileMapChangeData(position,tile.TileData.Tile));
        }

        public void Flush()
        {
            while (_tileChangeStack.Count > 0)
            {
                TileMapChangeData data = _tileChangeStack.Dequeue();
                _tilemap.SetTile(data.Pos,data.Tile);
            }
        }

        public void Clear()
        {
            _tiles.Clear();
            _tileChangeStack.Clear();
            _tilemap.ClearAllTiles();
        }
        
    }
    
    
}