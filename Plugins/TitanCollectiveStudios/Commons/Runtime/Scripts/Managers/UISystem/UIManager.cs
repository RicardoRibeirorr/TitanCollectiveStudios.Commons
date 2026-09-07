using TitanCollectiveStudios.GameManagers.GameStates;
using UnityEngine;

namespace TitanCollectiveStudios.Commons.Managers
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIManager : GameSystem<UIManager>
    {
        //[SerializeField] private UIPlayerInfoDisplayer uiPlayerInfoDisplay;


        private CanvasGroup canvasGroup; //allows for fade in/out of all uis inside
        private bool isInitialized = false;

        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        void Initialize()
        {
            if (isInitialized) return;

            //uiPlayerInfoDisplay.Initialize();
        }

        protected override void OnGameStateChanged(eGameStates state)
        {
            switch (state)
            {
                case eGameStates.MainMenu:
                    break;
                case eGameStates.Game_Start:
                    break;
                case eGameStates.Game_Pause:
                    break;
                case eGameStates.Game_Load:
                    break;
                case eGameStates.Game_Save:
                    break;
                case eGameStates.Transition_Start:
                    canvasGroup.alpha = 0;
                    break;
                case eGameStates.Transition_End:
                    canvasGroup.alpha = 0;
                    break;
                case eGameStates.WaitReady:
                    canvasGroup.alpha = 0;
                    break;
                case eGameStates.Running:
                    if(canvasGroup.alpha==0)
                        //canvasGroup.DOFade(1,1.5f);
                    Initialize();
                    break;
                case eGameStates.Cinematic:
                    canvasGroup.alpha = 0;
                    break;
                default:
                    break;
            }
        }
    }
}
