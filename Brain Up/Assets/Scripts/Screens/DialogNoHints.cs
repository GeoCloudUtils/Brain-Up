/*
    Author: Ghercioglo Roman
 */
using Assets.Scripts.Games;
using UnityEngine;

namespace Assets.Scripts.Screens
{
    public class DialogNoHints : MonoBehaviour
    {
        public GameObject screen;


        void Start()
        {

        }

        public void OnYesClicked()
        {
            Show(false);
            ControllerGlobal.Instance.Pause(false);

            //TODO Wath add
            Database.Instance.Hints += 4;
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