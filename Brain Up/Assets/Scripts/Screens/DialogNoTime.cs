/*
    Author: Ghercioglo Roman
 */
using Assets.Scripts.Framework.General;
using Assets.Scripts.Games;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Screens
{
    public class DialogNoTime : MonoBehaviour
    {
        [Header("References")]
        public GameObject screen;
        public TMP_Text timeForAd;
        public ScreenSelectModule selectModuleScreen;

        private void Start()
        {
            timeForAd.text = "+" + TimeHelper.FormatMMSS(ControllerGlobal.Instance.timeForWatchAd);
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
            });
        }

        internal void Show(bool show)
        {
            screen.SetActive(show);
        }
    }
}
