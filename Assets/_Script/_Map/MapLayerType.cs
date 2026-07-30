namespace _Script._Map
{
    public static class MapLayer
    {
        public static readonly MapLayerType[] Layers =
        {
            MapLayerType.Biome, MapLayerType.Tile, MapLayerType.Structure, MapLayerType.LiquidPipe,
            MapLayerType.GasPipe, MapLayerType.Wire
        };
    }
    public enum MapLayerType
    {
        Biome,Tile,Structure,LiquidPipe,GasPipe,Wire
    }
}