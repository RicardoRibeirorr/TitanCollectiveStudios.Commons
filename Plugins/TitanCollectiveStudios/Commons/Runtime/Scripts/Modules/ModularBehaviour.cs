/**
 * THANK YOU UNITY TEAM!
 * 
 * This was very much inspired by the Cinemachine system ;)
 */

using System;
using System.Collections.Generic;
using TitanCollectiveStudios.Commons.Inspectors;
using TitanCollectiveStudios.Commons.Packages;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Assertions;

namespace TitanCollectiveStudios.Commons.Modules
{

    /// <summary>
    /// Base class for a Monobehaviour that represents a modular system to be extended by an agent.
    ///
    /// This is intended to be extended by an agent (character, car, automata, torrent)
    ///
    /// A ModularBehaviour handles the pipeline of Modules. In it self does not add much, but it handles how
    /// modules can behave, how agents handle them, and a way of avoiding a bunch of code that will be discarted.
    /// 
    /// It gives agents a way of handling very strict peaces of code (aka modules) making it super reusable.
    /// 
    /// @Usage:
    /// ```
    /// class CharacterBlackBoard{ 
    ///  public string CharacterName; //... other properties
    /// }
    /// 
    /// class Character extends ModularBehaviour<MyBlackboard>{
    ///     CombatModule combatModule {get;} //this is not required but it's good to have these references for state machines etc
    ///     
    ///     protected override Awake(){
    ///         combatModule = this.GetModule<combatModule>();    
    ///     }    
    /// }
    /// ```
    /// 
    /// You can also setup without a blackboard
    /// ```
    /// class Character extends ModularBehaviour{
    ///     CombatModule combatModule {get;} //this is not required but it's good to have these references for state machines etc
    ///     
    ///     protected override Awake(){
    ///         combatModule = this.GetModule<combatModule>();    
    ///     }    
    /// }
    /// ```
    /// </summary>
    /// 
    [DisallowMultipleComponent]
    [ColoredComponent(DefaultColor.Red)]
    public abstract class ModularBehaviour<TBlackboard> : ModularBehaviour
    where TBlackboard : iAgentBlackbox
    {
        //protected abstract TBlackboard Blackboard { get; set; }
        //public override iBlackboard ModularBlackboard => Blackboard;
        //public TBlackboard TypedBlackboard => Blackboard;
    }

    [DisallowMultipleComponent]
    [ColoredComponent(DefaultColor.Red)]
    public abstract class ModularBehaviour : MonoBehaviour
    {
        protected List<Module> _modules = new();

        /// <summary> Setups a blackboard that should be replaced by the agent, containing
        /// all information to be shared among the modules. This includes values, components, etc.
        /// 
        /// Create your blackboard class and implement the interfaces that the modules require.
        /// 
        /// @Usage:
        /// class CharacterBlackboard:iBlackboard, //required
        ///                           iMovementBlackboard, //module specific
        ///                           iCombatBlackboard //module specific
        /// {
        ///    //...implement the interfaces properties here
        /// }
        /// 
        /// class CharacterAgent : ModularBehaviour{
        ///  [SerializeField] CharacterBlackboard _blackbox;
        ///  
        ///  public override CharacterBlackboard Blackboard => _blackbox;
        /// }
        /// </summary>
        //public abstract iBlackboard ModularBlackboard { get; }

        /// <summary> The modules connected to this modular behaviour</summary>
        protected List<Module> Modules => _modules;

        /// <summary>
        /// A delegate to hook into the state calculation pipeline.
        /// This will be called after each pipeline stage, to allow others to hook into the pipeline.
        /// </summary>
        internal void AddModule(Module module)
        {
            RemoveNulls();
            Assert.IsNotNull(module);

            if (_modules.Contains(module))
                return;

            if (_modules.Exists(m => m.GetType() == module.GetType()))
                throw new Exception($"Module '{module.GetType().Name}' already exists. Duplicates are not allowed.");

            _modules.Add(module);
        }


        /// <summary>Remove a Pipeline stage hook callback.</summary>
        /// <param name="extension">The extension to remove.</param>
        internal void RemoveModule(Module module)
        {
            RemoveNulls();
            _modules.Remove(module);
        }


        /// <summary>
        /// Return a specific module from the pipeline Pipeline stage hook callback.
        /// 
        /// This is very usefull for reusable agents, if you have multiple types of Modules 
        /// that extend the same module, you can either get your specific module or search by
        /// the base.
        /// 
        /// @Example: 
        /// ```csharp
        /// CombatModule2D extends CombatModule {}
        /// MountedModule3D extends CombatModule {}
        /// ```
        /// 
        /// In this case your can:
        /// ```
        /// this.GetModule<CombatModule>(); //it will give you the one you have attached (CombatModule2D; MountedModule3D; or CombatModule)
        /// this.GetModule<CombatModule2D>(); //it will give you the right module type you mentioned
        /// ```
        /// 
        /// </summary>
        /// <param name="TModule">The module class type.</param>
        public TModule GetModule<TModule>() where TModule : Module
        {
            foreach (var module in _modules)
            {
                if (module is TModule typed)
                    return typed;
            }

            throw new Exception(
                $"Module '{typeof(TModule).Name}' was not found on '{name}'."
            );
        }



        /// <summary>Enables all modules in pipeline.</summary>
        protected virtual void EnableModules()
        {
            foreach (var module in _modules)
                module.enabled = true;
        }


        /// <summary>Disables all modules in pipeline.</summary>
        protected virtual void DisableModules()
        {
            foreach (var module in _modules)
                module.enabled = false;
        }


        /// <summary>Base class implementation makes sure the queue remains up-to-date.</summary>
        protected virtual void OnEnable()
        {
            RemoveNulls();
        }

        /// <summary>Base class implementation makes sure the queue remains up-to-date.</summary>
        protected virtual void OnDisable()
        {
            RemoveNulls();
        }


        /// <summary>Cleans up all the messy extensions left from manually removing modules.</summary>
        internal void RemoveNulls()
        {
            _modules.RemoveAll(i => i == null);
        }
    }
}