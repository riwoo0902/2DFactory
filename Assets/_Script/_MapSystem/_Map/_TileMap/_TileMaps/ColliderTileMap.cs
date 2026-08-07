using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Script._MapSystem._Map._TileMap._TileMaps
{
    public class ColliderTileMap : MapTileMap
    {
        public ColliderTileMap(string name, Transform grid) : base(name, grid)
        {
            GameObject.AddComponent<TilemapCollider2D>().compositeOperation = Collider2D.CompositeOperation.Merge;
            GameObject.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            GameObject.AddComponent<CompositeCollider2D>().geometryType = CompositeCollider2D.GeometryType.Polygons;
        }
        
    }
}