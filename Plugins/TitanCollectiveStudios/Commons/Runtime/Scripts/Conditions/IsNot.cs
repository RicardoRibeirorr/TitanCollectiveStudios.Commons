using System;
using MackySoft.SerializeReferenceExtensions;
using UnityEngine;

namespace TitanCollectiveStudios.Commons.Conditions
{

    [AddTypeMenu("Base/Is Not")]
    [Serializable]
    public class IsNot : ABaseCondition
    {
        [SerializeReference, SubclassSelector] private iCondition m_condition = null;

        public override bool Evaluate() => !m_condition?.Evaluate() ?? true;

        public override void StartListening(Action action)
        {
            base.StartListening(action);
            m_condition.StartListening(action);
        }

        public override void StopListening()
        {
            base.StopListening();
            m_condition.StopListening();
        }
    }
}
