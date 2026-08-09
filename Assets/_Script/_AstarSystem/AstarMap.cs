using System;
using _Script._Core._EventSystem;
using _Script._MapSystem;
using _Script._MapSystem._Map;
using _Script._MapSystem._Map._TileMap._TileMaps;
using UnityEngine;

namespace _Script._AstarSystem
{
    public class AstarMap : MonoBehaviour
    {
        private void Awake()
        {
            EventBus<MapGenerateEndEvent>.Event += Init;
        }

        private void OnDestroy()
        {
            EventBus<MapGenerateEndEvent>.Event -= Init;
        }

        private void Init(MapGenerateEndEvent evt)
        {
            ColliderTileMap map = (evt.MapGrid.GetTileMap(MapLayerType.Tile) as MultiTileMap)?.GetTileMap(0) as ColliderTileMap;

            if (map == null) throw new Exception("Map is Null");

            BoundsInt bounds = map.Tilemap.cellBounds;


            
        }
        
        
        
    }
}