using System;
using System.Collections.Generic;
using _Script._2DPlatformAstar.Data;
using _Script._2DPlatformAstar.Data.AstarData;
using _Script._2DPlatformAstar.Data.MapData;
using _Script._Test;
using UnityEngine;

namespace _Script._2DPlatformAstar.MoveSystem
{
    public static class PathFind
    {
        public static List<Vector2> GetPath(NavMap navMap, Vector2 startPos, Vector2 targetPos)
        {
            List<Vector2> path = new List<Vector2>();

            Vector2Int startTilePos = navMap.WorldToTile(startPos); 
            Vector2Int targetTilePos = navMap.WorldToTile(targetPos); 
            
            List<Vector2Int> tilePath = GetTilePath(navMap, startTilePos, targetTilePos);
            
            if(tilePath == null) return null;
            
            foreach (Vector2Int tilePos in tilePath)
            {
                path.Add(navMap.TileToWorld(tilePos));
            }
            
            path.Add(targetPos);
            
            return path;
        }

        private static List<Vector2Int> GetTilePath(NavMap navMap,
            Vector2Int startPos, Vector2Int targetPos)
        {
            if (startPos == targetPos) return new();
            
            if(!navMap.TryGet(startPos, out Node startNode)
               || !navMap.TryGet(targetPos, out _)) return null;
            
            MinHeap<PathData> heap = new MinHeap<PathData>(PathDataComparison);
            Dictionary<Vector2Int,PathData> dict = new();
            
            dict.Add(startPos,new PathData(startPos,startPos,0f,Vector2Int.Distance(startPos,targetPos)));

            foreach (Link link in startNode.Links)
            {
                PathData newPathData = new PathData(startPos, link.Target, 
                    link.Distance ,Vector2Int.Distance(link.Target,targetPos));
                
                heap.Push(newPathData);
                dict.Add(link.Target,newPathData);
            }
            
            PathData endPathData;

            int count = 0;
            
            while (true)
            {
                if (heap.Count <= 0) return null;
                
                PathData currentData = heap.Pop();
                count++;
                if (currentData.Pos == targetPos)
                {
                    endPathData = currentData;
                    break;
                }

                if(!navMap.TryGet(currentData.Pos, out Node currentNode)) continue;

                foreach (Link link in currentNode.Links)
                {
                    if(dict.ContainsKey(link.Target)) continue;
                    
                    PathData newPathData = new PathData(link.Start, link.Target,
                        currentData.G + link.Distance 
                        ,Vector2Int.Distance(link.Target,targetPos));
                    
                    heap.Push(newPathData);
                    dict.Add(link.Target,newPathData);
                }
            }
            
            FDebug.Log(count);
            
            List<Vector2Int> path = new();

            PathData currentPath = endPathData;
            
            while (true)
            {
                if(currentPath.PrevPos == startPos) break;
                path.Add(currentPath.PrevPos);
                if (!dict.TryGetValue(currentPath.PrevPos, out currentPath)) 
                    throw new Exception("이전 위치 없음");
            }
            
            path.Reverse();

            return path;
        }

        private static int PathDataComparison(PathData a, PathData b)
        {
            if(Mathf.Approximately(a.Distance, b.Distance)) return 0;
            return a.Distance - b.Distance > 0 ? 1 : -1;
        }
        
        
    }
}