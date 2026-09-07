using TitanCollectiveStudios.GameManagers.GameStates;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TitanCollectiveStudios.GameManagers
{
    public class GameStartState : GameState
    {
        public override eGameStates State => eGameStates.Game_Start;
        private Scene _scene;
        private bool loaded;
        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("Game " + GetType().Name);

            _scene = SceneManager.GetActiveScene();

        }
        public override void OnUpdate()
        {
            base.OnUpdate();

            if (_scene.isLoaded)
            {
                if (!String.IsNullOrEmpty(_blackbox.currentSaveID))
                {
                    _stateMachine.Change<GameLoadState>();
                    return;
                }
                else
                {
                    //throw new Exception("Trying to start a game without a save?");
                    _stateMachine.Change<GameWaitReadyState>();
                    return;
                }
            }

        }
        public override void OnExit() { base.OnExit(); }
    }
}