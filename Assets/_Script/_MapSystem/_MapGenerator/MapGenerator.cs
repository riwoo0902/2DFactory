using System;
using System.Threading.Tasks;
using _Script._Core._EventSystem;
using _Script._MapSystem._Map;
using _Script._MapSystem._Map._Tile;
using _Script._MapSystem._Map._Tile._Tiles;
using _Script._MapSystem._Map._TileMap;
using _Script._MapSystem._MapGenerator._Biome;
using UnityEngine;
using Random = System.Random;

namespace _Script._MapSystem._MapGenerator
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] private MapGenerateData mapGenerateData;
        
        private void Awake()
        {
            EventBus<MapGenerateEvent>.Event += MapGenerate;
        }

        private void OnDestroy()
        {
            EventBus<MapGenerateEvent>.Event -= MapGenerate;
        }

        private async void MapGenerate(MapGenerateEvent evt)
        {
            try
            {
                MapGrid grid = evt.MapGrid;
                int seed = evt.Seed;
            
                Debug.Assert(grid != null,"MapGrid is null");
            
                grid.Clear();

                await Task.Run(() => MapGenerate(grid, seed));
            
                grid.Flush();
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }

        private void MapGenerate(MapGrid grid, int seed)
        {
            BiomeGenerate(grid.GetTileMap(MapLayerType.Biome),seed);
        }

        private void BiomeGenerate(MapTileMap tileMap,int seed)
        {
            Debug.Assert(tileMap != null,"BiomeTileMap is null");
            
            Random random = new Random(seed);
            
            int biomeCount = mapGenerateData.BiomeCount;
            Biome[] biomes = new Biome[biomeCount];
            
            Vector2Int mapSize = new Vector2Int(mapGenerateData.MapSizeX, mapGenerateData.MapSizeY);
            
            for (int i = 0; i < biomeCount; i++)
            {
                Vector3Int pos = new Vector3Int(random.Next(0, mapSize.y), random.Next(0, mapSize.y));
                TileData data = mapGenerateData.BiomeTiles[random.Next(0, mapGenerateData.BiomeTiles.Length)];
                biomes[i] = new Biome(pos, data);
            }
            
            for (int x = 0; x < mapSize.x; x++)
            {
                for (int y = 0; y < mapSize.y; y++)
                {
                    Vector3Int pos = new Vector3Int(x, y);
                    Biome biome = GetNearBiome(pos, biomes);
                    biome.Size += 1;
                    tileMap.SetTile(pos, new BiomeTile(biome.Data));
                }
            }
            
            
            
        }
        

        private static Biome GetNearBiome(Vector3Int pos,Biome[] biomes)
        {
            if(biomes == null) return null;
            
            Biome currentBiome = null;
            
            float minDistance = float.MaxValue;
            
            foreach (Biome biome in biomes)
            {
                if(biome == null) continue;
                float distance = Vector3.Distance(pos,biome.CenterPos);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    currentBiome = biome;
                }
            }
            
            return currentBiome;
        }
        
        
    }
}