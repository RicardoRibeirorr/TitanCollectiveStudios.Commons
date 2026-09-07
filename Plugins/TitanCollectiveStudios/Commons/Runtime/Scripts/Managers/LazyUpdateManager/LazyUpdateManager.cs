using System;
using System.Collections;
using System.Collections.Generic;
using TitanCollectiveStudios.Commons.Core;
using TitanCollectiveStudios.Commons.Managers;
using TitanCollectiveStudios.Commons.Utils;
using UnityEngine;

namespace TitanCollectiveStudios.GameManagers
{
    /// <summary>
    /// Centralized update dispatcher.
    ///
    /// Replaces Unity's Update/LateUpdate/FixedUpdate calls for registered
    /// iLazyMonobehaviour instances.
    ///
    /// Objects are only registered for update loops they actually implement.
    /// This avoids thousands of empty Update calls when managing many objects.
    ///
    /// Example:
    ///
    /// class Enemy : iLazyMonobehaviour
    /// {
    ///     private void LazyUpdate()
    ///     {
    ///         // Called automatically.
    ///     }
    /// }
    /// </summary>
    /// 
    public class LazyUpdateManager : ALazyUpdateManager<LazyUpdateManager> { }


    [ExecuteAlways]
    public abstract class ALazyUpdateManager<T> : Standalone<T> where T : ALazyUpdateManager<T>
    {
        [Header("Update Settings")]

        [Tooltip("Time in seconds between LazyUpdate executions.")]
        [SerializeField]
        private float m_updateInterval = 1f;


        /// <summary>
        /// Time accumulated since the last lazy update execution.
        /// </summary>
        private float m_timer;


        /// <summary>
        /// Objects registered for LazyUpdate calls.
        /// </summary>
        private readonly FastRemoveList<iLazyMonobehaviour> _updates = new();


        /// <summary>
        /// Objects registered for LazyLateUpdate calls.
        /// </summary>
        private readonly FastRemoveList<iLazyMonobehaviour> _lateUpdates = new();


        /// <summary>
        /// Objects registered for LazyFixedUpdate calls.
        /// </summary>
        private readonly FastRemoveList<iLazyMonobehaviour> _fixedUpdates = new();


        /// <summary>
        /// Returns the configured delay between LazyUpdate executions.
        /// </summary>
        public float UpdateInterval => m_updateInterval;


        /// <summary>
        /// Returns whether this manager currently has registered objects.
        /// </summary>
        public bool HasRegisteredObjects =>
            _updates.Count > 0 ||
            _lateUpdates.Count > 0 ||
            _fixedUpdates.Count > 0;


        /// <summary>
        /// Unity Update loop.
        ///
        /// Accumulates time and only executes LazyUpdate callbacks when the
        /// configured interval has elapsed.
        /// </summary>
        private void Update()
        {
            if (!HasRegisteredObjects)
                return;


            m_timer += Time.deltaTime;


            if (m_timer < m_updateInterval)
                return;


            m_timer = 0f;


            ExecuteUpdate();
        }


        /// <summary>
        /// Executes all registered LazyUpdate methods.
        /// </summary>
        private void ExecuteUpdate()
        {
            foreach (iLazyMonobehaviour obj in _updates)
            {
                try
                {
                    obj.ManagedUpdate?.Invoke();
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);
                }
            }
        }


        /// <summary>
        /// Executes all registered LazyLateUpdate methods.
        ///
        /// Currently runs every Unity frame.
        /// If needed, this can have its own interval later.
        /// </summary>
        private void LateUpdate()
        {
            foreach (iLazyMonobehaviour obj in _lateUpdates)
            {
                try
                {
                    obj.ManagedLateUpdate?.Invoke();
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);
                }
            }
        }


        /// <summary>
        /// Executes all registered LazyFixedUpdate methods.
        ///
        /// Uses Unity's physics timing.
        /// </summary>
        private void FixedUpdate()
        {
            foreach (iLazyMonobehaviour obj in _fixedUpdates)
            {
                try
                {
                    obj.ManagedFixedUpdate?.Invoke();
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);
                }
            }
        }


        /// <summary>
        /// Registers a iLazyMonobehaviour into this manager.
        ///
        /// Only callbacks that exist on the object are registered.
        /// </summary>
        public void Register(iLazyMonobehaviour obj)
        {
            if (obj.ManagedUpdate != null)
                _updates.Add(obj);


            if (obj.ManagedLateUpdate != null)
                _lateUpdates.Add(obj);


            if (obj.ManagedFixedUpdate != null)
                _fixedUpdates.Add(obj);
        }


        /// <summary>
        /// Removes a iLazyMonobehaviour from this manager.
        /// </summary>
        public void Unregister(iLazyMonobehaviour obj)
        {
            _updates.Remove(obj);
            _lateUpdates.Remove(obj);
            _fixedUpdates.Remove(obj);
        }


        /// <summary>
        /// Removes all registered objects.
        /// </summary>
        public void Clear()
        {
            _updates.Clear();
            _lateUpdates.Clear();
            _fixedUpdates.Clear();
        }
    }


    /// <summary>
    /// A list optimized for fast removals.
    ///
    /// Unlike the standard List.Remove(), which shifts all elements after the removed
    /// item and has O(n) complexity, this collection removes items by replacing the
    /// removed element with the last element in the list.
    ///
    /// Removal is O(1), making it useful for systems where objects frequently register
    /// and unregister themselves, such as update managers, event systems, and
    /// entity/component systems.
    ///
    /// Note:
    /// The order of elements is not guaranteed after removing an item.
    /// </summary>
    public class FastRemoveList<T> : IEnumerable<T>
    {
        private readonly List<T> _items = new();
        private readonly HashSet<T> _lookup = new();
        public int Count => _items.Count;


        public void Add(T item)
        {
            if (_lookup.Add(item))
            {
                _items.Add(item);
            }
        }


        public void Remove(T item)
        {
            if (!_lookup.Remove(item))
                return;

            int index = _items.IndexOf(item);

            int last = _items.Count - 1;

            _items[index] = _items[last];
            _items.RemoveAt(last);
        }


        public void Clear()
        {
            _items.Clear();
            _lookup.Clear();
        }


        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }


    /// <summary>
    /// Provides centralized timing information for managed update systems.
    ///
    /// This class tracks the time elapsed between refresh calls and can be used by
    /// custom schedulers, lazy update systems, or job managers that need their own
    /// timing reference instead of relying directly on Unity's Update loop.
    ///
    /// Example usages:
    /// - Running AI updates at custom intervals.
    /// - Throttling expensive calculations.
    /// - Scheduling background gameplay systems.
    /// </summary>
    public class UpdateJobTime
    {
        /// <summary>
        /// Global instance used by systems that require shared update timing.
        /// </summary>
        public static UpdateJobTime InstanceRef { get; } = new();


        /// <summary>
        /// Time elapsed since the last Refresh() call.
        /// </summary>
        public float DeltaTime { get; private set; }


        private float _lastTime;


        /// <summary>
        /// Updates the internal timer and calculates the elapsed time since the
        /// previous refresh.
        ///
        /// Should normally be called once per frame by the central update manager.
        /// </summary>
        public void Refresh()
        {
            float current = Time.realtimeSinceStartup;

            DeltaTime = current - _lastTime;

            _lastTime = current;
        }
    }
}