/*
    Author: Ghercioglo Roman
 */
using Assets.Scripts.Games;
using UnityEngine;

namespace Assets.Scripts.Screens
{
    public class DialogNoCoins : MonoBehaviour
    {
        [Header("References")]
        public GameObject screen;
        public ScreenSelectModule selectModuleScreen;
        public int COINS_FOR_AD;

        private void Start()
        {
        }

        public void OnGamesListClicked()
        {
            screen.SetActive(false);
            ControllerGlobal.Instance.StopGame(GameEndReason.Exit);
            GameScreenGlobal.Instance.Show(false);
        }

        public void OnOpenShopClicked()
        {
            //TODO open shop
        }

        public void OnWatchAdClicked()
        {
            screen.SetActive(false);
            ControllerGlobal.Instance.WatchAd((success)=>
            {
                Database.Instance.Coins += COINS_FOR_AD;
                ControllerGlobal.Instance.RestartGame();
            });
        }

        internal void Show(bool show)
        {
            screen.SetActive(show);
        }
    }
}
