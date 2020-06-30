/*
    Author: Ghercioglo Roman
 */

using Assets.Scripts.Framework.Other;
using Assets.Scripts.Games;
using Scripts.Extensions;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Screens
{
    class GameScreenGlobal : SingleInstanceObject<GameScreenGlobal>
    {
        [Header("References")]
        public GameObject screen=null;
        public ScreenSelectModule selectModuleScreen = null;
        public TMP_Text hintsCount = null;
        public TMP_Text coinsCount = null;
        public GameObject timerParent = null;
        public GameObject checkbar = null;
        public DialogWin winScreen = null;
        public DialogNoAttempts noAttemptsScreen = null;
        public DialogNoTime noTimeScreen = null;
        public DialogNoHints noHintsScreen = null;
        public DialogNoCoins noCoinsScreen = null;
        //
        protected Database _database;
        protected ControllerGlobal globalController;
        protected int gameId = -1;
        public GameScreenAbstract _lastScreen;



        protected void Start()
        {
            if (selectModuleScreen == null) Debug.LogError(nameof(selectModuleScreen) + " is not assigned!");
            if (hintsCount == null) Debug.LogError(nameof(hintsCount) + " is not assigned!");
            if (winScreen == null) Debug.LogError(nameof(winScreen) + " is not assigned!");

            globalController = ControllerGlobal.Instance;
            _database = Database.Instance;

            //connect to events
            _database.onHintsCountChanged += OnHintsCountChanged;
            _database.onCoinsCountChanged += OnCoinsCountChanged;
            globalController.GameFinished += OnGameFinished;
            globalController.GameStarted += OnGameStarted;

            //update interface
            OnHintsCountChanged(0, _database.Hints);
            OnCoinsCountChanged(0, _database.Coins);
        }

        public void Show(bool show)
        {
            screen.SetActive(show);
            if (_lastScreen != null && !show)
                _lastScreen.Show(false);
        }

        public void ShowOnlyScreen(bool show)
        {
            screen.SetActive(show);
        }

        public void OnCheckClicked()
        {
            if (globalController.Check() == true)
            {
                globalController.StopGame(GameEndReason.Win);
            }
        }

        public void OnGameListClicked()
        {
            if(_lastScreen != null)
                _lastScreen.Show(false);
            globalController.StopGame(GameEndReason.Exit);
        }

        #region Events

        private void OnHintsCountChanged(int oldCount, int newCount)
        {
            hintsCount.text = newCount > 99 ? "99+" : newCount.ToString();
        }
        private void OnCoinsCountChanged(int oldCount, int newCount)
        {
            coinsCount.text = newCount.ToString();
        }


        protected void OnGameFinished(GameEndReason reason)
        {
            if (reason == GameEndReason.Win)
            {
                if (!globalController.IsCurrModuleFinished())
                {
                    int points = 100;
                    points -= globalController.Attempts * 10;
                    points -= globalController.HintsUsed * 5;
                    points = points.Clamp(10, 100);
                    winScreen.Show(points, 100, 45, hints: globalController.HintsUsed);
                }
            }
            else if (reason == GameEndReason.NoTime)
            {
                noTimeScreen.Show(true);
            }
            else if (reason == GameEndReason.NoAttempts)
                noAttemptsScreen.Show(true);
            else if (reason == GameEndReason.Exit)
            {
                selectModuleScreen.Show(true);
                screen.SetActive(false);
            }
            else if (reason == GameEndReason.NoCoins)
            {
                noCoinsScreen.Show(true);
            }
        }

        protected void OnGameStarted(bool enableTimer, bool enableCheckbar)
        {
            GameId gameId = globalController.currGameId;

            timerParent.SetActive(enableTimer);
            checkbar.SetActive(enableCheckbar);

            screen.SetActive(true);
        }
        #endregion

    }
}
