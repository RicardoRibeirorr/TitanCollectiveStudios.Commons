using System.Linq;
using TitanCollectiveStudios.Commons.Modules;
using UnityEditor;
using UnityEngine;

namespace TitanCollectiveStudios.Commons.Inspectors.Editor
{
    [CustomEditor(typeof(ModularBehaviour), true)]
    public class ModularBehaviourEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            GUILayout.Space(10);

            if (GUILayout.Button("Add Module"))
            {
                ShowMenu();
            }
        }

        void ShowMenu()
        {
            GenericMenu menu = new GenericMenu();

            var modular = (ModularBehaviour)target;

            var moduleTypes = TypeCache.GetTypesDerivedFrom<Module>()
                .Where(t =>
                    !t.IsAbstract &&
                    !t.IsGenericType &&
                    !t.ContainsGenericParameters);

            foreach (var type in moduleTypes)
            {
                bool alreadyExists = modular.GetComponent(type) != null;

                menu.AddItem(
                    new GUIContent(type.Name),
                    false,
                    () =>
                    {
                        Undo.AddComponent(modular.gameObject, type);
                    });
            }

            menu.ShowAsContext();
        }
    }
}