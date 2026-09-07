/**
 * THANK YOU UNITY TEAM!
 * 
 * This was very much inspired by the Cinemachine system ;)
 */


using TitanCollectiveStudios.Commons.Inspectors;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace TitanCollectiveStudios.Commons.Modules
{

    /// <summary>
    /// Base class for a Module extension module. Module usually do nothing unless asked for,
    /// and it's usually the task of StateMachine or the agent to handle it.
    /// 
    /// Hooks into the ModularBehaviour pipeline, extended by the Agent (car, character, automata).  
    /// Use this to add extra extensions to your games, creating super modular and reusable code.
    /// 
    /// IMPORTANT DESIGN: Modules depende on NOTHING but the agent Blackboard, agent and unity standard components.
    /// It is very rare (but not impossible) to have modules that required others, and in this case use 
    /// the annotation ````[RequireComponent(typeOf(MyOtherModule))]```
    /// 
    /// @Usage:
    /// ```csharp
    /// class MovementModule extends Module{
    ///     void Move(){ /* do something with agent.blackboard.move */ }
    /// }
    /// 
    /// //Then in the state machine
    /// class MovementState extends AgentState{
    ///     public void OnUpdate(){
    ///         agent.movementModule.Move();
    ///     }
    /// }
    /// ```
    /// 
    /// Make use of annotation ```[DisallowMultipleComponent]``` if your module is unique.
    /// </summary>

    [ColoredComponent(DefaultColor.Green)]
    public abstract class Module : MonoBehaviour
    {
        private ModularBehaviour _owner;


        /// <summary>Get the ModuleBehaviour to which this module extension is attached.</summary>
        protected ModularBehaviour ComponentOwner
        {
            get
            {
                if (_owner == null)
                    TryGetComponent(out _owner);

                return _owner;
            }
        }


#if UNITY_EDITOR

        [DidReloadScripts]
        private static void OnScriptReload()
        {
            var modules = Resources.FindObjectsOfTypeAll<Module>();

            System.Array.Sort(
                modules,
                (x, y) =>
                    MonoImporter.GetExecutionOrder(MonoScript.FromMonoBehaviour(y)) -
                    MonoImporter.GetExecutionOrder(MonoScript.FromMonoBehaviour(x))
            );

            foreach (var module in modules)
            {
                if (module.isActiveAndEnabled)
                    module.ConnectToModular(true);
            }
        }
#endif

        /// <summary>Connect to ModularBehaviour pipeline.
        /// Override implementations must call this base implementation</summary>
        protected virtual void Awake() => ConnectToModular(true);

        /// <summary>Disconnect from ModularBehaviour pipeline.
        /// Override implementations must call this base implementation</summary>
        protected virtual void OnDestroy() => ConnectToModular(false);

        /// <summary>Does nothing.  It's here for the little checkbox in the inspector.</summary>
        protected virtual void OnEnable() { }


        protected virtual void RefreshConnection() { }

        /// <summary>Connect to ModularBehaviour.  Implementation must be safe to be called
        /// redundantly.  Override implementations must call this base implementation</summary>
        /// <param name="connect">True if connecting, false if disconnecting</param>
        protected virtual void ConnectToModular(bool connect)
        {
            var owner = ComponentOwner;

            if (owner == null)
            {
                Debug.LogWarning(
                    $"Module '{GetType().Name}' requires a ModularBehaviour on '{gameObject.name}'. Removing module.",
                    this
                );

                Destroy(this);
                return;
            }

            if (connect)
            {
                if (!ValidateRequirements())
                {
                    //Debug.LogError(
                    //    $"Module '{GetType().Name}' requirements not met. Removing module.",
                    //    this
                    //);

                    //DestroyImmediate((MonoBehaviour)this); //cast  type so that it does not tried to remove gameobject
                    return;
                }

                owner.AddModule(this);
            }
            else
            {
                owner.RemoveModule(this);
            }
        }

        protected virtual bool ValidateRequirements(){return true;}
    }

    public abstract class Module<TRequirement> : Module
    where TRequirement : class
    {
        protected TRequirement Blackboard { get; private set; }


        protected override bool ValidateRequirements()
        {
            if (Blackboard != null) return true;

            Blackboard = GetComponent<TRequirement>();

            if (Blackboard == null)
            {
                Debug.LogError(
                    $"Module '{GetType().Name}' requirements a blackboard that extends '{typeof(TRequirement).Name}'.",
                    this
                );
                return false;
            }

            return true;
        }
    }
}
