namespace _Script._Core.ModuleSystem
{
    public interface IModule
    {
        void Initialize(ModuleOwner owner);
        void AfterInitialize() { }
    }
}