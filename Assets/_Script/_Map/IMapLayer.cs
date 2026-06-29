using System;
using System.Collections.Generic;
using _Script._Core._Loading;

namespace _Script._Map
{
    public interface IMapLayer : ILoading
    {
        Type[] GetRequireTypes();
        void Initialize();
        
        void CreateLayer(Dictionary<Type,IMapLayer> requireData = null);
    }
}