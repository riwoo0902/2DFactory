using UnityEngine;

namespace _Script._AstarSystem
{
    public static class VecGroup
    {
        public static readonly Vector2Int[] IntDir8 =
        {
            new(-1,-1), new(-1,0), new(-1,1),
            new(0,-1),  new(0,1),
            new(1,-1),  new(1,0),  new(1,1),
        };
        public static readonly Vector2Int[] CenterUp =
        {
            new(0,0), new(0,1)
        };
    }
}