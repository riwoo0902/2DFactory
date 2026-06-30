using _Script._Map._Interface;
using UnityEngine;

namespace _Script._Map._MapLayers
{
    public class TestLayer : AbstractMapLayer
    {
        public override float GetLoadingValue()
        {
            return 50f;
        }

        public override bool Complete()
        {
            return false;
        }
        
        public override void Initialize()
        {
            
        }
        
    }
}