using System.Collections.Generic;
using System.Linq;
using _Script._Core;
using _Script._MapSystem._Map._TileMap;
using _Script._MapSystem._Map._TileMap._TileMaps;
using UnityEngine;

namespace _Script._MapSystem._Map
{
    public class MapGrid
    {
        private readonly Grid _grid;
        
        private readonly Dictionary<MapLayerType, ITileMap> _tileMaps;
        
        public bool IsGenerating { get; set; }
        
        public MapGrid(Grid grid)
        {
            _grid = grid;
            _tileMaps = new();
            
            _tileMaps.Add(MapLayerType.Biome,      new MapTileMap(nameof(MapLayerType.Biome),      _grid.transform,0));
            _tileMaps.Add(MapLayerType.Tile,       new MultiTileMap(nameof(MapLayerType.Tile),  _grid.transform,1));
            _tileMaps.Add(MapLayerType.Structure,  new MapTileMap(nameof(MapLayerType.Structure),  _grid.transform,2));
            _tileMaps.Add(MapLayerType.LiquidPipe, new MapTileMap(nameof(MapLayerType.LiquidPipe), _grid.transform,3));
            _tileMaps.Add(MapLayerType.GasPipe,    new MapTileMap(nameof(MapLayerType.GasPipe),    _grid.transform,4));
            _tileMaps.Add(MapLayerType.Wire,       new MapTileMap(nameof(MapLayerType.Wire),       _grid.transform,5));
        }

        public ITileMap GetTileMap(MapLayerType type) => _tileMaps.GetValueOrDefault(type);

        public Vector3Int GetTilePos(Vector3 pos) => _grid.WorldToCell(pos);

        public void Flush()
        {
            foreach (var map in _tileMaps.Values)
            {
                if (map is IFlush flush)
                {
                    flush.Flush();
                }
            }
        }
        
        public void Clear() => _tileMaps.Values.Foreach(x => x.Clear());
        
    }
}