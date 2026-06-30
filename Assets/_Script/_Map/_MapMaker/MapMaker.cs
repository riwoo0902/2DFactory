using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using _Script._Core._EventSystem;
using _Script._Core._Loading;
using _Script._Core._ServiceLocator;
using _Script._Map._Event;
using _Script._Map._Interface;
using _Script._Map._MapData;
using UnityEngine;

namespace _Script._Map
{
    [DefaultExecutionOrder(-10)]
    [RequireComponent(typeof(Grid))]
    public class MapMaker : MonoBehaviour
    {
        private MapData _mapData;
        
        private void Awake()
        {
            IAbstractMapLayer[] layers = GetComponentsInChildren<IAbstractMapLayer>(true);
            
            _mapData = new MapData(layers);
            
            _ = CreateMapTask(layers);
            
            StartCoroutine(UpdateLoadingData(layers));
        }
        
        
        private IEnumerator UpdateLoadingData(IAbstractMapLayer[] layers)
        {
            Loader<IAbstractMapLayer> loader = new Loader<IAbstractMapLayer>(layers);
            if(loader == null) throw new Exception("Map loader is null");
            
            while (true)
            {
                EventBus<MapLoadingEvent>.Invoke(MapEvents.MapLoadingEvent.Init(loader.GetLoadingValue()));
                if(loader.Complete()) break;
                yield return null;
            }
            
            EventBus<MapLoadingEndEvent>.Invoke(new MapLoadingEndEvent());
        }

        private async Task CreateMapTask(IAbstractMapLayer[] layers)
        {
            await Task.Run(CreateMap);
            ApplyToTilemap();
        }

        private void CreateMap()
        {
            //맵 데이터 생성
        }

        private void ApplyToTilemap()
        {
            //생성한 데이터 타일맵에 적용
        }
        
    }

    
}