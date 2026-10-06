using _Script._2DPlatformAstar.Data.MapData;
using _Script._Core._ServiceLocator;
using UnityEngine;

namespace _Script._2DPlatformAstar.Surface
{
    public class NullNavSurface : INavSurface
    {
        public NavMap NavMapData => GetNavMapData();
        
        private NavMap GetNavMapData()
        {
            INavSurface surface = ServiceLocator.Get<INavSurface>();
            return surface is NullNavSurface ? null : surface.NavMapData;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void NullServiceRegister()
        {
            NullNavSurface nullService = new NullNavSurface();
            ServiceLocator.Register<INavSurface>(nullService);
        }

        
    }
}