using System;

namespace TitanCollectiveStudios.GameManagers
{
    public static class GameNotifications
    {
        /*************************************************
         *                  Lifecycle
         *************************************************/
        public static Action OnGameStarted;
        public static Action DoGamePaused;
        public static Action OnGameRunning;
        public static Action OnGameWaitReady;


        /*************************************************
         *                  Save/Load
         *************************************************/
        //public static Action<string> RequestLoad; //not implemented yet
        public static Action DoSave;
        public static Action DoLoadLatestSave;


        /*************************************************
         *                  Map Transition
         *************************************************/
        public static Action<string> DoMapTransition;


        /*************************************************
         *                  Cinematic
         *************************************************/
        public static Action DoCinematicStart;
        public static Action DoCinematicEnd;


    }
}