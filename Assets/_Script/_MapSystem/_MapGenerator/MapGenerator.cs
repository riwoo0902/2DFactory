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
                Debug.Log("MapGenerate Start");
                
                MapGrid grid = evt.MapGrid;
                int seed = evt.Seed;

                Debug.Assert(grid != null, "MapGrid is null");

                if (!_grids.Add(grid))
                {
                    Debug.LogWarning("This MapGrid is Generating");
                    return;
                }

                grid.Clear();

                await Task.Run(() => MapGenerate(grid, seed));

                grid.Flush();
                
                _grids.Remove(evt.MapGrid);
                
                Debug.Log("MapGenerate End");
                
                EventBus<MapGenerateEndEvent>.Invoke(new MapGenerateEndEvent(grid));
                
            }
            catch (Exception e)
            {
                Debug.LogError("[MapGenerate] " + e.Message);
            }
        }

        private void MapGenerate(MapGrid grid, int seed)
        {
            Random random = new Random(seed);
            BiomeGenerate(grid.GetTileMap(MapLayerType.Biome) as MapTileMap, random);
            BiomeOutLineGenerate(grid.GetTileMap(MapLayerType.Biome) as MapTileMap,grid.GetTileMap(MapLayerType.Tile) as MapTileMap,random);
        }
        
        #region Biome

        private void BiomeGenerate(MapTileMap tileMap, Random random)
        {
            if(tileMap == null) throw new Exception("BiomeTileMap is null");
            if(random == null) throw new Exception("Random is null");
            
            int biomeSize = mapGenerateData.BiomeSize;
            List<Biome> biomes = new List<Biome>();
            
            Vector2Int biomeCount = new Vector2Int(mapGenerateData.MapSizeX / biomeSize, mapGenerateData.MapSizeY / biomeSize);
            
            for (int x = 0; x < biomeCount.x; x++)
            {
                for (int y = 0; y < biomeCount.y; y++)
                {
                    Vector3Int noise = new Vector3Int(random.Next(0, mapGenerateData.BiomeCenterNoisePower), random.Next(0, mapGenerateData.BiomeCenterNoisePower));
                    Vector3Int pos = GetBiomePos(x,y,biomeSize) + noise;
                    if(!CheckInMap(pos)) continue;
                    TileData data = mapGenerateData.BiomeTiles[random.Next(0, mapGenerateData.BiomeTiles.Length)];
                    biomes.Add(new Biome(data,pos));
                }
            }
            
            Vector2Int mapSize = new Vector2Int(mapGenerateData.MapSizeX, mapGenerateData.MapSizeY);
            
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

        private static Vector3Int GetBiomePos(int xCount, int yCount, int size)
        {
            int radius = size / 2;
            int xOffset = yCount % 2 == 0 ? radius : size;
            return new Vector3Int(xCount * size + xOffset, yCount * size + radius);
        }
        
        private static Biome GetNearBiome(Vector3Int pos,List<Biome> biomes)
        {
            if(biomes == null) return null;
            
            Biome currentBiome = null;
            
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
        }
        
        
        private static readonly Vector3Int[] Dir4 = { new(1,0), new(-1,0), new(0,1), new(0,-1) };
        
        private void BiomeOutLineGenerate(MapTileMap biomeMap,MapTileMap tileMap, Random seed)
        {
            if(biomeMap == null) throw new Exception("BiomeTileMap is null");
            if(tileMap == null) throw new Exception("TileTileMap is null");
            
            Vector2Int mapSize = new Vector2Int(mapGenerateData.MapSizeX, mapGenerateData.MapSizeY);
            
            List<Vector3Int> outLineList = new List<Vector3Int>();
            
            for (int x = 0; x < mapSize.x; x++)
            {
                for (int y = 0; y < mapSize.y; y++)
                {
                    Vector3Int pos = new Vector3Int(x, y);
                    if(!biomeMap.TryGetTile(pos,out AbstractTile currentTile)) continue;
                    
                    bool outLine = false;
                    foreach (var dir in Dir4)
                    {
                        Vector3Int nextPos = pos + dir;
                        if(!biomeMap.TryGetTile(nextPos,out AbstractTile nextTile)) continue;
                        if (currentTile.TileData == nextTile.TileData) continue;
                        outLine = true;
                        break;
                    }

                    if (outLine) outLineList.Add(pos);
                }
            }

            List<Vector3Int> posList = new();
            foreach (Vector3Int outLinePos in outLineList)
            {
                foreach (Vector3Int pos in GetPosList(outLinePos,mapGenerateData.BiomeOutLinePower,posList))
                {
                    tileMap.SetTile(pos,new GameTile(mapGenerateData.BiomeOutLineTile));
                }
            }
        }

        private List<Vector3Int> GetPosList(Vector3Int center, int radius,List<Vector3Int> list = null)
        {
            if(list == null) list = new List<Vector3Int>();
            else list.Clear();
            
            int radiusSquared = radius * radius;

            for (int x = -radius; x <= radius; x++)
            {
                for (int y = -radius; y <= radius; y++)
                {
                    if (x * x + y * y <= radiusSquared)
                    {
                        Vector3Int pos = new Vector3Int(
                            center.x + x,
                            center.y + y,
                            center.z
                        );
                        if(!CheckInMap(pos)) continue;
                        list.Add(pos);
                    }
                }
            }
            
            return list;
        }
        
        #endregion
        
        private bool CheckInMap(Vector3Int pos) => (0 <= pos.x && pos.x < mapGenerateData.MapSizeX && 0 <= pos.y && pos.y < mapGenerateData.MapSizeY);
    }
}