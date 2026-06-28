using System;
using System.Linq;
using UnityEngine;

namespace _Script._Core._Loading
{
    public class Loader
    {
        private readonly ILoading[] _loadings;
        private readonly int _count;
        
        public Loader(ILoading[] loadings)
        {
            if(loadings == null) throw new Exception("Loading is null");
            if(loadings.Length == 0) throw new Exception("Loading is empty");
            
            _loadings = loadings;
            _count = loadings.Length;
        }

        public float GetLoadingValue()
        {
            float sum = 0;
            foreach (ILoading loading in _loadings) sum += Mathf.Clamp(loading.GetLoadingValue(),0,100);
            return Mathf.Clamp(sum / _count,0,100);
        }
        
        public bool Complete() => _loadings.All(x => x.Complete());
        
    }
}