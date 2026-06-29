using System;
using System.Collections.Generic;
using System.Linq;

namespace _Script._Map._MapCreate
{
    public class MapLayerOrderResolver
    {
        public readonly Dictionary<Type, IMapLayer> MapData;
        private readonly IMapLayer[] _sortLayers;
        private int _currentIndex = 0;
        private readonly int _maxIndex;
        
        public MapLayerOrderResolver(IMapLayer[] mapLayers)
        {
            _sortLayers = GetSortLayer(mapLayers);
            MapData = _sortLayers.ToDictionary(x => x.GetType());
            _maxIndex = _sortLayers.Length - 1;
        }

        public bool TryGetNextLayer(out IMapLayer layer)
        {
            if (_currentIndex <= _maxIndex)
            {
                layer = _sortLayers[_currentIndex++];
                return true;
            }

            layer = null;
            return false;
        }

        private IMapLayer[] GetSortLayer(IMapLayer[] mapLayers)
        {
            Dictionary<Type, List<IMapLayer>> requireData = new();
            
            List<IMapLayer> firstLayer = new();
            
            foreach (IMapLayer layer in mapLayers)
            {
                if(layer == null) continue;
                
                Type[] types = layer.GetRequireTypes();
                if (types == null || types.Length == 0)
                {
                    firstLayer.Add(layer);
                    continue;
                }
                
                foreach (Type requireType in layer.GetRequireTypes())
                {
                    if (requireData.TryGetValue(requireType, out List<IMapLayer> requireLayer)) requireLayer.Add(layer);
                    else requireData.Add(requireType,new List<IMapLayer>{layer});
                }
            }
            
            Queue<IMapLayer> queue = new();
            HashSet<Type> hash = new();
            
            List<IMapLayer> returnLayer = new(mapLayers.Length);

            foreach (IMapLayer layer in firstLayer)
            {
                queue.Enqueue(layer);
                hash.Add(layer.GetType());
            }

            while (queue.Count > 0)
            {
                IMapLayer currentLayer = queue.Dequeue();
                
                returnLayer.Add(currentLayer);
                
                Type currentType = currentLayer.GetType();
                if(!requireData.TryGetValue(currentType, out List<IMapLayer> targetLayer)) continue;

                foreach (IMapLayer layer in targetLayer)
                {
                    if(hash.Contains(layer.GetType())) continue;
                    if (!CheckHaveRequireTypes(hash, layer)) continue;
                    
                    queue.Enqueue(layer);
                    hash.Add(layer.GetType());
                }
            }
            
            return returnLayer.ToArray();
        }

        private static bool CheckHaveRequireTypes(HashSet<Type> types, IMapLayer mapLayer)
        {
            if(mapLayer == null) return false;
            
            foreach (Type targetLayerRequireType in mapLayer.GetRequireTypes())
            {
                if(!types.Contains(targetLayerRequireType)) return false;
            }
            return true;
        }
    }
}