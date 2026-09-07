#if UNITY_EDITOR
using System.Linq;
using UnityEditor;
using UnityEngine;


/*******************************************************
 * 
 *  File:       DisabledAttributeDrawer.cs
 *  Description: Custom property drawer for the DisableAttribute, which disables the GUI for the decorated property in the Unity Inspector.
 *  
 *  Folder:     Assets/Scripts/Editor (REQUIRED TO BE IN AN "Editor" FOLDER)
 *  
 *  Author:     RicardoRibeiroRR
 *  
 *******************************************************/


namespace TitanCollectiveStudios.Commons.Inspectors.Editor { 

    [CustomEditor(typeof(MonoBehaviour), true)]
    [CanEditMultipleObjects]
    public class ColoredComponentAttributeEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var colorAttribute = target.GetType()
                .GetCustomAttributes(typeof(ColoredComponentAttribute), true)
                .FirstOrDefault() as ColoredComponentAttribute;

            if (colorAttribute != null)
            {
                var old = GUI.backgroundColor;
                GUI.backgroundColor = colorAttribute.Color;

                EditorGUILayout.BeginVertical("HelpBox");
                GUI.backgroundColor = old;

                DrawDefaultInspector();

                EditorGUILayout.EndVertical();
            }
            else
            {
                DrawDefaultInspector();
            }
        }
    }
}
#endif