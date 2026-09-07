using System;
using MackySoft.SerializeReferenceExtensions;
using UnityEngine;

namespace TitanCollectiveStudios.Commons.Conditions
{
    public enum EGameConditionOperation
    {
        All,
        Any
    }


    [AddTypeMenu("Base/Are Conditions Met")]
    [Serializable]
    public class AreConditionMet : ABaseCondition
    {
        [SerializeField]
        private EGameConditionOperation m_operator = EGameConditionOperation.All;

        [SerializeReference, SubclassSelector]
        private iCondition[] m_conditions = null;

        public override bool Evaluate()
        {
            switch (m_operator)
            {
                case EGameConditionOperation.All: return CheckAnd();
                case EGameConditionOperation.Any: return CheckOr();
            }

            return false;
        }

        private bool CheckAnd()
        {
            foreach (iCondition condition in m_conditions)
            {
                if (!(condition?.Evaluate() ?? true))
                {
                    return false;
                }
            }

            return true;
        }

        private bool CheckOr()
        {
            foreach (iCondition condition in m_conditions)
            {
                if (condition?.Evaluate() ?? true)
                {
                    return true;
                }
            }

            return false;
        }

        public override void StartListening(Action action)
        {
            base.StartListening(action);

            foreach (iCondition condition in m_conditions)
            {
                condition?.StartListening(action);
            }
        }

        public override void StopListening()
        {
            base.StopListening();

            foreach (iCondition condition in m_conditions)
            {
                condition?.StopListening();
            }
        }
    }
}
