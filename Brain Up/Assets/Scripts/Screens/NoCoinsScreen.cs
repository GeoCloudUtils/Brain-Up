/*
    Author: Ghercioglo Roman
 */
using Assets.Scripts.Games;
using Assets.Scripts.Games.VM_Letters;
using UnityEngine;

namespace Assets.Scripts.Screens
{
    public class NoCoinsScreen : MonoBehaviour
    {
        [Header("References")]
        public GameObject screen;
        public SelectModuleScreen selectModuleScreen;
        public int COINS_FOR_AD;

        private void Start()
        {
        }

        public void OnGamesListClicked()
        {
            screen.SetActive(false);
            GlobalController.Instance.StopGame(GameEndReason.Exit);
            GameScreen.Instance.Show(false);
        }

        public void OnOpenShopClicked()
        {
            //TODO open shop
        }

        public void OnWatchAdClicked()
        {
            screen.SetActive(false);
            GlobalController.Instance.WatchAd((success)=>
            {
                Database.Instance.Coins += COINS_FOR_AD;
                GlobalController.Instance.RestartGame();
            });
        }

        internal void Show(bool show)
        {
            screen.SetActive(show);
        }
    }
}
