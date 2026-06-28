using System.Threading.Tasks;
using LrwLib.ButtonAttribute;
using UnityEngine;

namespace _Script._Map
{
    [DefaultExecutionOrder(-10)]
    public class MapMaker : MonoBehaviour
    {
        private IMapLayer[] _layers;
        
        private void Awake()
        {
            CreateMap();
        }

        [Button]
        private async void CreateMap()
        {
            _layers = GetComponentsInChildren<IMapLayer>();

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
}