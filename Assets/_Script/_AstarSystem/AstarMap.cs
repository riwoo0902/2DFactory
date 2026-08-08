using System;
using _Script._Core._EventSystem;
using _Script._MapSystem;
using UnityEngine;

namespace _Script._AstarSystem
{
    public class AstarMap : MonoBehaviour
    {
        private void Awake()
        {
            EventBus<MapGenerateEndEvent>.Event += EventBusOnEvent;
        }

        private void OnDestroy()
        {
            EventBus<MapGenerateEndEvent>.Event -= EventBusOnEvent;
        }

        private void EventBusOnEvent(MapGenerateEndEvent evt)
        {
            
            
            
            
            
            
        }
        
        
        
        
        
    }
}