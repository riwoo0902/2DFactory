using System;
using UnityEngine;

namespace _Script._MapSystem._Map._TileMap
{
    public interface IFlush
    {
        void Flush();
        event Action<Vector3Int[]> OnFlush;
    }
}