

namespace TitanCollectiveStudios.Commons.Inspectors.Editor
{
#if UNITY_EDITOR
    using UnityEditor;
    using UnityEngine;

    [InitializeOnLoad]
    public static class BindProcessor
    {
        static BindProcessor()
        {
            Selection.selectionChanged += BindSelected;
        }

        private static void BindSelected()
        {
            foreach (GameObject go in Selection.gameObjects)
            {
                var components = go.GetComponents<MonoBehaviour>();

                foreach (var component in components)
                {
                    if (component != null)
                        BindMethods.Bind(component);
                }
            }
        }
    }

#endif
}
