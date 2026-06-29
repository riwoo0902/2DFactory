using System.Threading.Tasks;
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
            _ = CreateMap();
            ServiceLocator.Register<IMap>(this);
        }

        private void OnDestroy()
        {
            ServiceLocator.Register<IMap>(new NullMapService());
        }

        private async Task CreateMap()
        {
            _layers = GetComponentsInChildren<IMapLayer>();
            
            _loader = new Loader<IMapLayer>(_layers);
            
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