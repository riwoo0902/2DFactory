using System;
using System.Collections.Generic;
using System.Linq;
using _Script._Map._Interface;

namespace _Script._Map._MapData
{
    public class MapData
    {
        private readonly Dictionary<LayerType, IAbstractMapLayer> _layers;

        public MapData(IAbstractMapLayer[] layers)
        {
            _layers = layers.ToDictionary(x => x.LayerType);
        }

        public T GetLayer<T>() where T : IAbstractMapLayer
        {
            if (_layers.Values.FirstOrDefault(x => x is T) is T t) return t;
            
            throw new Exception($"{typeof(T).Name} Layer not found");
        }
        
        
    }
}