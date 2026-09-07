using System;
using UnityEditor;
using UnityEngine;


namespace TitanCollectiveStudios.Commons.Inspectors.Editor
{
    [CustomPropertyDrawer(typeof(ShowIfAttribute))]
    public class ShowIfDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            ShowIfAttribute showIf = (ShowIfAttribute)attribute;

            if (!ShouldShow(property, showIf))
                return 0;

            return EditorGUI.GetPropertyHeight(property, label);
        }


        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ShowIfAttribute showIf = (ShowIfAttribute)attribute;

            if (!ShouldShow(property, showIf))
                return;

            EditorGUI.PropertyField(position, property, label, true);
        }


        protected virtual bool ShouldShow(SerializedProperty property, ShowIfAttribute attribute)
        {
            SerializedProperty condition =
                property.serializedObject.FindProperty(attribute.ConditionalField);


            if (condition == null)
            {
                Debug.LogWarning(
                    $"ShowIf: Could not find field '{attribute.ConditionalField}'"
                );

                return false;
            }


            return Evaluate(condition, attribute.ExpectedValue);
        }


        protected virtual bool Evaluate(SerializedProperty property, object expected)
        {
            switch (property.propertyType)
            {
                case SerializedPropertyType.Boolean:
                    return property.boolValue.Equals(expected);


                case SerializedPropertyType.Enum:
                    if (expected is Enum enumValue)
                    {
                        return property.enumValueIndex ==
                            Convert.ToInt32(enumValue);
                    }

                    return false;


                case SerializedPropertyType.String:
                    string value = property.stringValue;

                    if (expected == null)
                        return string.IsNullOrEmpty(value);

                    return value == expected.ToString();


                case SerializedPropertyType.Integer:
                    return property.intValue.Equals(Convert.ToInt32(expected));


                case SerializedPropertyType.Float:
                    return Mathf.Approximately(
                        property.floatValue,
                        Convert.ToSingle(expected)
                    );


                case SerializedPropertyType.ObjectReference:
                    return property.objectReferenceValue != null;


                default:
                    Debug.LogWarning(
                        $"ShowIf does not support {property.propertyType}"
                    );

                    return false;
            }
        }
    }
}