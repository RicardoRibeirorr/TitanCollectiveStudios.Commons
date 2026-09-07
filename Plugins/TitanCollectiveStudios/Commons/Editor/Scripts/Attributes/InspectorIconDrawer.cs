#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace TitanCollectiveStudios.Commons.Inspectors.Editor
{
    [CustomPropertyDrawer(typeof(InspectorIconAttribute))]
    public class InspectorIconDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var iconAttribute = (InspectorIconAttribute)attribute;

            GUIContent newLabel = new GUIContent(label);

            GUIContent icon = EditorGUIUtility.IconContent(iconAttribute.IconName);
            newLabel.image = icon.image;

            EditorGUI.PropertyField(position, property, newLabel, true);
        }
    }
}
#endif