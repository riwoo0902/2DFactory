using UnityEngine;

namespace _Script._Map
{
    [RequireComponent(typeof(Grid))]
    public class Map : MonoBehaviour
    {
        private MapGrid _mapGrid;

        private void Awake()
        {
            Grid grid = GetComponent<Grid>();
            Debug.Assert(grid != null,"Grid is null");
            _mapGrid = new MapGrid(grid);
            
        }
        
        
    }
}