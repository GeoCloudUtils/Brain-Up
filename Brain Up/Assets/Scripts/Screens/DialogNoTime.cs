/*
    Author: Ghercioglo Roman
 */
using Assets.Scripts.Games;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Screens
{
    public class DialogNoTime : MonoBehaviour
    {
        [Header("References")]
        public GameObject screen;
        public ScreenSelectModule selectModuleScreen;

        private void Start()
        {
        }

        public void OnGamesListClicked()
        {
            screen.SetActive(false);
            ControllerGlobal.Instance.StopGame(GameEndReason.Exit);
            GameScreenGlobal.Instance.Show(false);
        }

        public void OnRetryClicked()
        {
            screen.SetActive(false);
            ControllerGlobal.Instance.RestartGame();
        }

        public void OnWatchAdClicked()
        {
            screen.SetActive(false);
            ControllerGlobal.Instance.Prolong((success)=>
            {
                if (!success)
                    screen.SetActive(true);
                //if (success)
                //    GlobalController.Instance.Prolong();
                //else
                //    screen.SetActive(true);
            });
        }

        internal void Show(bool show)
        {
            screen.SetActive(show);
        }
    }
}
