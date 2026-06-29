using System;
using System.Collections;
using System.Threading.Tasks;
using _Script._Core._EventSystem;
using _Script._Core._Loading;
using _Script._Core._ServiceLocator;
using UnityEngine;

namespace _Script._Map
{
    [DefaultExecutionOrder(-10)]
    [RequireComponent(typeof(Grid))]
    public class MapMaker : MonoBehaviour,IMap
    {
        private IMapLayer[] _layers;
        private Loader<IMapLayer> _loader;
        
        private void Awake()
        {
            _layers = GetComponentsInChildren<IMapLayer>();
            
            _ = CreateMap();
            
            ServiceLocator.Register<IMap>(this);

            StartCoroutine(UpdateLoadingData(new Loader<IMapLayer>(_layers)));
        }

        private IEnumerator UpdateLoadingData(Loader<IMapLayer> loader)
        {
            if(loader == null) throw new Exception("Map loader is null");
            
            while (true)
            {
                EventBus<MapLoadingEvent>.Invoke(MapEvents.MapLoadingEvent.Init(loader.GetLoadingValue()));
                if(loader.Complete()) break;
                yield return null;
            }
            EventBus<MapLoadingEndEvent>.Invoke(new MapLoadingEndEvent());
        }

        private void OnDestroy()
        {
            ServiceLocator.Register<IMap>(new NullMapService());
        }

        private async Task CreateMap()
        {
            foreach (IMapLayer layer in _layers)
            {
                layer.Initialize();
            }
            
            await Task.Run(CreateLayers);
        }
        
        private void CreateLayers()
        {
            IMapLayer prevLayer = null;
            foreach (IMapLayer layer in _layers)
            {
                layer.CreateLayer(prevLayer);
                prevLayer = layer;
            }
        }
        
    }

    public struct NullMapService : IMap
    {

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Initialize()
        {
            ServiceLocator.Register<IMap>(new NullMapService());
        }
    }
}