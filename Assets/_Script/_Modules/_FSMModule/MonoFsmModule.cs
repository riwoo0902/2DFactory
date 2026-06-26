using System;
using Script._Core._FSM;
using Script._Core.ModuleSystem;
using UnityEngine;

namespace Script._Modules._FSMModule
{
    public interface IMonoFsmModule
    {
        void ChangeState<T>() where T : IMonoState;
    }

    public class MonoFsmModule : MonoBehaviour,IModule, IMonoFsmModule
    { 
        [HideInInspector] 
        public string startStateName;
        
        private StateMachine<Type> _stateMachine;
        
        private ModuleOwner _owner;
        
        public void Initialize(ModuleOwner owner)
        {
            _owner = owner;
            
            _stateMachine = new();
            
            IMonoState[] monoStates = GetComponentsInChildren<IMonoState>(true);
            foreach (IMonoState monoState in monoStates)
            {
                Type stateType = monoState.GetType();
                _stateMachine.AddState(stateType,monoState);
            }
        }

        public void AfterInitialize()
        {
            foreach (var monoState in _stateMachine.GetStates())
            {
                if (monoState is IMonoState iMonoState)
                {
                    iMonoState.Init(_owner);
                }
            }
            
            if (string.IsNullOrEmpty(startStateName)) return;
            
            Type startType = ResolveStateType(startStateName);
            if(startType != null) ChangeState(startType);
            else Debug.LogWarning("Start state not found: " + startStateName);
        }

        private Type ResolveStateType(string stateName)
        {
            Type[] types = _stateMachine.GetKeys();

            foreach (Type stateType in types)
            {
                if (stateType.AssemblyQualifiedName == stateName ||
                    stateType.FullName == stateName ||
                    stateType.Name == stateName)
                {
                    return stateType;
                }
            }

            return null;
        }
        
        public void ChangeState<T>() where T : IMonoState => ChangeState(typeof(T));
        private void ChangeState(Type type) =>  _stateMachine.ChangeState(type);
        
        private void Update()
        {
            _stateMachine.Update();
        }

        private void FixedUpdate()
        {
            _stateMachine.FixedUpdate();
        }
        
    }
}
