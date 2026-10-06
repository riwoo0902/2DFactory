using _Script._2DPlatformAstar.Data.MapData;
using UnityEngine;

namespace _Script._2DPlatformAstar.Data.AstarData
{
    public readonly struct PathData
    {
        public readonly Vector2Int PrevPos;
        public readonly Vector2Int Pos;
        public readonly float G;
        public readonly float H;
        public readonly float Distance;
        public PathData(Vector2Int prevPos, Vector2Int pos, float g, float h)
        {
            PrevPos = prevPos;
            Pos = pos;
            G = g;
            H = h;
            Distance = G + H;
        }

    }
}