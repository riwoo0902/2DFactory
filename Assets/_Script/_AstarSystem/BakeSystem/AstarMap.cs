using System;
using System.Collections.Generic;
using System.Linq;
using _Script._Core;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Script._AstarSystem.BakeSystem
{
    public class AstarMap
    {
        private readonly AstarMoveDirDataSo _moveDirDataSo;

        private readonly HashSet<Vector2Int> _map = new();

        private readonly Dictionary<Vector2Int, Node> _nodeDict = new();

        public Node[] Nodes => _nodeDict != null ? _nodeDict.Values.ToArray() : new Node[]{};

        private Vector2Int[] LinkFindDirs => VecGroup.IntDir8;
        
        public AstarMap(AstarMoveDirDataSo moveDirDataSo)
        {
            if (moveDirDataSo == null) throw new Exception("Astar Move Data so is null");
            
            _moveDirDataSo = moveDirDataSo;
        }
        
        public AstarMap(AstarMoveDirDataSo moveDirDataSo,Tilemap tilemap)
        {
            if (moveDirDataSo == null) throw new Exception("Astar Move Data so is null");
            if(tilemap == null) throw new Exception("Tilemap is null");
            
            _moveDirDataSo = moveDirDataSo;
            
            tilemap.CompressBounds();
            BoundsInt boundsInt = tilemap.cellBounds;

            for (int x = boundsInt.min.x; x < boundsInt.max.x; x++)
            {
                for (int y = boundsInt.min.y; y < boundsInt.max.y; y++)
                {
                    if(!tilemap.HasTile(new Vector3Int(x, y))) continue;
                    AddNode(new Vector2Int(x, y));
                }
            }
        }

        public void AddBlock(Vector2Int pos)
        {
            RemoveNode(pos);
            
            _map.Add(pos);

            Vector2Int upPos = pos + Vector2Int.up;
            
            if (CanAddNode(upPos)) AddNode(upPos);
            
        }

        public void RemoveBlock(Vector2Int pos)
        {
            _map.Remove(pos);
            
            Vector2Int upPos = pos + Vector2Int.up;
            if (CanAddNode(upPos)) AddNode(upPos);
        }
        
        private bool CanAddNode(Vector2Int pos)
        {
            if (!_map.Contains(pos + Vector2Int.down)) return false;
            
            foreach (Vector2Int upVec in VecGroup.CenterUp)
            {
                if (_map.Contains(pos + upVec)) return false;
            }
            
            return true;
        }
        
        private void AddNode(Vector2Int pos)
        {
            if(_nodeDict.ContainsKey(pos)) return;

            Node node = new Node(pos);
            
            _nodeDict.Add(pos,node);
            
            foreach (Vector2Int dir in LinkFindDirs)
            {
                Vector2Int nextPos = pos + dir;
                if(!_nodeDict.TryGetValue(nextPos, out Node nextNode)) continue;
                nextNode.Links.Add(pos);
                node.Links.Add(nextPos);
            }
        }
        
        private void RemoveNode(Vector2Int pos)
        {
            if(!_nodeDict.Remove(pos)) return;

            foreach (Vector2Int dir in LinkFindDirs)
            {
                Vector2Int nextPos = pos + dir;
                if(!_nodeDict.TryGetValue(nextPos, out Node nextNode)) continue;
                nextNode.Links.Remove(pos);
            }
        }
        
        
    }
}