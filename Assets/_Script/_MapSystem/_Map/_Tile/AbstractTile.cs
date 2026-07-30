using System;
using UnityEngine;

namespace _Script._MapSystem._Map._Tile
{
    public abstract class AbstractTile
    {
        public TileData TileData { get; private set; }

        public AbstractTile(TileData tileData)
        {
            TileData = tileData;
        }
        
    }
}