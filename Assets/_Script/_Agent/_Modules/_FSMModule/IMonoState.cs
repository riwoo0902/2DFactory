using _Script._Core._FSM;
using _Script._Core.ModuleSystem;

namespace _Script._Agent._Modules._FSMModule
{
    public interface IMonoState :  IState
    {
        void Init(ModuleOwner owner);
    }
}