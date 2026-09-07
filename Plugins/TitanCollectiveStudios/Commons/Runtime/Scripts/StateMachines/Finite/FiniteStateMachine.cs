using System;
using UnityEngine;

namespace TitanCollectiveStudios.Commons.StateMachines.Finite
{
    public class FiniteStateMachine<J> : MonoBehaviour where J : _IFiniteMachineState
    {
        private AFiniteStateMachine<J> core = new AFiniteStateMachine<J>();
        public J CurrentState => core.CurrentState;
        public J PreviousState => core.PreviousState;
        public virtual void Register<T>(J instance) where T : J => core.Register<T>(instance);
        public virtual void Change<T>() where T : J => core.Change<T>();
        public virtual bool IsIn<T>() where T : J => core.IsIn<T>();

        protected virtual void Start() { }
        protected virtual void Update()
        {
            core?.Update();
        }

        public virtual void ForceChange(Type t) => core.ForceChange(t);
    }
    //public class StateMachine<J> : MonoBehaviour where J : _IFiniteMachineState
    //{

    //    public event Action<StateMachine<J>> OnChanged;

    //    public J CurrentState { get; private set; }
    //    public J PreviousState { get; private set; }

    //    private readonly Dictionary<Type, J> states = new();

    //    public virtual void Register<T>(J instance) where T : J
    //    {
    //        states[typeof(T)] = instance;
    //    }

    //    public virtual void Change<T>() where T : J
    //    {
    //        if (CurrentState is T)
    //            return;

    //        if (!states.TryGetValue(typeof(T), out var newState))
    //            throw new KeyNotFoundException($"State {typeof(T)} not registered.");

    //        PreviousState = CurrentState;
    //        CurrentState?.OnExit();
    //        CurrentState = newState;
    //        CurrentState.OnEnter();
    //        OnChanged?.Invoke(this);
    //    }

    //    public virtual bool IsIn<T>() where T : J => CurrentState is T;

    //    public virtual void Update()
    //    {
    //        CurrentState?.OnUpdate();
    //    }

    //    // For multiplayer (external force from server)
    //    public virtual void ForceChange(Type stateType)
    //    {
    //        if (!states.TryGetValue(stateType, out var newState))
    //            throw new KeyNotFoundException($"State {stateType} not registered.");

    //        PreviousState = CurrentState;
    //        CurrentState?.OnExit();
    //        CurrentState = newState;
    //        CurrentState.OnEnter();
    //        OnChanged?.Invoke(this);
    //    }
    //}
}