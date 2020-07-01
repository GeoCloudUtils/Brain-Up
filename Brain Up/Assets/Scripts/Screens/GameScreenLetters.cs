/*
    Author: Ghercioglo Roman
 */
using Assets.Scripts.Games;
using Assets.Scripts.Games.GuessWordGame;
using UnityEngine;

namespace Assets.Scripts.Screens
{
    public class GameScreenLetters : MonoBehaviour, GameScreenAbstract
    {
        [Header("References")]
        public ScreenSelectModule selectModuleScreen;
        public GameObject screen;
        public DialogWin winScreen;
        public DialogNoTime noTimeScreen;
        public DialogNoHints noHintsScreen;
        //
        protected Database _database;
        protected ControllerGlobal globalController;
        private ControllerGuessWord controller;
        protected int gameId;



        protected void Start()
        {
            if (selectModuleScreen == null) Debug.LogError(nameof(selectModuleScreen) + " is not assigned!");
            if (screen == null) Debug.LogError(nameof(screen) + " is not assigned!");
            if (winScreen == null) Debug.LogError(nameof(winScreen) + " is not assigned!");
             
            globalController = ControllerGlobal.Instance;
            controller = (ControllerGuessWord)ControllerGuessWord.Instance;
            _database = Database.Instance;

            gameId = (int)GameId.RepeatLetters;

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
