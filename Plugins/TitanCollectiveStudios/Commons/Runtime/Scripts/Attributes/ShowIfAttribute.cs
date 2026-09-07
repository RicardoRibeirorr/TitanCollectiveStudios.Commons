using UnityEngine;

namespace TitanCollectiveStudios.Commons.Inspectors
{
    public class ShowIfAttribute : PropertyAttribute
    {
        public string ConditionalField;
        public object ExpectedValue;

        /// <summary>
        /// Shows a serialized property if it matches the condition. We advice the usage of "nameOf(yourVariable)" 
        /// if you rename the variable later, the compiler catches it.
        /// ```
        ///     [SerializeField] private bool useManualVariables;
        ///     
        ///     [ShowIf(nameof(useManualVariables))] //if boolean no need for setup the condition
        ///     [SerializeField] private float _speed
        ///     
        ///     //OR
        /// 
        ///     [ShowIf(useManualVariables,true)]
        ///     [SerializeField] private float _speed
        /// ```
        /// </summary>
        /// <param name="conditionalField"></param>
        /// <param name="showValue"></param>

        public ShowIfAttribute(string conditionalField, object expectedValue)
        {
            ConditionalField = conditionalField;
            ExpectedValue = expectedValue;
        }
        public ShowIfAttribute(string conditionalField)
        {
            ConditionalField = conditionalField;
            ExpectedValue = true;
        }
    }
}