using System.Collections;
using System.Threading.Tasks;
using _Script._Core._EventSystem;
using _Script._Map._MapLayer;
using UnityEngine;

namespace _Script._Map._MapGenerator
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] private MapGenerateData mapGenerateData;
        
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

        private IMap _map;
        private async void MapGenerate(IMap map)
        {
            _map = map;
            
            await Task.Run(TaskRun);

            StartCoroutine(LoadingUpdate());
        }

        private IEnumerator LoadingUpdate()
        {
            while (true)
            {
                yield return null;
            }
        }

        private void TaskRun()
        {
            
            
            
            
        }
        
        
    }
}