namespace _Script._Map
{
    public interface IMapLayer
    {
        void Initialize();
        
        void CreateLayer(IMapLayer prevLayer = null);
    }
}