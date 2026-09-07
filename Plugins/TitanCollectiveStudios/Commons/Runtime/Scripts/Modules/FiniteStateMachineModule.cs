namespace TitanCollectiveStudios.Commons.Modules
{
    /// <summary>
    /// Finite state machine module that updates the state machine every frame.
    /// Use this module when states require continuous evaluation during the game loop.
    /// </summary>
    public class FiniteStateMachineModule : BaseFSMModule
    {
        /// <summary>
        /// Updates the finite state machine on every frame.
        /// Executes the current state's update logic.
        /// </summary>
        protected virtual void Update()
        {
            core?.Update();
        }
    }
}