using Script._Core.ModuleSystem;
using UnityEngine;

namespace Script._Modules._FSMModule
{
    public abstract class AbstractMonoState : MonoBehaviour,IMonoState
    {
        protected ModuleOwner Owner;
        public virtual void Init(ModuleOwner owner)
        {
            Owner = owner;
        }

        public abstract void Enter();

        public abstract void StateUpdate();

        public abstract void StateFixedUpdate();

        public abstract void Exit();


    }
}