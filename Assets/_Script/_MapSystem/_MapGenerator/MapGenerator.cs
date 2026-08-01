using System;
using System.Collections.Generic;
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

        private static HashSet<MapGrid> _grids = new();
        private async void MapGenerate(MapGenerateEvent evt)
        {
            try
            {
                MapGrid grid = evt.MapGrid;
                int seed = evt.Seed;
            
                Debug.Assert(grid != null,"MapGrid is null");

                if (!_grids.Add(grid))
                {
                    Debug.LogWarning("This MapGrid is Generating");
                    return;
                }

                grid.Clear();

                await Task.Run(() => MapGenerate(grid, seed));
            
                grid.Flush();
                
                _grids.Remove(grid);
                
                EventBus<MapGenerateEndEvent>.Invoke(new MapGenerateEndEvent());
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
        
        /*private void BiomeGenerate(MapTileMap tileMap,int seed)
        {
            Debug.Assert(tileMap != null,"BiomeTileMap is null");
            
            Random random = new Random(seed);
            
            int biomeCount = mapGenerateData.BiomeCount;
            Biome[] biomes = new Biome[biomeCount];
            
            Vector2Int mapSize = new Vector2Int(mapGenerateData.MapSizeX, mapGenerateData.MapSizeY);
            
            for (int i = 0; i < biomeCount; i++)
            {
                Vector3Int pos = new Vector3Int(random.Next(0, mapSize.x), random.Next(0, mapSize.y));
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
                    biome.BiomePositions.Add(pos);
                    tileMap.SetTile(pos, new BiomeTile(biome.Data));
                }
            }
        }

        private Biome GetNearBiome(Vector3Int pos,Biome[] biomes)
        {
            if(biomes == null) return null;
            
            Biome currentBiome = null;
            
            float a = Mathf.PerlinNoise(pos.x, pos.y);
            
            float minDistance = float.MaxValue;
            
            foreach (Biome biome in biomes)
            {
                if(biome == null) continue;
                float distance = Vector3.SqrMagnitude(pos - biome.CenterPos);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    currentBiome = biome;
                }
            }
            
            return currentBiome;
        }*/

        #region V2

        private static readonly Vector3Int[] Dir =
        {
            new Vector3Int(1,1),  new Vector3Int(0,1),  new Vector3Int(-1,1),
            new Vector3Int(1,0),                        new Vector3Int(-1,0),
            new Vector3Int(1,-1), new Vector3Int(0,-1), new Vector3Int(-1,-1),
        };
        private void BiomeGenerate(MapTileMap tileMap, int seed)
        {
            Random random = new Random(seed);
            
            List<Biome> biomeList = new List<Biome>();
            
            HashSet<Vector3Int> visitedTiles = new HashSet<Vector3Int>();
            
            Vector2Int mapSize = new Vector2Int(mapGenerateData.MapSizeX, mapGenerateData.MapSizeY);

            for (int x = 0; x < mapSize.x; x++)
            {
                for (int y = 0; y < mapSize.y; y++)
                {
                    Vector3Int currentPos = new Vector3Int(x, y);
                    
                    if(!visitedTiles.Add(currentPos)) continue;
                    
                    float currentValue = GetPerlinNoise(currentPos);

                    if (Mathf.Abs(currentValue) < mapGenerateData.BiomeLinePower) continue;
                    
                    Queue<Vector3Int> queue = new Queue<Vector3Int>();
                    
                    queue.Enqueue(currentPos);

                    Biome biome = new Biome(mapGenerateData.BiomeTiles[random.Next(0, mapGenerateData.BiomeTiles.Length)]);
                    biomeList.Add(biome);

                    while (queue.Count > 0)
                    {
                        currentPos = queue.Dequeue();
                        foreach (var dirVec in Dir)
                        {
                            Vector3Int nextPos = currentPos + dirVec;
                            if(!visitedTiles.Add(nextPos)) continue;
                            biome.Size += 1;
                            biome.BiomePositions.Add(nextPos); 
                            queue.Enqueue(nextPos);
                        }
                    }
                }

                foreach (var biome in biomeList)
                {
                    foreach (var pos in biome.BiomePositions)
                    {
                        tileMap.SetTile(pos,new BiomeTile(biome.Data));
                    }
                }
                
            }
            
        }

        private static float GetPerlinNoise(Vector3Int pos)
            => Mathf.PerlinNoise(pos.x, pos.y) * 2 - 1;

        #endregion
        
    }
}