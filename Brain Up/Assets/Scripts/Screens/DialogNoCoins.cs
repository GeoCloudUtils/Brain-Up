/*
    Author: Ghercioglo Roman
 */
using Assets.Scripts.Games;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Screens
{
    public class DialogNoCoins : MonoBehaviour
    {
        [Header("References")]
        public GameObject screen;
        public ScreenSelectModule selectModuleScreen;
        public ScreenShop shopScreen;
        public TMP_Text coinsCount;

        private void Start()
        {
            coinsCount.text = "+" + ControllerGlobal.Instance.COINS_FOR_AD;
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
            ControllerGlobal.Instance.WatchAd((success)=>
            {
                var global = ControllerGlobal.Instance;
                Database.Instance.Coins += global.COINS_FOR_AD;
                screen.SetActive(false);
            });
        }

        internal void Show(bool show)
        {
            screen.SetActive(show);
        }
    }
}
