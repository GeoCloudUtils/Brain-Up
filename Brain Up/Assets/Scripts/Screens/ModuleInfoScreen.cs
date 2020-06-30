/*
    Author: Ghercioglo Roman
 */
using Assets.Scripts.Games;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Screens
{
    public class ModuleInfoScreen : MonoBehaviour
    {
        public GameObject screen;
        public SelectModuleScreen selectModuleScreen;
        public TMP_Text info;
        //
        private GameId _gameId;
        private GameLanguage _language;

        void Start()
        {

        }

        public void Show(GameId gameId, GameLanguage language, string info)
        {
            this._gameId = gameId;
            this._language = language;
            this.info.text = info;
            screen.SetActive(true);
        }

        public void OnStartClicked()
        {
            screen.SetActive(false);
            //VM_LettersController.Instance.StartGame(_gameId, _language);
        }

        public void OnExitClicked()
        {
            screen.SetActive(false);
            selectModuleScreen.Show(true);
        }
    }
}