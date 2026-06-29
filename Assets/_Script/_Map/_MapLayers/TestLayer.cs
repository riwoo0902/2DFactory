using UnityEngine;

namespace _Script._Map._MapLayers
{
    public class TestLayer : AbstractMapLayer
    {
        public override void Initialize()
        {
            base.Initialize();
            Priority = 0;
        }

        public override void CreateLayer(IMapLayer prevLayer = null)
        {
            Debug.Log("2");
        }

        public override float GetLoadingValue()
        {
            return 0;
        }

        public override bool Complete()
        {
            return false;
        }
    }
}