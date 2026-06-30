using _Script._Core._ServiceLocator;
using _Script._Map._Interface;
using UnityEngine;

namespace _Script._Map
{
    [DefaultExecutionOrder(-10)]
    [RequireComponent(typeof(Grid))]
    public class Map : MonoBehaviour,IMap//모듈 오너
    {
        private void Awake()
        {
            ServiceLocator.Register<IMap>(this);
        }


        private void OnDestroy()
        {
            ServiceLocator.Register<IMap>(new NullMapService());
        }
        
        
    }
    public struct NullMapService : IMap
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Initialize()
        {
            ServiceLocator.Register<IMap>(new NullMapService());
        }
    }
}