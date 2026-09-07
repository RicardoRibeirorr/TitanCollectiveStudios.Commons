using Assets.Plugins.TitanCollectiveStudios.Commons.Runtime.Scripts.Managers;
using TitanCollectiveStudios.GameManagers.GameStates;
using System;
using System.Collections.Generic;
using TitanCollectiveStudios.Commons.Inspectors;
using TitanCollectiveStudios.Commons.Managers;
using TitanCollectiveStudios.Commons.StateMachines.Finite;
using UnityEngine.SceneManagement;

namespace TitanCollectiveStudios.GameManagers
{

    [System.Serializable]
    public struct GameBlackbox
    {
        //string loadSaveName; //not developed yet
        public string transitioningToMap;
        public string currentSaveID;
    }

    public class GameManager : StandaloneFiniteStateMachine<GameManager, GameState>
    {
        public GameSettingsData Settings;
        public GameBlackbox Blackbox = new GameBlackbox();
        [Disabled] public List<iGameSystem> _systems = new List<iGameSystem>();


        public Action<eGameStates> OnStateChanged;
        public eGameStates State => this.CurrentState.State;

        public object PlayerSystem { get; internal set; }

        protected override void Start()
        {
            base.Start();

            //Register States
            Register<GameMainMenu>(new GameMainMenu());
            Register<GameStartState>(new GameStartState());
            Register<GamePaused>(new GamePaused());
            Register<GameSaveState>(new GameSaveState());
            Register<GameLoadState>(new GameLoadState());
            Register<GameTransitionStartState>(new GameTransitionStartState());
            Register<GameTransitionEndState>(new GameTransitionEndState());
            Register<GameWaitReadyState>(new GameWaitReadyState());
            Register<GameRunningState>(new GameRunningState());
            Register<GameCinematicState>(new GameCinematicState());

            //Register events to states
            GameNotifications.DoGamePaused += Change<GamePaused>;
            GameNotifications.DoSave += Change<GameSaveState>;
            GameNotifications.DoLoadLatestSave += () => { ClearBlackbox(); Change<GameLoadState>(); };
            GameNotifications.DoMapTransition += (string i) => { Blackbox.transitioningToMap = i; Change<GameLoadState>(); };
            GameNotifications.DoCinematicStart += Change<GameCinematicState>;
            GameNotifications.DoCinematicEnd += Change<GameRunningState>;

            //If scene is "MainMenu" then stay there until decision to load/create save.
            //Otherwise we are already in the gameplay (of developing scene)
            if (SceneManager.GetActiveScene().name == "MainMenu")
                Change<GameMainMenu>();
            else
                Change<GameStartState>();
        }

        private void ClearBlackbox() { Blackbox = new GameBlackbox(); }


        public override void Change<GameState>()
        {
            base.Change<GameState>();
            OnStateChanged?.Invoke(CurrentState.State);
        }
        public override void ForceChange(Type t)
        {
            base.ForceChange(t);
            OnStateChanged?.Invoke(CurrentState.State);
        }

        public void RegisterSystem(iGameSystem gameSystem)
        {
            _systems.Add(gameSystem);
        }
    }

}
