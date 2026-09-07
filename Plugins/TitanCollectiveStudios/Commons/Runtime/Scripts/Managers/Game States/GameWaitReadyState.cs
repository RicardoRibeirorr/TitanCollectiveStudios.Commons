using TitanCollectiveStudios.GameManagers.GameStates;
using UnityEngine;


namespace TitanCollectiveStudios.GameManagers
{
    public class GameWaitReadyState : GameState
    {
        float loadingCountDown = 2f;
        public override eGameStates State => eGameStates.WaitReady;
        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("Game " + GetType().Name);
            GameNotifications.OnGameWaitReady?.Invoke();
            loadingCountDown = 2f;
        }
        public override void OnUpdate() { 
            base.OnUpdate();


            loadingCountDown -= Time.deltaTime;

            if (loadingCountDown <= 0)
                _stateMachine.Change<GameRunningState>();
        }
        public override void OnExit() { base.OnExit(); }
    }
}