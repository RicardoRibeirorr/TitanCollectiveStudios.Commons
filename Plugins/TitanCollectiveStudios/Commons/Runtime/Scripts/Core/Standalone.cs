using UnityEngine;


namespace TitanCollectiveStudios.Commons.Core
{
    public abstract class Standalone<T> : MonoBehaviour where T : Standalone<T>
    {
        public static T i { get; private set; }
        public static T Instance => i;

        protected virtual void Awake()
        {
            if (i != null && i != this)
            {
                Destroy(gameObject);
                return;
            }

            i = (T)this;
            //DontDestroyOnLoad(gameObject);
        }

        protected virtual void OnDestroy()
        {
            if (i == this)
                i = null;
        }
    }
}