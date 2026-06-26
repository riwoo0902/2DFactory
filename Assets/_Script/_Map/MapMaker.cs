using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Script._Map
{
    [DefaultExecutionOrder(-10)]
    [RequireComponent(typeof(Tilemap))]
    public class MapMaker : MonoBehaviour
    {
        private Tilemap _tilemap;
        private void Awake()
        {
            _tilemap = GetComponent<Tilemap>();
            
            
        }
        
        
    }
}