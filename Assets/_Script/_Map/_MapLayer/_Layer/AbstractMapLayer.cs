using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Script._Map._MapLayer._Layer
{
    [RequireComponent(typeof(Tilemap))]
    public abstract class AbstractMapLayer : MonoBehaviour,IMapLayer
    {
        public abstract LayerType GetLayerType();

        protected Tilemap Tilemap;
        
        public void Initialize()
        {
            Tilemap = GetComponent<Tilemap>();
            OnInitialize();
        }

        protected virtual void OnInitialize() {}

    }
}