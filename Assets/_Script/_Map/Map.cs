using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Script._Map
{
    public class Map : MonoBehaviour
    {
        private MapGrid _mapGrid;

        private void Awake()
        {
            Grid grid = GetComponent<Grid>();
            _mapGrid = new MapGrid(grid);
            
            
        }
        
        
    }
}