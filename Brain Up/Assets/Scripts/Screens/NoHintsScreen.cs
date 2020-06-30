/*
    Author: Ghercioglo Roman
 */
using Assets.Scripts.Games;
using UnityEngine;

namespace Assets.Scripts.Screens
{
    public class NoHintsScreen : MonoBehaviour
    {
        public GameObject screen;


        void Start()
        {

        }

        public void OnYesClicked()
        {
            Show(false);
            GlobalController.Instance.Pause(false);

            //TODO Wath add
            Database.Instance.Hints += 4;
        }

        public void OnNoClicked()
        {
            Show(false);
            GlobalController.Instance.Pause(false);
        }


        internal void Show(bool show)
        {
            screen.SetActive(show);
        }
    }
}