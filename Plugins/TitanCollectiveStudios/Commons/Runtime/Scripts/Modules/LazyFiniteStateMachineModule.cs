using System.Collections;
using UnityEngine;

namespace TitanCollectiveStudios.Commons.Modules
{
    /// <summary>
    /// Finite state machine module that updates the state machine at a configurable interval.
    /// Useful for reducing update frequency of non-critical systems such as background AI,
    /// decision making, or expensive state evaluations.
    /// </summary>
    public class LazyFiniteStateMachineModule : BaseFSMModule
    {
        [SerializeField]
        private float interval = 1f;

        private Coroutine routine;

        /// <summary>
        /// Sets the time interval between finite state machine updates.
        /// </summary>
        /// <param name="intervalBetweenUpdates">The delay in seconds between updates.</param>
        public void SetInterval(float intervalBetweenUpdates)
        {
            interval = intervalBetweenUpdates;
        }

        /// <summary>
        /// Starts the coroutine responsible for periodically updating the finite state machine.
        /// </summary>
        protected virtual void Start()
        {
            routine = StartCoroutine(UpdateRoutine());
        }

        /// <summary>
        /// Coroutine that periodically updates the finite state machine based on the configured interval.
        /// </summary>
        /// <returns>An enumerator used by Unity's coroutine system.</returns>
        private IEnumerator UpdateRoutine()
        {
            var wait = new WaitForSeconds(interval);

            while (true)
            {
                core.Update();
                yield return wait;
            }
        }
    }
}