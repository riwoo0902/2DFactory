using System.Collections.Generic;
using UnityEngine;

namespace _Script._AstarSystem.BakeSystem
{
    public class Node
    {
        public readonly Vector2Int SelfPos;
        public readonly List<Vector2Int> Links = new();
        
        public Node(Vector2Int selfPos)
        {
            SelfPos = selfPos;
        }
        
    }
}