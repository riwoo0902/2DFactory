using _Script._Core._EventSystem;
using _Script._Map;
using UnityEngine;

namespace _Script._Test
{
    public class MapLoadingTest : MonoBehaviour
    {
        private void Awake()
        {
            EventBus<MapLoadingEvent>.Event += EventBusOnEvent;
        }

        private void OnDestroy()
        {
            EventBus<MapLoadingEvent>.Event -= EventBusOnEvent;
        }

        private void EventBusOnEvent(MapLoadingEvent evt)
        {
            Debug.Log(evt.Value);
        }
        
    }
}