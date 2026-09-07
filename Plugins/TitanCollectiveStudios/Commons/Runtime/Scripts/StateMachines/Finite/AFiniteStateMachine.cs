using System;
using System.Collections.Generic;


namespace TitanCollectiveStudios.Commons.StateMachines.Finite
{
    public interface _IFiniteMachineState
    {
        void OnEnter();
        void OnUpdate();
        void OnExit();
    }

    public class AFiniteStateMachine<J> where J : _IFiniteMachineState
    {
        public J CurrentState { get; private set; }
        public J PreviousState { get; private set; }

        private readonly Dictionary<Type, J> states = new();

        public virtual void Register<T>(J instance) where T : J
        {
            states[typeof(T)] = instance;
        }

        public virtual void Change<T>() where T : J
        {
            if (CurrentState is T)
                return;

            if (!states.TryGetValue(typeof(T), out var newState))
                throw new KeyNotFoundException($"State {typeof(T)} not registered.");

            PreviousState = CurrentState;
            CurrentState?.OnExit();
            CurrentState = newState;
            CurrentState.OnEnter();
        }

        public virtual bool IsIn<T>() where T : J
            => CurrentState is T;

        public virtual void Update()
        {
            CurrentState?.OnUpdate();
        }

        public virtual void ForceChange(Type stateType)
        {
            if (!states.TryGetValue(stateType, out var newState))
                throw new KeyNotFoundException($"State {stateType} not registered.");

            PreviousState = CurrentState;
            CurrentState?.OnExit();
            CurrentState = newState;
            CurrentState.OnEnter();
        }
    }
}