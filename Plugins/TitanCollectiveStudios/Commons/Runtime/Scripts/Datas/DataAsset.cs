using TitanCollectiveStudios.Commons.Inspectors;
using UnityEditor;
using UnityEngine;

namespace TitanCollectiveStudios.Commons.Datas
{
    public abstract class DataAsset : ScriptableObject
    {
        [SerializeField, Disabled]
        private string id;

        public string Id => id;

#if UNITY_EDITOR
        protected virtual void OnValidate()
        {
            if (!string.IsNullOrEmpty(id))
                return;

            id = System.Guid.NewGuid().ToString("N");

            EditorUtility.SetDirty(this);
        }
#endif
    }
}