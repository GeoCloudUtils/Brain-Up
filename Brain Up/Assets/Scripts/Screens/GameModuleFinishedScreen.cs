/*
    Author: Ghercioglo Roman
 */
using Assets.Scripts.Games;
using Assets.Scripts.Games.VM_Letters;
using UnityEngine;

namespace Assets.Scripts.Screens
{
    public class GameModuleFinishedScreen : MonoBehaviour
    {
        [Header("References")]
        public GameObject screen;
        public SelectModuleScreen selectModuleScreen;
        //
        private VM_LettersController controller;

        private void Start()
        {
            controller = (VM_LettersController)VM_LettersController.Instance;
        }

        public void Show()
        {
            screen.SetActive(true);
        }

        public void OnGamesListClicked()
        {
            screen.SetActive(false);
            controller.StopGame(GameEndReason.Exit);
            selectModuleScreen.Show(true);
        }
    }
}
