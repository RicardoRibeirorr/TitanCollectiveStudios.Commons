using TitanCollectiveStudios.Commons.Agents.Tasks;
using TitanCollectiveStudios.Commons.Inspectors;
using TitanCollectiveStudios.Commons.Modules;
using UnityEngine;


namespace TitanCollectiveStudios.Commons.Agents.Modules
{
    [DisallowMultipleComponent]
    public class TaskRunnerModule : Module
    {
        [SerializeField, Disabled] protected string _currentTaskName;
        private iAgentTask _currentTask;


        protected virtual void Update()
        {
            if (_currentTask == null)
            {
                StopTask();
                return;
            }


            _currentTask.UpdateTask();


            if (_currentTask.IsCompleted())
            {
                _currentTask.StopTask();

                _currentTask = null;
            }
        }


        /// <summary>
        /// Assigns a new task to this agent.
        /// Existing tasks are cancelled.
        /// </summary>
        public void SetTask(iAgentTask task)
        {
            if (_currentTask != null)
            {
                _currentTask = null;
                _currentTask.StopTask();
            }


            _currentTask = task;
            _currentTask.StartTask();
            this.enabled = true;

            _IntUptName();
        }

        public void StopTask()
        {
            if(_currentTask!=null)
                _currentTask.StopTask();
            _currentTask = null;

            this.enabled = false;
            _IntUptName();
        }

        private void _IntUptName()
        {
            _currentTaskName = _currentTask == null ? "" : _currentTask.GetType().Name;
        }
    }
}
