using _Script._Core._EventSystem;
using _Script._MapSystem._MapGenerator;
using LrwLib.ButtonAttribute;
using UnityEngine;

namespace _Script._MapSystem._Map
{
    [RequireComponent(typeof(Grid))]
    public class Map : MonoBehaviour
    {
        [SerializeField] private MapGenerateData mapGenerateData;
        [SerializeField] private int seed;
        
        private MapGrid _mapGrid;

        private void Awake()
        {
            Grid grid = GetComponent<Grid>();
            Debug.Assert(grid != null,"Grid is null");
            _mapGrid = new MapGrid(grid);
        }

        private void Start()
        {
            GenerateMap();
        }
        
        private void GenerateMap()
        {
            EventBus<MapGenerateEvent>.Invoke(new MapGenerateEvent(_mapGrid,mapGenerateData,seed));
        }
        
        [UnityButton("TestGenerateMap")]
        private void TestGenerateMap()
        {
            EventBus<MapGenerateEvent>.Invoke(new MapGenerateEvent(_mapGrid,mapGenerateData,Random.Range(0,10000)));
        }
        
    }
}