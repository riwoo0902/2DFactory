using LrwLib.LrwAddClass;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Script._Map
{
    [DefaultExecutionOrder(-10)]
    public class MapMaker : MonoBehaviour
    {
        private IMapLayer[] _layers;
        
        private void Awake()
        {
            _layers = GetComponentsInChildren<IMapLayer>();

            _layers.Foreach(x => x.Initialize());
            
        }
        
        
        
        
    }
}