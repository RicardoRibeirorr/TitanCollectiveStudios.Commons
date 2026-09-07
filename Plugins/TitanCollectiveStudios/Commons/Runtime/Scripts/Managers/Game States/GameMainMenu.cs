using TitanCollectiveStudios.GameManagers.GameStates;
using UnityEngine;

namespace TitanCollectiveStudios.GameManagers
{
    public class GameMainMenu : GameState
    {
        public override eGameStates State => eGameStates.MainMenu;

        public override void OnEnter() { base.OnEnter(); Debug.Log("Game " + GetType().Name); }
        public override void OnUpdate() { base.OnUpdate(); }
        public override void OnExit() { base.OnExit(); }
    }
}