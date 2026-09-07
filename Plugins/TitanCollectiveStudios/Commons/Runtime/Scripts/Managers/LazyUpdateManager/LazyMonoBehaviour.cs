using System;
using System.Collections.Generic;
using System.Reflection;
using TitanCollectiveStudios.Commons.Core;
using UnityEngine;

namespace TitanCollectiveStudios.GameManagers
{
    /// <summary>
    /// Base class for behaviours managed by a LazyUpdateManager.
    ///
    /// The generic manager type allows different systems to have their own update
    /// scheduler.
    ///
    /// Example:
    /// 
    /// public class Enemy : LazyMonoBehaviour<AIUpdateManager>
    /// {
    ///     private void LazyUpdate()
    ///     {
    ///     }
    /// }
    ///
    /// The class automatically detects optional callbacks:
    /// - LazyUpdate()
    /// - LazyLateUpdate()
    /// - LazyFixedUpdate()
    ///
    /// Missing callbacks are ignored.
    /// </summary>
    /// 

    public class LazyMonoBehaviour : LazyMonoBehaviour<LazyUpdateManager> { }
    public abstract class LazyMonoBehaviour<TManager> : MonoBehaviour, iLazyMonobehaviour 
        where TManager : ALazyUpdateManager<TManager>
    {
        protected TManager Manager => ALazyUpdateManager<TManager>.Instance;


        /// <summary>
        /// Cached delegate for the user's LazyUpdate method.
        /// Null when the derived class does not implement LazyUpdate.
        /// </summary>
        internal Action ManagedUpdate { get; private set; }
        Action iLazyMonobehaviour.ManagedUpdate => ManagedUpdate;


        /// <summary>
        /// Cached delegate for the user's LazyLateUpdate method.
        /// Null when the derived class does not implement LazyLateUpdate.
        /// </summary>
        internal Action ManagedLateUpdate { get; private set; }
        Action iLazyMonobehaviour.ManagedLateUpdate => ManagedLateUpdate;


        /// <summary>
        /// Cached delegate for the user's LazyFixedUpdate method.
        /// Null when the derived class does not implement LazyFixedUpdate.
        /// </summary>
        internal Action ManagedFixedUpdate { get; private set; }
        Action iLazyMonobehaviour.ManagedFixedUpdate => ManagedFixedUpdate;


        /// <summary>
        /// Stores discovered methods per component type.
        ///
        /// Reflection is expensive, so every class type is inspected only once.
        /// </summary>
        private static readonly Dictionary<Type, CachedMethods> s_cache = new();


        /// <summary>
        /// Stores the discovered optional update methods for a class type.
        /// </summary>
        private sealed class CachedMethods
        {
            public MethodInfo Update;
            public MethodInfo LateUpdate;
            public MethodInfo FixedUpdate;
        }


        /// <summary>
        /// Registers this object into its assigned manager.
        /// </summary>
        protected virtual void OnEnable()
        {
            CacheCallbacks();

            Manager.Register(this);
        }


        /// <summary>
        /// Removes this object from its assigned manager.
        /// </summary>
        protected virtual void OnDisable()
        {
            if (Manager != null)
                Manager.Unregister(this);
        }


        /// <summary>
        /// Finds optional update methods and creates delegates pointing to this instance.
        ///
        /// The reflection result is cached per type, but delegates are created per
        /// object because they need a target instance.
        /// </summary>
        private void CacheCallbacks()
        {
            Type type = GetType();


            if (!s_cache.TryGetValue(type, out CachedMethods cache))
            {
                cache = new CachedMethods
                {
                    Update = FindMethod(type, "LazyUpdate"),
                    LateUpdate = FindMethod(type, "LazyLateUpdate"),
                    FixedUpdate = FindMethod(type, "LazyFixedUpdate")
                };


                s_cache.Add(type, cache);
            }


            ManagedUpdate = cache.Update == null
                ? null
                : (Action)Delegate.CreateDelegate(typeof(Action), this, cache.Update);


            ManagedLateUpdate = cache.LateUpdate == null
                ? null
                : (Action)Delegate.CreateDelegate(typeof(Action), this, cache.LateUpdate);


            ManagedFixedUpdate = cache.FixedUpdate == null
                ? null
                : (Action)Delegate.CreateDelegate(typeof(Action), this, cache.FixedUpdate);
        }


        /// <summary>
        /// Searches a type for a valid optional lifecycle method.
        ///
        /// Requirements:
        /// - Instance method
        /// - Public or private
        /// - No parameters
        /// - Returns void
        /// </summary>
        private static MethodInfo FindMethod(Type type, string methodName)
        {
            MethodInfo method = type.GetMethod(
                methodName,
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic);


            if (method == null)
                return null;


            if (method.ReturnType != typeof(void))
                return null;


            if (method.GetParameters().Length != 0)
                return null;


            return method;
        }
    }

    public interface iLazyMonobehaviour
    {
        Action ManagedUpdate { get; }
        Action ManagedLateUpdate { get; }
        Action ManagedFixedUpdate { get; }

    }
}