using _Script._Core._Loading;

namespace _Script._Map
{
    public interface IMapLayer : ILoading
    {
        void Initialize();
        
        void CreateLayer(IMapLayer prevLayer = null);
    }
}