using System.Collections.Generic;
using _Script._Core;
using _Script._MapSystem._Map._TileMap;
using UnityEngine;

namespace _Script._MapSystem._Map
{
    public class MapGrid
    {
        private readonly Grid _grid;
        
        private readonly Dictionary<MapLayerType, MapTileMap> _tileMaps;
        
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

        public MapTileMap GetTileMap(MapLayerType type) => _tileMaps.GetValueOrDefault(type);

        public Vector3Int GetTilePos(Vector3 pos) => _grid.WorldToCell(pos);

        public void Flush() => _tileMaps.Values.Foreach(x => x.Flush());
        
        public void Clear() => _tileMaps.Values.Foreach(x => x.Clear());
        
    }
}