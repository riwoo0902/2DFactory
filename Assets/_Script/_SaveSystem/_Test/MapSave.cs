using System;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Script._SaveSystem._Test
{
    [RequireComponent(typeof(Tilemap))]
    public class MapSave : MonoBehaviour
    {
        
        private Tilemap _tilemap;

        [SerializeField] private Tile _tile;
        
        private void Awake()
        {
            _tilemap = GetComponent<Tilemap>();
            LoadTileMap();
        }

        private void LoadTileMap()
        {
            _tilemap.ClearAllTiles();
            _tilemap.CompressBounds();
            
            string data = SaveManager.ReadFile("TestSaveData.txt");
            
            if(string.IsNullOrEmpty(data)) return;
            
            Vector3Int[] posArr = data.Split("\n").Select(ChangeVector).ToArray();

            foreach (var pos in posArr)
            {
                _tilemap.SetTile(pos, _tile);
            }
            
        }

        private Vector3Int ChangeVector(string data)
        {
            int[] arr = data.Split(",").Select(int.Parse).ToArray();
            Vector3Int vec = new(arr[0], arr[1], arr[2]);
            return vec;
        }

        private void OnDestroy()
        {
            Save();
        }

        private void Save()
        {
            StringBuilder sb = new();
            BoundsInt bounds = _tilemap.cellBounds;

            for (int x = bounds.xMin; x < bounds.xMax; x++)
            {
                for (int y = bounds.yMin; y < bounds.yMax; y++)
                {
                    sb.Append($"{ChangeString(new Vector3Int(x,y))}\n");
                }
            }
            SaveManager.WriteFile("TestSaveData.txt", sb.ToString());
        }

        private string ChangeString(Vector3Int pos)
        {
            return $"{pos.x},{pos.y},{pos.z}";
        }
    }
}