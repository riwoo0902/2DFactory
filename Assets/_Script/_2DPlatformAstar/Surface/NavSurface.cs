using System;
using _Script._2DPlatformAstar.Data;
using _Script._2DPlatformAstar.Data.MapData;
using _Script._Core._ServiceLocator;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Script._2DPlatformAstar.Surface
{
    public class NavSurface : MonoBehaviour, INavSurface
    {
        [SerializeField] private Tilemap tilemap;
        
        public NavMap NavMapData { get; private set; }
        
        private void Awake()
        {
            NavMapData = new NavMap(tilemap);
            ServiceLocator.Register<INavSurface>(this);
            TestInit();
        }
        
        private void OnDestroy()
        {
            NullNavSurface.NullServiceRegister();
        }

        private void Update()
        {
            NavMapData.Update();
        }

        private void TestInit()
        {
            if(tilemap == null) return;
            
            tilemap.CompressBounds();
            BoundsInt bound = tilemap.cellBounds;

            for (int x = bound.xMin; x < bound.xMax; x++)
            {
                for (int y = bound.yMin; y < bound.yMax; y++)
                {
                    Vector2Int pos = new Vector2Int(x, y);
                    NavMapData.Set(pos,tilemap.HasTile((Vector3Int)pos));
                }
            }
        }
        
#if UNITY_EDITOR
        private Vector3 TileToWorldPos(Vector2Int pos)
            => NavMapData.TileToWorld(pos);
        private void OnDrawGizmosSelected()
        {
            if(NavMapData == null) return;
            var values = NavMapData.Map.Values;

            foreach (Node node in values)
            {
                DrawNode(node);
            }
        }

        private void DrawNode(Node node)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(TileToWorldPos(node.Position),0.3f);

            foreach (Link link in node.Links)
            {
                DrawLink(link);
            }
        }
        
        private void DrawLink(Link link)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(TileToWorldPos(link.Start), TileToWorldPos(link.Target));
        }
#endif
    }
}