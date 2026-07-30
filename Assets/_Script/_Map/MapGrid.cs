using System.Collections.Generic;
using _Script._Core._Manager;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Script._Map
{
    public class MapGrid
    {
        private Grid _grid;
        
        private Dictionary<MapLayerType, MapTileMap> _tileMap;
        
        public MapGrid(Grid grid)
        {
            _grid = grid;
            _tileMap = new();
            foreach (MapLayerType layer in MapLayer.Layers)
            {
                GameObject go = new GameObject(layer.ToString(),typeof(Tilemap));
                go.transform.SetParent(_grid.transform);
                Tilemap tilemap = go.GetComponent<Tilemap>();
                _tileMap.Add(layer,new MapTileMap(tilemap));
            }
        }
        
        
    }
}