
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace TitanCollectiveStudios.Commons.Inspectors.Editor
{
    [CustomPropertyDrawer(typeof(InspectorNoteAttribute))]
    public class InspectorNoteDrawer : PropertyDrawer
    {
        private const float Spacing = 4f;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            InspectorNoteAttribute note = attribute as InspectorNoteAttribute;

            float noteHeight = EditorGUI.GetPropertyHeight(
                property,
                label,
                true
            );

            GUIStyle style = EditorStyles.helpBox;
            style.wordWrap = true;

            noteHeight += style.CalcHeight(
                new GUIContent(note.Message),
                EditorGUIUtility.currentViewWidth
            );

            return noteHeight + Spacing;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            InspectorNoteAttribute note = attribute as InspectorNoteAttribute;

            GUIStyle style = EditorStyles.helpBox;
            style.wordWrap = true;

            float height = style.CalcHeight(
                new GUIContent(note.Message),
                position.width
            );

            Rect noteRect = new Rect(
                position.x,
                position.y,
                position.width,
                height
            );

            EditorGUI.HelpBox(
                noteRect,
                note.Message,
                MessageType.Info
            );

            Rect propertyRect = new Rect(
                position.x,
                position.y + height + Spacing,
                position.width,
                EditorGUI.GetPropertyHeight(property, label, true)
            );

            EditorGUI.PropertyField(
                propertyRect,
                property,
                label,
                true
            );
        }
    }
}
#endif
