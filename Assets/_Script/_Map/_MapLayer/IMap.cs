using _Script._Map._MapLayer._Layer;

namespace _Script._Map._MapLayer
{
    public interface IMap
    {
        IMapLayer GetLayer(LayerType layerType);
    }
}