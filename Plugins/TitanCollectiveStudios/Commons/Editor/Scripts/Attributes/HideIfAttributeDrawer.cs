using UnityEngine;

using UnityEditor;


namespace TitanCollectiveStudios.Commons.Inspectors.Editor
{
    [CustomPropertyDrawer(typeof(HideIfAttribute))]
    public class HideIfDrawer : ShowIfDrawer
    {
        /// <summary>
        /// Invert the show condition
        /// 
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        protected override bool ShouldShow(SerializedProperty property, ShowIfAttribute attribute)
        {
            return !base.ShouldShow(property, attribute);
        }
    }
}