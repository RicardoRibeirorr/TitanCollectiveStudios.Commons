using TitanCollectiveStudios.GameManagers.GameStates;
using TitanCollectiveStudios.Commons.Managers;
using UnityEngine;

namespace TitanCollectiveStudios.GameManagers
{
    public class GameLoadState : GameState
    {
        public override eGameStates State => eGameStates.Game_Load;
        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("Game " + GetType().Name);


            SaveSystem.i.LoadGame();
            _stateMachine.Change<GameWaitReadyState>();
        }
        public override void OnUpdate() { base.OnUpdate(); }
        public override void OnExit() { base.OnExit(); }
    }
}