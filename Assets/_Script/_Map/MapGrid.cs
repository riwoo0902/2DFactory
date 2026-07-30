using System.Collections.Generic;
using _Script._Map._TileMap;
using UnityEngine;

namespace _Script._Map
{
    public class MapGrid
    {
        private Grid _grid;
        
        private Dictionary<MapLayerType, MapTileMap> _tileMaps;
        
        public MapGrid(Grid grid)
        {
            _grid = grid;
            _tileMaps = new();
            
            _tileMaps.Add(MapLayerType.Biome,      new MapTileMap(nameof(MapLayerType.Biome),      _grid.transform));
            _tileMaps.Add(MapLayerType.Tile,       new MapTileMap(nameof(MapLayerType.Tile),       _grid.transform));
            _tileMaps.Add(MapLayerType.Structure,  new MapTileMap(nameof(MapLayerType.Structure),  _grid.transform));
            _tileMaps.Add(MapLayerType.LiquidPipe, new MapTileMap(nameof(MapLayerType.LiquidPipe), _grid.transform));
            _tileMaps.Add(MapLayerType.GasPipe,    new MapTileMap(nameof(MapLayerType.GasPipe),    _grid.transform));
            _tileMaps.Add(MapLayerType.Wire,       new MapTileMap(nameof(MapLayerType.Wire),       _grid.transform));
            
            
        }
        
        
    }
}