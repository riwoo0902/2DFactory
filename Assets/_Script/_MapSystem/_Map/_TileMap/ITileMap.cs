using _Script._MapSystem._Map._Tile;
using UnityEngine;

namespace _Script._MapSystem._Map._TileMap
{
    public interface ITileMap
    {
        bool TryGetTile(Vector3Int position, out AbstractTile tile);
        bool HasTile(Vector3Int position);
        void SetTile(Vector3Int position, AbstractTile tile);
        void Clear();
    }
}