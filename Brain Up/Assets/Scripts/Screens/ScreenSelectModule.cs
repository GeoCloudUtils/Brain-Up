/*
    Author: Ghercioglo Roman
 */
using Assets.Scripts.Games;
using Assets.Scripts.Games.__Other;
using Assets.Scripts.Games.GuessWordGame;
using UnityEngine;

namespace Assets.Scripts.Screens
{
    public class ScreenSelectModule : MonoBehaviour
    {
        [Header("References")]
        public GameObject screen;
        ModuleDescriptions moduleDescriptions;
        public ScreenSelectCategory selectCategoryScreen;
        //
        private ControllerGuessWord _controller;


        private void Start()
        {
            _controller = ControllerGuessWord.Instance;
            moduleDescriptions = Resources.Load<ModuleDescriptions>("ModuleDescriptions/Desc");
        }

        private string GetModuleDesc(GameId gameId)
        {
            foreach (ModuleDescriptionRow row in moduleDescriptions.descriptions)
            {
                if (row.gameId == gameId) return row.description;
            }

            return null;
        }

        public void Show(bool show)
        {
            screen.SetActive(show);
        }


        public void SelectModule(int moduleId)
        {
            screen.SetActive(false);

            if (moduleId == -1)
                selectCategoryScreen.OnGameClicked((int)GameId.TimeKiller);
            else
                selectCategoryScreen.Show((GameModule)moduleId);
        }
    }
}