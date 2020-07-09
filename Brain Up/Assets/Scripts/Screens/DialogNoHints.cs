/*
    Author: Ghercioglo Roman
 */
using Assets.Scripts.Games;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Screens
{
    public class DialogNoHints : MonoBehaviour
    {
        public GameObject screen;
        public TMP_Text hintsCount;

        void Start()
        {
            hintsCount.text = string.Format("You can get <b>+{0}</b> hints for watchind an Video Ad. Want hints?",
                ControllerGlobal.Instance.HINTS_FOR_AD);
        }

        public void OnYesClicked()
        {
            Show(false);
            ControllerGlobal.Instance.Pause(false);

            ControllerGlobal.Instance.WatchAd((watched)=>
            {
                if(watched)
                    Database.Instance.Hints += 4;
                ControllerGlobal.Instance.Pause(true);
            });
        }

        public void OnNoClicked()
        {
            Show(false);
            ControllerGlobal.Instance.Pause(false);
        }


        internal void Show(bool show)
        {
            screen.SetActive(show);
        }
    }
}