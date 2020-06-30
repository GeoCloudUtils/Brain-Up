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

        public void Show(GameModule category)
        {
            _lastCategory = categories[(int)category];
            _lastCategory.SetActive(true);
        }

        public void OnGameClicked(int gameId)
        {
            if (_lastCategory != null)
                _lastCategory.SetActive(false);
            ControllerGlobal.Instance.StartGame((GameId)gameId, GameLanguage.English);
            GameScreenGlobal.Instance.Show(true);
        }

        public void OnExitClicked()
        {
            if (_lastCategory != null)
                _lastCategory.SetActive(false);
            selectModuleScreen.Show(true);
        }
    }
}