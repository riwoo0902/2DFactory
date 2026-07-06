using System;
using System.Collections.Generic;
using System.Linq;
using _Script._Core._EventSystem;
using _Script._Map._MapLayer._Layer;
using UnityEngine;

namespace _Script._Map._MapLayer
{
    [RequireComponent(typeof(Grid))]
    public class Map : MonoBehaviour,IMap
    {
        private Dictionary<LayerType,IMapLayer> _mapLayers = new();
        
        private void Awake()
        {
            IMapLayer[]  mapLayers = GetComponentsInChildren<IMapLayer>();
            _mapLayers = mapLayers.ToDictionary(x => x.GetLayerType());
            
            foreach(IMapLayer mapLayer in mapLayers)
            {
                mapLayer.Initialize();
            }
        }

        private void Start()
        {
            EventBus<MapSettingEndEvent>.Invoke(new MapSettingEndEvent(this));
        }
        
        public IMapLayer GetLayer(LayerType layerType)
        {
            if(_mapLayers.TryGetValue(layerType,out  IMapLayer mapLayer))
                return mapLayer;
            
            throw new Exception($"map layer({layerType}) is not found");
        }
    }
}