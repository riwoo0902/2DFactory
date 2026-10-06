using System.Collections.Generic;
using UnityEngine;

namespace _Script._2DPlatformAstar.Data.MapData
{
    public readonly struct Node
    {
        public readonly Vector2Int Position;
        public readonly List<Link> Links;

        public Node(Vector2Int position)
        {
            Position = position;
            Links = new List<Link>(8);
        }
        
    }
}