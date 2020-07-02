/*
    Author: Ghercioglo Roman
 */
using Assets.Scripts.Framework.General;
using Assets.Scripts.Games;
using Scripts.Extensions;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Screens
{
    public class DialogWin : MonoBehaviour
    {
        [Header("References")]
        public GameObject screen;
        public ScreenSelectModule selectModuleScreen;
        public GameScreenLetters lettersGamesScreen;
        public TMP_Text coinsCollected;
        public TMP_Text experienceCollected;
        public TMP_Text timePassed;
        public TMP_Text hintsUsed;
        public TMP_Text efficiency;
        public TMP_Text errors;
        //
        private ControllerGlobal controller;


        private void Start()
        {
            controller = ControllerGlobal.Instance;
        }

        public void Show(int efficiency, int coins=-1, int experience=-1, int time=-1, int hints=-1, int errors=-1)
        {
            this.efficiency.text = efficiency.Clamp(0,100) + "%";
            if (coins != -1)
                coinsCollected.text = "+" + coins;
            if (experience != -1)
                experienceCollected.text = "+" + experience;
            if (time != -1)
                timePassed.text = TimeHelper.FormatMMSS(time);
            if (hints != -1)
                hintsUsed.text = hints.ToString();
            if (errors != -1)
                this.errors.text = errors.ToString();

            coinsCollected.transform.parent.gameObject.SetActive(coins != -1);
            experienceCollected.transform.parent.gameObject.SetActive(experience != -1);
            timePassed.transform.parent.gameObject.SetActive(time != -1);
            hintsUsed.transform.parent.gameObject.SetActive(hints != -1);
            this.errors.transform.parent.gameObject.SetActive(errors != -1);

            screen.SetActive(true);
        }

        public void OnGamesListClicked()
        {
            screen.SetActive(false);
            controller.StopGame(GameEndReason.Exit);
            selectModuleScreen.Show(true);
        }

        public void OnNextLevelClicked()
        {
            screen.SetActive(false);
            controller.RestartGame();
            if(GameScreenGlobal.Instance._lastScreen!=null)
            GameScreenGlobal.Instance._lastScreen.Show(true);
        }

        internal void Show(bool show)
        {
            screen.SetActive(show);
        }
    }
}
