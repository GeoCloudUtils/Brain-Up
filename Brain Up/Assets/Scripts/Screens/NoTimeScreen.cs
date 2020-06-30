/*
    Author: Ghercioglo Roman
 */
using Assets.Scripts.Games;
using Assets.Scripts.Games.VM_Letters;
using UnityEngine;

namespace Assets.Scripts.Screens
{
    public class NoTimeScreen : MonoBehaviour
    {
        [Header("References")]
        public GameObject screen;
        public SelectModuleScreen selectModuleScreen;

        private void Start()
        {
        }

        public void OnGamesListClicked()
        {
            screen.SetActive(false);
            GlobalController.Instance.StopGame(GameEndReason.Exit);
            GameScreen.Instance.Show(false);
        }

        public void OnRetryClicked()
        {
            screen.SetActive(false);
            GlobalController.Instance.RestartGame();
        }

        public void OnWatchAdClicked()
        {
            screen.SetActive(false);
            GlobalController.Instance.Prolong((success)=>
            {
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
