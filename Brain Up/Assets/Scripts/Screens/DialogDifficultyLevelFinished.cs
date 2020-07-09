/*
    Author: Ghercioglo Roman
 */
using Assets.Scripts.Games;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Screens
{
    public class DialogDifficultyLevelFinished : MonoBehaviour
    {
        [Header("References")]
        public GameObject screen;
        public ScreenSelectModule selectModuleScreen;
        public TMP_Text info;
        public TMP_Text coinsReward;
        public TMP_Text experienceReward;

        public void OnYesClicked()
        {
            screen.SetActive(false);
            ControllerGlobal.Instance.StopGame(GameEndReason.Exit);
        }

        internal void Show(bool show)
        {
            screen.SetActive(show);
        }

        internal void InitScreen(string gameName, string difficultyName, int coinsCountReward, int expCountReward)
        {
            info.text = string.Format("You finished <b>\"{0}\"</b> stage of <b>\"{1}\"</b> game.\"",difficultyName,gameName);

            if (coinsCountReward > 0)
            {
                coinsReward.text = "+" + coinsCountReward;
                coinsReward.transform.parent.gameObject.SetActive(true);
            }
            else
                coinsReward.transform.parent.gameObject.SetActive(false);

            if (expCountReward > 0)
            {
                experienceReward.text = "+" + expCountReward;
                experienceReward.transform.parent.gameObject.SetActive(true);
            }
            else
                experienceReward.transform.parent.gameObject.SetActive(false);
        }
    }
}
