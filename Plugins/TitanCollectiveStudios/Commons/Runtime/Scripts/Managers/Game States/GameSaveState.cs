using TitanCollectiveStudios.GameManagers.GameStates;
using TitanCollectiveStudios.Commons.Managers;
using UnityEngine;

namespace TitanCollectiveStudios.GameManagers
{
    public class GameSaveState : GameState
    {
        public override eGameStates State => eGameStates.Game_Save;
        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("Game " + GetType().Name);


            SaveSystem.i.SaveGame();

            _stateMachine.Change<GameWaitReadyState>();
        }
        public override void OnUpdate() { base.OnUpdate(); }
        public override void OnExit() { base.OnExit(); }
    }
}