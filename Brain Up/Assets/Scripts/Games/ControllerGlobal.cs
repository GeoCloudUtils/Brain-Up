/*
    Author: Ghercioglo "Romeon0" Roman
 */
using Assets.Scripts.Framework.Other;
using Assets.Scripts.Games.Abstract;
using Assets.Scripts.Games.SelectFlagGame;
using Assets.Scripts.Games.GuessWordGame;
using Assets.Scripts.Games.TimeKillerGame;
using Assets.Scripts.Games.RepeatColorsGame;
using Assets.Scripts.Games.RepeatLettersGame;
using Assets.Scripts.Screens;
using System;
using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.Games
{
    [Serializable]
    public enum GameId
    {
        RepeatLetters = 0,
        RepeatColors = 1,
        SelectFlag = 3,
        GuessWord = 5,
        TimeKiller = 6,
        Acknowledge_History = 2,
        Acknowledge_Countries = 4,

    }

    [Serializable]
    public enum GameModule
    {
        VisualMemory = 0,
        Acknowledge = 1,
    }

    [Serializable]
    public enum GameLanguage
    {
        None = 0,
        English = 1,
        Russian = 2,
        Romanian = 3,
    }

    [Serializable]
    public enum GameDifficulty
    {
        None = 0,
        Easy = 1,
        Medium = 2,
        Hard = 3,
    }

    [Serializable]
    public enum GameEndReason
    {
        Exit = 1,
        NoTime = 2,
        Win = 3,
        NoAttempts = 4,
        NoCoins = 5
    }



    public class ControllerGlobal : SingleInstanceObject<ControllerGlobal>
    {
        //Vars
        public ModelGlobal model;
        public ViewGlobal view;
        public float time;
        public float timeForWatchAd = 60f;
        public GameId currGameId;
        public GameLanguage currGameLanguage;
        //
        private bool _gameRunning = false;
        private bool timerEnabled = false;
        private Database _database;
        //getters & setters
        public int HintsUsed { get; set; }
        public int Attempts { get; set; }
        //events
        public Action<GameEndReason> GameFinished;
        public Action<bool, bool> GameStarted;

        private ControllerAbstract _currController;

        public class I
        {

        }

        public class A : I
        {
            public I Hello() { return this; }
        }

        public class B : A
        {
            public new I Hello() { return this; }
        }


        //Classes
        public class Controllers
        {
            public static ControllerGuessWord vmLetters;
            public static ControllerRepeatColors vmColors;
            public static ControllerRepeatLetters ackHistory;
            public static ControllerSelectFlag ackFlags;
            public static ControllerTimeKiller ackTimeKiller;
        }


        private void Start()
        {
            _database = Database.Instance;

            //TMP> Reset progress
            _database.Coins = 30;
            _database.Hints = 0;
            for (int a = 0; a < 10; ++a)
                _database.SetGameProgress(a, 1);

            //  AddControllerForGame(GameId.Acknowledge_Countries, (ControllerAbstract<ModelAbstract, ViewAbstract<ModelAbstract>>)ControllerRepeatColors.Instance);
            //  AddControllerForGame(GameId.Acknowledge_History, (ControllerAbstract<ModelAbstract, ViewAbstract<ModelAbstract>>)ControllerRepeatColors.Instance);

           
        }


        private void Update()
        {
            if (!_gameRunning) return;

            time -= Time.deltaTime;
            if (Time.frameCount % 20 == 0)
            {
                if (time <= 0)
                {
                    time = 0;
                    view.UpdateTimer(time);
                    StopGame(GameEndReason.NoTime);
                }
                else
                    view.UpdateTimer(time);
            }
        }


        internal bool Check()
        {
            return ((ICheckingGame)_currController).Check();
        }

        public bool IsCurrModuleFinished()
        {
            return false;
        }


        public void StartGame(GameId gameId, GameLanguage gameLanguage = GameLanguage.None)
        {
            Debug.Log("GlobalController: Starting game...");
            
            switch (gameId){
                case GameId.SelectFlag: 
                    _currController = ControllerSelectFlag.Instance; break;
                case GameId.GuessWord:
                    _currController = ControllerGuessWord.Instance; break;
                case GameId.TimeKiller:
                    _currController = ControllerTimeKiller.Instance; break;
                case GameId.RepeatColors:
                    _currController = ControllerRepeatColors.Instance; break;
                case GameId.RepeatLetters:
                    _currController = ControllerRepeatLetters.Instance; break;
            }


            currGameId = gameId;
            currGameLanguage = gameLanguage;

            GameScreenGlobal.Instance._lastScreen = _currController.GetView().GetScreen() ;

            Action<bool,bool> action = (enableTimer, enableCheckbar) =>
            {
                var global = ControllerGlobal.Instance;
                Debug.LogWarning("Game Started. Enable timer: " + enableTimer);
                if (enableTimer)
                {
                    global.time = global.model.gameDuration;
                    global._gameRunning = true;
                }
                timerEnabled = enableTimer;
                global.GameStarted?.Invoke(enableTimer, enableCheckbar);
            };
            _currController.StartGame(action);

            Debug.Log("GlobalController: Game started.");
        }

        internal void Prolong(Action<bool> resultCallback)
        {
            resultCallback += (watched) =>
            {
                if (watched == true)
                {
                    time += timeForWatchAd;
                    _gameRunning = true;
                }
            };

            WatchAd(resultCallback);
        }

        internal void RestartGame()
        {
            _gameRunning = false;
            StartGame(currGameId, currGameLanguage);
        }

        internal void StopGame(GameEndReason reason)
        {
            _gameRunning = false;
            _currController.StopGame();
            GameFinished?.Invoke(reason);
        }

        internal bool Advance()
        {
            return ((IAdvancingGame)_currController).Advance();
        }

        internal void Pause(bool pause)
        {
            if(timerEnabled)
                _gameRunning = !pause;
        }

        internal bool Hint()
        {
            bool used = _currController.Hint();


            if (used)
                HintsUsed += 1;
            return used;
        }

        internal void WatchAd(Action<bool> callback)
        {
            //TODO Show Ad
            bool result = true;


            Debug.Log("Ad watched result: " + result);
            callback?.Invoke(result);
        }
    }
}
