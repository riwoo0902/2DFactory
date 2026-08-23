using System;
using System.Linq;
using System.Text;
using _Script._Test;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Script._SaveSystem._Test
{
    [RequireComponent(typeof(Tilemap))]
    public class MapSave : MonoBehaviour
    {
        
        private Tilemap _tilemap;

        [SerializeField] private Tile tile;
        
        private void Awake()
        {
            _tilemap = GetComponent<Tilemap>();
        }

        private void OnEnable()
        {
            LoadTileMap();
        }

        private void LoadTileMap()
        {
            _tilemap.ClearAllTiles();
            _tilemap.CompressBounds();
            
            string data = SaveManager.ReadFile("TestSaveData.txt");
            
            if(string.IsNullOrEmpty(data)) return;
            
            Vector3Int[] posArr = data.Split("\n")
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(ChangeVector)
                .ToArray();

            foreach (var pos in posArr)
            {
                _tilemap.SetTile(pos, tile);
            }
            
        }

        private Vector3Int ChangeVector(string data)
        {
            try
            {
                int[] arr = data.Split(",").Select(int.Parse).ToArray();
                Vector3Int vec = new(arr[0], arr[1], arr[2]);
                return vec;
            }
            catch
            {
                FDebug.Log(data);
                return Vector3Int.zero;
            }
            
        }

        private void OnDisable()
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
                    Vector3Int vec = new Vector3Int(x, y);
                    if(_tilemap.HasTile(vec))
                        sb.AppendLine($"{ChangeString(vec)}");
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