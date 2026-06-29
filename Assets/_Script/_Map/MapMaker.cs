using System;
using System.Collections;
using System.Threading.Tasks;
using _Script._Core._EventSystem;
using _Script._Core._Loading;
using _Script._Core._ServiceLocator;
using _Script._Map._MapCreate;
using UnityEngine;

namespace _Script._Map
{
    [DefaultExecutionOrder(-10)]
    [RequireComponent(typeof(Grid))]
    public class MapMaker : MonoBehaviour,IMap
    {
        private IMapLayer[] _layers;
        
        private void Awake()
        {
            ServiceLocator.Register<IMap>(this);
            
            _layers = GetComponentsInChildren<IMapLayer>(true);
            
            foreach (IMapLayer layer in _layers)
            {
                layer.Initialize();
            }
            
            _ = CreateMap();
            
            StartCoroutine(UpdateLoadingData(_layers));
        }
        
        private void OnDestroy()
        {
            ServiceLocator.Register<IMap>(new NullMapService());
        }
        
        private IEnumerator UpdateLoadingData(IMapLayer[] layers)
        {
            Loader<IMapLayer> loader = new Loader<IMapLayer>(layers);
            if(loader == null) throw new Exception("Map loader is null");
            
            while (true)
            {
                EventBus<MapLoadingEvent>.Invoke(MapEvents.MapLoadingEvent.Init(loader.GetLoadingValue()));
                if(loader.Complete()) break;
                yield return null;
            }
            EventBus<MapLoadingEndEvent>.Invoke(new MapLoadingEndEvent());
        }
        
        private async Task CreateMap() => await Task.Run(CreateLayers);
        
        private void CreateLayers()
        {
            
            
            
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