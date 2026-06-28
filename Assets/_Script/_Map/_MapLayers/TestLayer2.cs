using UnityEngine;

namespace _Script._Map._MapLayers
{
    public class TestLayer2 : AbstractMapLayer
    {
        public override void Initialize()
        {
            base.Initialize();
            Priority = -1;
        }
        
        public override void CreateLayer(IMapLayer prevLayer = null)
        {
            Debug.Log("1");
        }
        
    }
}