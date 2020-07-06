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

        public void OnYesClicked()
        {
            screen.SetActive(false);
            ControllerGlobal.Instance.StopGame(GameEndReason.Exit);
        }

        internal void Show(bool show)
        {
            screen.SetActive(show);
        }

        internal void InitScreen(string gameName, string difficultyName)
        {
            info.text = string.Format("You finished <b>\"{0}\"</b> stage of <b>\"{1}\"</b> game.\"",difficultyName,gameName);
        }
    }
}
