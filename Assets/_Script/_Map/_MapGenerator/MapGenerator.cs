using System.Threading.Tasks;
using _Script._Core._EventSystem;
using _Script._Map._MapLayer;
using UnityEngine;

namespace _Script._Map._MapGenerator
{
    public class MapGenerator : MonoBehaviour
    {
        #region GenerateStarter
        private void Awake()
        {
            EventBus<MapSettingEndEvent>.Event += MapSettingEnd;
        }
        
        private void OnDestroy()
        {
            EventBus<MapSettingEndEvent>.Event -= MapSettingEnd;
        }
        
        private void MapSettingEnd(MapSettingEndEvent evt) => MapGenerate(evt.Map);

        #endregion
        
        private async void MapGenerate(IMap map)
        {
            await Task.Run(TaskRun);
        }

        private void TaskRun()
        {
            
        }
    }
}