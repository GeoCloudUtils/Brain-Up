/*
    Author: Ghercioglo Roman
 */
using Assets.Scripts.Games;
using Assets.Scripts.Games.__Other;
using Assets.Scripts.Games.VM_Letters;
using UnityEngine;

namespace Assets.Scripts.Screens
{
    public class SelectModuleScreen : MonoBehaviour
    {
        [Header("References")]
        public GameObject screen;
        public ModuleInfoScreen moduleInfoScreen;
        ModuleDescriptions moduleDescriptions;
        public SelectCategoryScreen selectCategoryScreen;
        //
        private VM_LettersController _controller;


        private void Start()
        {
            _controller = (VM_LettersController)VM_LettersController.Instance;
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

        public void StartGame_OrderedLetters()
        {
            screen.SetActive(false);
            moduleInfoScreen.Show(GameId.VisualMemory_Letters, GameLanguage.English, GetModuleDesc(GameId.VisualMemory_Letters));
        }

        internal void Show(bool show)
        {
            screen.SetActive(show);
        }

        public void StartGame_NonOrderedLetters()
        {
            screen.SetActive(false);
            moduleInfoScreen.Show(GameId.NonOrderedLetters, GameLanguage.English, GetModuleDesc(GameId.NonOrderedLetters));
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