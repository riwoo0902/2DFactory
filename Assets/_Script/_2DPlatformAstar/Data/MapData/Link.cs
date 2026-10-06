using UnityEngine;

namespace _Script._2DPlatformAstar.Data.MapData
{
    public readonly struct Link
    {
        public readonly Vector2Int Start;
        public readonly Vector2Int Target;
        public readonly float Distance;

        public Link(Vector2Int start, Vector2Int target)
        {
            Start = start;
            Target = target;
            Distance = Vector2Int.Distance(start, target);
        }
    }
}