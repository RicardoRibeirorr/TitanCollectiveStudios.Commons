using System.Collections.Generic;
using TitanCollectiveStudios.Commons.Agents.Modules;
using TitanCollectiveStudios.Commons.Inspectors;
using TitanCollectiveStudios.Commons.Modules;
using TitanCollectiveStudios.Commons.Packages;
using UnityEngine;
using UnityEngine.Assertions;



namespace TitanCollectiveStudios.Commons.Agents
{
    [RequireComponent(typeof(TaskRunnerModule))]
    [ColoredComponent(DefaultColor.Red)]
    public class AAgent : ModularBehaviour, iAgent
    {
        [SerializeField] protected bool m_isPlayer;
        [SerializeField] protected List<string> m_flags;

        /***************************************
         *        Agent Required Modules
         ***************************************/
        public TaskRunnerModule TaskRunner{ get; private set; }
        public bool IsPlayer { get => m_isPlayer; protected set => m_isPlayer = value; }
        public List<string> Flags { get => m_flags; protected set => m_flags = value; }

        /// <summary>
        /// Create a component that inherits "iAgentBlackbox", and the interfaces required by your modules.
        /// 
        /// @Example:
        /// ```
        /// public class AnimalBlackbox : MonoBehaviour, iAgentBlackbox { }
        /// 
        /// //For using movement module
        /// 
        /// public class AnimalBlackbox : MonoBehaviour, iAgentBlackbox, iBaseMovementBlackboard { .... }
        /// ```
        /// </summary>
        public iAgentBlackbox Blackbox { get; protected set; }

        protected void Awake()
        {
            this.TaskRunner = GetComponent<TaskRunnerModule>();
            this.Blackbox = GetComponent<iAgentBlackbox>();

            Assert.IsNotNull(TaskRunner, "Agent missing the TaskRunnerModule");

            Assert.IsNotNull(Blackbox, "Agent missing a Blackbox component attached. Ex: implementation: public class AnimalBlackbox : MonoBehaviour, iAgentBlackbox {} ");

            if (TaskRunner == null || Blackbox == null)
            {
                this.gameObject.SetActive(false);
                this.DisableModules();
            }
        }
    }
}
