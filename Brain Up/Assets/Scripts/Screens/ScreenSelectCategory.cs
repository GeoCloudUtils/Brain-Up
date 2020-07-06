/*
    Author: Ghercioglo Roman
 */
using Assets.Scripts.Games;
using UnityEngine;

namespace Assets.Scripts.Screens
{
    public class ScreenSelectCategory : MonoBehaviour
    {
        public GameObject[] categories;
        public ScreenSelectModule selectModuleScreen;
        private GameObject _lastCategory;
        public ScreenDifficultyLevels difficultyLevelsScreen;

        public void Show(GameModule category)
        {
            _lastCategory = categories[(int)category];
            _lastCategory.SetActive(true);
        }

        public void OnGameClicked(int gameId)
        {
            if (_lastCategory != null)
                _lastCategory.SetActive(false);

            var global = ControllerGlobal.Instance;
            GameId gameType = (GameId)gameId;
            if (global.GetLevelDataFor(gameType) == null)//have not difficulty levels
            {
                GameScreenGlobal.Instance.Show(true);
                global.currDifficulty = GameDifficulty.Any;
                global.StartGame(gameType, GameLanguage.English);
            }
            else
            {
                difficultyLevelsScreen.InitScreen(gameType);
                difficultyLevelsScreen.Show(true);
            }

            
        }

        public void OnExitClicked()
        {
            if (_lastCategory != null)
                _lastCategory.SetActive(false);
            selectModuleScreen.Show(true);
        }
    }
}