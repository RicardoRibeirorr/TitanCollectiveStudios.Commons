using UnityEngine;


namespace TitanCollectiveStudios.Commons.Agents.Tasks
{
    public interface iAgentTask
    {
        /// <summary>
        /// Called once when the task starts.
        /// </summary>
        void StartTask();


        /// <summary>
        /// Called every update while the task is running.
        /// </summary>
        void UpdateTask();


        /// <summary>
        /// Returns true when the task has completed.
        /// </summary>
        bool IsCompleted();


        /// <summary>
        /// Called when the task is removed or completed.
        /// </summary>
        void StopTask();
    }
}