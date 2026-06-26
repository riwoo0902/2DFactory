using Script._Core._FSM;
using Script._Core.ModuleSystem;

namespace Script._Modules._FSMModule
{
    public interface IMonoState :  IState
    {
        void Init(ModuleOwner owner);
    }
}