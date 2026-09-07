using TitanCollectiveStudios.GameManagers.GameStates;
using UnityEngine;

namespace TitanCollectiveStudios.GameManagers
{
    public class GameCinematicState : GameState
    {
        public override eGameStates State => eGameStates.Cinematic;

        public override void OnEnter() { base.OnEnter(); Debug.Log("Game " + GetType().Name); }

        public override void OnUpdate() { base.OnUpdate(); }
        public override void OnExit() { base.OnExit(); }
    }
}