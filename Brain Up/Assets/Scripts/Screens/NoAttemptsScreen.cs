/*
    Author: Ghercioglo Roman
 */
using Assets.Scripts.Games;
using Assets.Scripts.Games.VM_Letters;
using UnityEngine;

namespace Assets.Scripts.Screens
{
    public class NoAttemptsScreen : MonoBehaviour
    {
        [Header("References")]
        public GameObject screen;


        public void OnGamesListClicked()
        {
            Show(false);
            GlobalController.Instance.StopGame(GameEndReason.Exit);
            GameScreen.Instance.Show(false);
        }

        public void OnRetryClicked()
        {
            Show(false);
            GlobalController.Instance.RestartGame();
        }

        internal void Show(bool show)
        {
            screen.SetActive(show);
        }
    }
}
