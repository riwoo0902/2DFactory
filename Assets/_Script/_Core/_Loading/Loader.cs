using System;
using System.Linq;
using UnityEngine;

namespace _Script._Core._Loading
{
    public class Loader<T> where T : ILoading
    {
        private readonly T[] _loadings;
        private readonly int _count;
        
        public Loader(T[] loadings)
        {
            if(loadings == null) throw new Exception("Loading is null");
            if(loadings.Length == 0) throw new Exception("Loading is empty");
            
            _loadings = loadings.Clone() as T[];
            if(_loadings == null) throw new Exception("Loading Clone is null");
            _count = _loadings.Length;
        }

        public float GetLoadingValue()
        {
            float sum = 0;
            foreach (T loading in _loadings)
            {
                sum += loading != null ? Mathf.Clamp(loading.GetLoadingValue(),0,100) : 0;
            }
            return Mathf.Clamp(sum / _count,0,100);
        }
        
        public bool Complete() => _loadings.All(x => x.Complete());
        
    }
}