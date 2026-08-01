using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Script._MapSystem._Map._Tile
{
    [CreateAssetMenu(fileName = "TileData", menuName = "Map/TileData", order = 0)]
    public class TileData : ScriptableObject
    {
        [field: SerializeField] public TileBase Tile { get; private set; }
    }
}