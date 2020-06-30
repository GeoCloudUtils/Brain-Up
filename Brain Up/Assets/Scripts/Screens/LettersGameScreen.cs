/*
    Author: Ghercioglo Roman
 */
using Assets.Scripts.Games;
using Assets.Scripts.Games.VM_Letters;
using UnityEngine;

namespace Assets.Scripts.Screens
{
    public class LettersGameScreen : MonoBehaviour, AbstractScreen
    {
        [Header("References")]
        public SelectModuleScreen selectModuleScreen;
        public GameObject screen;
        public WinScreen winScreen;
        public NoTimeScreen noTimeScreen;
        public NoHintsScreen noHintsScreen;
        public GameModuleFinishedScreen gameModeFinishedScreen;
        //
        protected Database _database;
        protected GlobalController globalController;
        private VM_LettersController controller;
        protected int gameId;



        protected void Start()
        {
            if (selectModuleScreen == null) Debug.LogError(nameof(selectModuleScreen) + " is not assigned!");
            if (screen == null) Debug.LogError(nameof(screen) + " is not assigned!");
            if (winScreen == null) Debug.LogError(nameof(winScreen) + " is not assigned!");
            if (gameModeFinishedScreen == null) Debug.LogError(nameof(gameModeFinishedScreen) + " is not assigned!");

            globalController = GlobalController.Instance;
            controller = (VM_LettersController)VM_LettersController.Instance;
            _database = Database.Instance;

            gameId = (int)GameId.VisualMemory_Letters;

        }

        public void Show(bool show)
        {
            screen.SetActive(show);
        }

        public void OnReloadClicked()
        {
            globalController.RestartGame();
        }

        public void OnGamesListClicked()
        {
            globalController.StopGame(GameEndReason.Exit);
            selectModuleScreen.Show(true);
        }


        public void OnHintClicked()
        {
            if (_database.Hints != 0)
            {
                bool success = globalController.Hint();
                if (success)
                    _database.Hints -= 1;
            }
            else
            {
                noHintsScreen.Show(true);
                globalController.Pause(true);
            }
          
        }
    }
}
