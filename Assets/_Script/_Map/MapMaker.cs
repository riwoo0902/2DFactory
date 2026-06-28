using System.Threading.Tasks;
using LrwLib.LrwAddClass;
using UnityEngine;

namespace _Script._Map
{
    [DefaultExecutionOrder(-10)]
    public class MapMaker : MonoBehaviour
    {
        private IMapLayer[] _layers;
        
        private void Awake()
        {
            IMapLayer[] layers = GetComponentsInChildren<IMapLayer>();
            
            CreateTask();
            
        }

        private async void CreateTask() => await Task.Run(CreateLayers);
        
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