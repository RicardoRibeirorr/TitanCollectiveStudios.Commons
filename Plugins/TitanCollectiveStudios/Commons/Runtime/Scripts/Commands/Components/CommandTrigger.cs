using UnityEngine;

using MackySoft.SerializeReferenceExtensions;
using TitanCollectiveStudios.Commons.Conditions;
using TitanCollectiveStudios.Commons.Packages;
using System.Collections.Generic;

namespace TitanCollectiveStudios.Commons.Commands
{
    public class CommandTrigger : MonoBehaviour
    {
        public enum EActivationEvent
        {
            OnStart,
            OnEnable,
            OnDisable,
            OnFixedUpdate,
            OnUpdate,
            OnAgentEnterTrigger,
            OnAgentExitTrigger,
            OnAgentCollision,
            OnAgentInteract,
            OnConditionStateChanged
        }

        [Header("Requirements")]
        [SerializeField] protected EActivationEvent m_activationEvent;
        [SerializeReference, SubclassSelector] protected iCondition m_condition;

        [Header("Actions")]
        [SerializeReference, SubclassSelector] protected iCommand m_toExecute;

        [Header("Settings")]
        [SerializeField] protected bool m_playerOnly = true;
        [SerializeField] protected List<string> m_agentFlags = new List<string>();

        protected virtual void OnEnable()
        {
            AttemptExecution(EActivationEvent.OnEnable);

            if (m_activationEvent == EActivationEvent.OnConditionStateChanged)
            {
                m_condition.StartListening(OnConditionStateChanged);
            }
        }

        protected virtual void OnDisable()
        {
            AttemptExecution(EActivationEvent.OnDisable);

            if (m_activationEvent == EActivationEvent.OnConditionStateChanged)
            {
                m_condition.StopListening();
            }
        }

        protected virtual void OnConditionStateChanged() => AttemptExecution(EActivationEvent.OnConditionStateChanged);

        protected virtual void AttemptExecution(EActivationEvent currentEvent, GameObject go = null)
        {
            if (currentEvent != m_activationEvent) return;
           Execute();
        }

        protected virtual void AttemptTriggerExecution(EActivationEvent currentEvent, Collider collider = null)
        {
            if (collider != null && collider.gameObject != null && collider.TryGetComponent<iAgent>(out var agent))
            {
                if (m_playerOnly && agent.IsPlayer == false) return;
                if (m_agentFlags.Count > 0 && HasFlags(agent.Flags) == false) return;

                //Execute
                AttemptExecution(currentEvent, collider.gameObject);
            }

        }

        protected virtual void Execute()
        {
            m_toExecute?.Execute();
        }

        protected virtual void Start() => AttemptExecution(EActivationEvent.OnStart);
        protected virtual void FixedUpdate() => AttemptExecution(EActivationEvent.OnFixedUpdate);
        protected virtual void Update() => AttemptExecution(EActivationEvent.OnUpdate);
        protected virtual void OnTriggerEnter(Collider collider) => AttemptTriggerExecution(EActivationEvent.OnAgentEnterTrigger, collider);
        protected virtual void OnTriggerExit(Collider collider) => AttemptTriggerExecution(EActivationEvent.OnAgentExitTrigger, collider);



        protected bool HasFlags(List<string> flags)
        {
            foreach (var item in flags)
            {
                if (m_agentFlags.Contains(item) == false) return false;
            }

            return true;
        }
    }
}