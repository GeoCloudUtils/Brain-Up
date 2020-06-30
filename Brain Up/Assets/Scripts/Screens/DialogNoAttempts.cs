/*
    Author: Ghercioglo Roman
 */
using Assets.Scripts.Games;
using UnityEngine;

namespace Assets.Scripts.Screens
{
    public class DialogNoAttempts : MonoBehaviour
    {
        [Header("References")]
        public GameObject screen;


        public void OnGamesListClicked()
        {
            Show(false);
            ControllerGlobal.Instance.StopGame(GameEndReason.Exit);
            GameScreenGlobal.Instance.Show(false);
        }

        public void OnRetryClicked()
        {
            Show(false);
            ControllerGlobal.Instance.RestartGame();
        }

        internal void Show(bool show)
        {
            screen.SetActive(show);
        }
    }
}
