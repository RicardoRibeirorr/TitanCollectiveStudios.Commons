using TitanCollectiveStudios.GameManagers;
using TitanCollectiveStudios.Commons.Managers;
using TitanCollectiveStudios.GameManagers.GameStates;

public abstract class GameSystem<T> : AGameSystem<T> where T : GameSystem<T>
{
    //public virtual void OnSystemInit() { }

    protected override void Awake()
    {
        base.Awake();
        if (GameManager.i == null)
            throw new System.Exception("GameSystems require a GameManager in the scene");

        GameManager.i.RegisterSystem(this);

        GameManager.i.OnStateChanged += OnGameStateChanged;
    }

    protected override void OnDestroy()
    {
        if (GameManager.i != null)
        {
            GameManager.i.OnStateChanged -= OnGameStateChanged;
        }
        base.OnDestroy();
    }

    protected virtual void OnGameStateChanged(eGameStates state)
    {
        //switch (state)
        //{
        //    case eGameStates.MainMenu:
        //        break;
        //    case eGameStates.Game_Start:
        //        break;
        //    case eGameStates.Game_Pause:
        //        break;
        //    case eGameStates.Game_Load:
        //        break;
        //    case eGameStates.Game_Save:
        //        break;
        //    case eGameStates.Transition_Start:
        //        break;
        //    case eGameStates.Transition_End:
        //        break;
        //    case eGameStates.Running:
        //        //There is no save to load OR it's a lost npc not handled by factory?
        //        if (IsInitialized == false) this.Initialize();
        //        break;
        //    case eGameStates.Cinematic:
        //        break;
        //    default:
        //        break;
        //}
    }
}
