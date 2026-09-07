using System;
using TitanCollectiveStudios.Commons.Inspectors;
using TitanCollectiveStudios.Commons.StateMachines.Finite;
using UnityEngine;


namespace TitanCollectiveStudios.Commons.Modules
{
    /// <summary>
    /// Base module that provides finite state machine functionality.
    /// Handles state transitions, current state tracking, and state queries.
    /// </summary>
    public abstract class BaseFSMModule : Module
    {
        [SerializeField, Disabled] protected string CurrentStateName;

        protected AFiniteStateMachine<_IFiniteMachineState> core = new AFiniteStateMachine<_IFiniteMachineState>();

        /// <summary>
        /// Gets the currently active state in the finite state machine.
        /// </summary>
        public _IFiniteMachineState CurrentState => core.CurrentState;

        /// <summary>
        /// Gets the previously active state before the current transition.
        /// </summary>
        public _IFiniteMachineState PreviousState => core.PreviousState;

        /// <summary>
        /// Determines whether the finite state machine is currently executing a specific state type.
        /// </summary>
        /// <typeparam name="T">The state type to check.</typeparam>
        /// <returns>True if the current state is of the specified type; otherwise false.</returns>
        public virtual bool IsIn<T>() where T : _IFiniteMachineState => core.IsIn<T>();

        /// <summary>
        /// Changes the current state to the specified state type.
        /// The transition is handled by the finite state machine.
        /// </summary>
        /// <typeparam name="T">The target state type.</typeparam>
        public virtual void Change<T>() where T : _IFiniteMachineState
        {
            core.Change<T>();
            CurrentStateName = core.CurrentState.GetType().Name;
        }

        /// <summary>
        /// Forces a state change using the provided state type.
        /// This bypasses generic type inference and allows runtime state selection.
        /// </summary>
        /// <param name="stateType">The target state type.</param>
        public virtual void ForceChange(Type stateType)
        {
            core.ForceChange(stateType);
            CurrentStateName = core.CurrentState.GetType().Name;
        }
    }
}
