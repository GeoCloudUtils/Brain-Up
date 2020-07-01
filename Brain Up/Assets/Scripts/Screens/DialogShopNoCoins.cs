/*
    Author: Ghercioglo Roman
 */
using Assets.Scripts.Games;
using UnityEngine;

namespace Assets.Scripts.Screens
{
    public class DialogShopNoCoins : MonoBehaviour
    {
        [Header("References")]
        public GameObject screen;
        public ScreenShop shopScreen;

        private void Start()
        {

        }

        internal void Show(bool show)
        {
            screen.SetActive(show);
        }


        public void OnGamesListClicked()
        {
            screen.SetActive(false);
            ControllerGlobal.Instance.StopGame(GameEndReason.Exit);
            GameScreenGlobal.Instance.Show(false);
        }

        public void OnOpenShopClicked()
        {
            Show(false);
            shopScreen.Show(true);
        }

        public void OnWatchAdClicked()
        {
            screen.SetActive(false);
            ControllerGlobal.Instance.WatchAd((success) =>
            {
                var global = ControllerGlobal.Instance;
                Database.Instance.Coins += global.COINS_FOR_AD;
                global.RestartGame();
            });
        }
    }
}
