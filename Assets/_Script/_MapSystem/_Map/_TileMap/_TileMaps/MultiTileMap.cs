using System;
using System.Collections.Generic;
using _Script._MapSystem._Map._Tile;
using _Script._MapSystem._Map._Tile._Tiles._GameTiles;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Script._MapSystem._Map._TileMap._TileMaps
{
    public class MultiTileMap : ITileMap,IFlush
    {
        private readonly Dictionary<Vector3Int, AbstractTile> _tiles;
        
        private readonly MapTileMap[] _tilemaps;
        private readonly Dictionary<Vector3Int,TileBase> _tileChangeStack;
        

        public MultiTileMap(string name,Transform grid,int sortingOrder = 0)
        {
            _tiles = new();
            _tilemaps = new MapTileMap[3];
            
        }
        
        public bool TryGetTile(Vector3Int position, out AbstractTile tile) 
            => _tiles.TryGetValue(position,out tile);
        
        public bool HasTile(Vector3Int position) 
            => _tiles.ContainsKey(position);
        
        public void SetTile(Vector3Int position, AbstractTile tile)
        {
            int index = GetTileIndex(tile);
            
            if(index == -1) return;
            
            _tiles[position] = tile;
            _tileChangeStack[new Vector3Int(position.x,position.y,index)] = tile.TileData.Tile;
        }

        private int GetTileIndex(AbstractTile tile)
        {
            int index = -1;
            
            Type type = tile.GetType();

            if (type == typeof(ObjectTile)) index = 0;
            else if (type == typeof(WaterTile)) index = 1;
            else if (type == typeof(AirTile)) index = 2;
            
            return index;
        }
        
        public void Flush()
        {
            
        }

        public void Clear()
        {
            _tiles.Clear();
            _tileChangeStack.Clear();
            foreach (var tilemap in _tilemaps)
            {
                tilemap.Clear();
            }
        }

        
    }
}