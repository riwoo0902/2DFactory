using UnityEngine;

namespace _Script._2DPlatformAstar.Data
{
    public static class DirData
    {
        public static readonly Vector2Int[] Dirs =
        {
            new(-1,1),  new(0,1), new(1,1),
            new(-1,0),            new(1,0),
            new(-1,-1), new(0,-1),new(1,-1),
        };
    }
}