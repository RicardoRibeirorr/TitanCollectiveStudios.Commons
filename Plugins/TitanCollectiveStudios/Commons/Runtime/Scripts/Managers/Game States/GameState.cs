using TitanCollectiveStudios.Commons.StateMachines.Finite;

namespace TitanCollectiveStudios.GameManagers.GameStates
{

    //This jsut serves the purpose of simplifying the way
    // system handle the game state.
    // Actually this should be the way to use it, but the current
    //state machine is more powerfull
    public enum eGameStates
    {
        MainMenu,

        Game_Start,
        Game_Pause,

        Game_Load,
        Game_Save,

        Transition_Start,
        Transition_End,

        WaitReady,

        Running,
        Cinematic
    }

    public abstract class GameState : _IFiniteMachineState
    {
        public abstract eGameStates State { get; }
        protected StandaloneFiniteStateMachine<GameManager, GameState> _stateMachine => GameManager.i;
        protected GameBlackbox _blackbox => GameManager.i.Blackbox;
        protected virtual void OnRegister() { }
        public virtual void OnEnter() { }
        public virtual void OnUpdate() { }
        public virtual void OnExit() { }

    }
}