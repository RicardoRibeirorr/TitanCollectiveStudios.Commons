using TitanCollectiveStudios.GameManagers.GameStates;
using UnityEngine;


namespace TitanCollectiveStudios.GameManagers
{
    public class GameRunningState : GameState
    {
        public override eGameStates State => eGameStates.Running;
        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("Game " + GetType().Name);
            GameNotifications.OnGameRunning?.Invoke();
        }
        public override void OnUpdate() { base.OnUpdate(); }
        public override void OnExit() { base.OnExit(); }
    }
}