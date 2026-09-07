using UnityEngine;

namespace TitanCollectiveStudios.Commons.Inspectors
{
    public class HideIfAttribute : ShowIfAttribute
    {
        public HideIfAttribute(string conditionalField, object expectedValue) : base(conditionalField, expectedValue)
        {
        }
    }
}