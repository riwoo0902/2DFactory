using System.Collections;
using System.Threading.Tasks;
using _Script._Core._EventSystem;
using _Script._Map._MapLayer;
using _Script._Map._MapLayer._Layer;
using _Script._Map._MapLayer._Layer._Layers;
using UnityEngine;

namespace _Script._Map._MapGenerator
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] private MapGenerateData mapGenerateData;
        
        private IMap _map;
        
        #region GenerateStarter
        private void Awake()
        {
            if (mapGenerateData == null)
            {
                Debug.LogError("MapGenerateData is null");
                return;
            }
            EventBus<MapSettingEndEvent>.Event += MapSettingEnd;
        }
        
        private void OnDestroy()
        {
            EventBus<MapSettingEndEvent>.Event -= MapSettingEnd;
        }

        private void MapSettingEnd(MapSettingEndEvent evt)
        {
            _map = evt.Map;
            TaskRun(evt.Map);
        }
        
        private async void TaskRun(IMap map) => await Task.Run(MapGenerate);
        
        #endregion
        
        private void MapGenerate()
        {
            BiomeLayer biome = _map.GetLayer(LayerType.Biome) as BiomeLayer;
            
        }
        
        
    }
}