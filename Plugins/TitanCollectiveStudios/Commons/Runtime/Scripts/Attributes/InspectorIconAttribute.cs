using UnityEngine;

namespace TitanCollectiveStudios.Commons.Inspectors
{
    public class InspectorIconAttribute : PropertyAttribute
    {
        public readonly string IconName;

        /// <summary>
        /// Use "UnityIcons" with prebuilded icons.
        /// 
        /// @Usage: UnityIcons.Help
        /// 
        /// </summary>
        /// <param name="iconName">Use UnityIcons static class to get a icon. Ex: UnityIcons.Help</param>
        public InspectorIconAttribute(string iconName)
        {
            IconName = iconName;
        }
    }
}