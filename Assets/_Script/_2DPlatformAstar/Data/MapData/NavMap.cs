using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Script._2DPlatformAstar.Data.MapData
{
    public class NavMap
    {
        public readonly Dictionary<Vector2Int, Node> Map;
        
        public event Action OnMapChanged;

        private readonly Vector2 _origin;
        private readonly Vector2 _cellSize;

        private bool _mapChanged = false;
        
        public NavMap(Tilemap tilemap)
        {
            Map = new();
            _origin = tilemap.transform.position;
            _cellSize = tilemap.layoutGrid.cellSize;
        }

        public void Update()
        {
            if(!_mapChanged) return;
            
            _mapChanged = false;
            OnMapChanged?.Invoke();
        }
        
        public bool TryGet(Vector2Int pos,out Node node)
            => Map.TryGetValue(pos, out node);

        public void Set(Vector2Int pos, bool block)
        {
            if (block)
            {
                Node newNode = new Node(pos);
                Map[pos] = newNode;
                UpdateNode(newNode);
            }
            else Map.Remove(pos);
            
            foreach (Vector2Int dir in DirData.Dirs)
            {
                Vector2Int currentPos = pos + dir;
                if(!Map.TryGetValue(currentPos,out Node node)) continue;
                UpdateNode(node);
            }

            _mapChanged = true;
        }

        private void UpdateNode(Node node)
        {
            node.Links.Clear();
            
            foreach (Vector2Int dir in DirData.Dirs)
            {
                Vector2Int currentPos = node.Position + dir;
                if (Map.TryGetValue(currentPos, out Node currentNode) 
                    && CanMove(node.Position, dir))
                {
                    node.Links.Add(new Link(node.Position, currentPos));
                }
            }
        }
        
        private bool CanMove(Vector2Int startPos, Vector2Int dir)
        {
            return Map.ContainsKey(startPos + new Vector2Int(dir.x,0))
                && Map.ContainsKey(startPos + new Vector2Int(0,dir.y));
        }

        public Vector2 TileToWorld(Vector2Int tilePos) =>
            _origin + tilePos * _cellSize + _cellSize / 2;

        public Vector2Int WorldToTile(Vector2 pos)
        {
            Vector2 local = pos - _origin;
            return new Vector2Int(
                Mathf.FloorToInt(local.x / _cellSize.x),
                Mathf.FloorToInt(local.y / _cellSize.y));
        }

    }
}