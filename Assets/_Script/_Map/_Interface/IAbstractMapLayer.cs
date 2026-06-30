using _Script._Core._Loading;

namespace _Script._Map._Interface
{
    public interface IAbstractMapLayer : ILoading
    {
        LayerType LayerType { get; }
        void Initialize();
    }
}