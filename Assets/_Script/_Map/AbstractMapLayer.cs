using _Script._Core._Loading;
using _Script._Map._Interface;
using UnityEngine;

namespace _Script._Map
{
    public abstract class AbstractMapLayer : MonoBehaviour,IAbstractMapLayer
    {
        [field:SerializeField] public LayerType LayerType { get; private set; }

        public abstract void Initialize();

        public abstract float GetLoadingValue();

        public abstract bool Complete();
        
    }
}