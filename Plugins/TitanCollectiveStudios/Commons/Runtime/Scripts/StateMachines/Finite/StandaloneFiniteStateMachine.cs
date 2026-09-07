using System;
using TitanCollectiveStudios.Commons.Core;

namespace TitanCollectiveStudios.Commons.StateMachines.Finite
{
    public abstract class StandaloneFiniteStateMachine<K, J> : Standalone<K>
        where K : StandaloneFiniteStateMachine<K, J>
        where J : _IFiniteMachineState
    {
        private readonly AFiniteStateMachine<J> core = new AFiniteStateMachine<J>();

        public J CurrentState => core.CurrentState;
        public J PreviousState => core.PreviousState;

        public virtual void Register<T>(J instance) where T : J => core.Register<T>(instance);
        public virtual void Change<T>() where T : J => core.Change<T>();
        public virtual bool IsIn<T>() where T : J => core.IsIn<T>();

        protected virtual void Start() { }
        protected virtual void Update()
        {
            core.Update(); // or pass dt if you extend it
        }

        public virtual void ForceChange(Type t) => core.ForceChange(t);
    }
}