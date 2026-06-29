using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Script._Map._MapLayers
{
    public class TestLayer : AbstractMapLayer
    {
        public override void Initialize()
        {
            base.Initialize();
        }

        public override Type[] GetRequireTypes()
        {
            return new Type[]{};
        }

        public override void CreateLayer(Dictionary<Type, IMapLayer> requireData = null)
        {
            Debug.Log(1);
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