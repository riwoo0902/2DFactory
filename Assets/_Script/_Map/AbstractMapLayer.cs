using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Script._Map
{
    [RequireComponent(typeof(Tilemap))]
    public abstract class AbstractMapLayer : MonoBehaviour,IMapLayer
    {
        private Tilemap _tilemap;
        private Dictionary<Vector3Int, Tile> _tiles;

        public virtual void Initialize()
        {
            _tilemap = GetComponent<Tilemap>();
            _tiles = new Dictionary<Vector3Int, Tile>();
            
        }

        public abstract void CreateLayer(IMapLayer prevLayer = null);
    }
}