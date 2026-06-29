using System;
using System.Collections.Generic;

namespace _Script._Map._MapCreate
{
    public class MapLayerOrderResolver
    {
        private IMapLayer[] _sortLayers;
        private Dictionary<Type[], IMapLayer> _requireData = new();
        
        public MapLayerOrderResolver(IMapLayer[] mapLayers)
        {
            _sortLayers = GetSortLayer(mapLayers);
        }
        
        public IMapLayer[] GetSortLayer(IMapLayer[] mapLayers)
        {
            IMapLayer[] sortLayer = new IMapLayer[mapLayers.Length];
            
            Queue<IMapLayer> queue = new Queue<IMapLayer>();
            HashSet<IMapLayer> hash = new HashSet<IMapLayer>();

            foreach (IMapLayer layer in mapLayers)
            {
                if(layer == null) continue;
                Type[] requireTypes = layer.GetRequireTypes();
                
                 
            }
            
            
            
            return sortLayer;
        }
        
    }
}