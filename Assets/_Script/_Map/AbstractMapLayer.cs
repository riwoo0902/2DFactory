using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Script._Map
{
    [RequireComponent(typeof(Tilemap))]
    public abstract class AbstractMapLayer : MonoBehaviour,IMapLayer
    {
        private Tilemap _tilemap;

        public virtual void Initialize()
        {
            _tilemap = GetComponent<Tilemap>();
        }

        public virtual Type[] GetRequireTypes() => null;
        public abstract void CreateLayer(Dictionary<Type,IMapLayer> requireData = null);
        public abstract float GetLoadingValue();
        public abstract bool Complete();
        
    }
}