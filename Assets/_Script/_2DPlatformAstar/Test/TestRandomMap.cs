using System;
using _Script._2DPlatformAstar.Data.MapData;
using _Script._2DPlatformAstar.Surface;
using _Script._Core._ServiceLocator;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

namespace _Script._2DPlatformAstar.Test
{
    public class TestRandomMap : MonoBehaviour
    {
        [SerializeField] private Tilemap map;
        [SerializeField] private Tile tile;
        [SerializeField] private bool block = true;
        private void Update()
        {
            if (Mouse.current.rightButton.isPressed)
            {
                Vector2 mousePos = Mouse.current.position.ReadValue();
                Vector2 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);

                NavMap nav = ServiceLocator.Get<INavSurface>().NavMapData;

                Vector2Int tilePos = nav.WorldToTile(worldMousePos);
                nav.Set(tilePos,block);
                
                map.SetTile((Vector3Int)tilePos,block ? tile : null);
            }
        }
        
    }
}