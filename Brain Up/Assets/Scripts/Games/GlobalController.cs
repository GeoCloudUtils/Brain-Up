/*
    Author: Ghercioglo "Romeon0" Roman
 */
using Assets.Scripts.Framework.Other;
using Assets.Scripts.Games.Abstract;
using Assets.Scripts.Games.ACK_Flags;
using Assets.Scripts.Games.ACK_History;
using Assets.Scripts.Games.ACK_TimeKiller;
using Assets.Scripts.Games.VM_Colors;
using Assets.Scripts.Games.VM_Letters;
using Assets.Scripts.Screens;
using System;
using UnityEngine;

namespace Assets.Scripts.Games
{
    [Serializable]
    public enum GameId
    {
        VisualMemory_Letters = 0,
        VisualMemory_Colors = 1,
        Acknowledge_History = 2,
        Acknowledge_Flags = 3,
        Acknowledge_Countries = 4,

        NonOrderedLetters = 5,
        GuessWord = 6,
        TimeKiller = 7,
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



    public class GlobalController : SingleInstanceObject<GlobalController>
    {
        public GlobalModel model;
        public GlobalView view;
        public float time;
        public float timeForWatchAd = 60f;
        public GameId currGameId;
        public GameLanguage currGameLanguage;
        //
        private bool _gameRunning = false;
        private Database _database;


        //getters & setters
        public int HintsUsed {get;set;}
        public int Attempts {get;set; }

        //events
        public Action<GameEndReason> GameFinished;
        public Action<bool,bool> GameStarted;

        //Classes
        private class Controllers
        {
            public static VM_LettersController vmLetters;
            public static VM_ColorsController vmColors;
            public static ACK_HistoryController ackHistory;
            public static ACK_FlagsController ackFlags;
            public static ACK_TimeKillerController ackTimeKiller;

            public static void Init()
            {
                vmLetters = VM_LettersController.Instance;
                vmColors = VM_ColorsController.Instance;
                ackHistory = ACK_HistoryController.Instance;
                ackFlags = ACK_FlagsController.Instance;
                ackTimeKiller = ACK_TimeKillerController.Instance;
            }

            public static void StartGame(GameId gameId)
            {
                dynamic controller = Controllers.vmLetters;
                switch (gameId)
                {
                    case GameId.VisualMemory_Letters:
                        controller = Controllers.vmLetters;
                        break;
                    case GameId.VisualMemory_Colors:
                        controller = Controllers.vmColors;
                        break;
                    case GameId.Acknowledge_History:
                        controller = Controllers.ackHistory;
                        break;
                    case GameId.Acknowledge_Flags:
                        controller = Controllers.ackFlags;
                        break;
                    case GameId.TimeKiller:
                        controller = Controllers.ackTimeKiller;
                        break;      
                }

                GameScreen.Instance._lastScreen = controller.view.gameScreen;

                Action<bool,bool> action = (enableTimer, enableCheckbar) =>
                {
                    var global = GlobalController.Instance;
                    Debug.LogWarning("Game Started. Enable timer: " + enableTimer);
                    if (enableTimer)
                    {
                        global.time = global.model.gameDuration;
                        global._gameRunning = true;
                    }
                    global.GameStarted?.Invoke(enableTimer, enableCheckbar);
                };
                controller.StartGame(action);
            }

            internal static void StopGame(GameId currGameId, GameEndReason reason)
            {
                switch (currGameId)
                {
                    case GameId.VisualMemory_Letters:
                        Controllers.vmLetters.StopGame();
                        break;
                    case GameId.VisualMemory_Colors:
                        Controllers.vmColors.StopGame();
                        break;
                    case GameId.Acknowledge_History:
                        Controllers.ackHistory.StopGame();
                        break;
                    case GameId.Acknowledge_Flags:
                        Controllers.ackFlags.StopGame();
                        break;
                    case GameId.TimeKiller:
                        Controllers.ackTimeKiller.StopGame();
                        break;
                }

            }

            internal static bool Hint(GameId currGameId)
            {
                bool used = false;
                switch (currGameId)
                {
                    case GameId.VisualMemory_Letters:
                        used = Controllers.vmLetters.Hint();
                        break;
                    case GameId.VisualMemory_Colors:
                        used = Controllers.vmColors.Hint();
                        break;
                    case GameId.Acknowledge_History:
                        used = Controllers.ackHistory.Hint();
                        break;
                    case GameId.Acknowledge_Flags:
                        used = Controllers.ackFlags.Hint();
                        break;
                    case GameId.TimeKiller:
                        used = Controllers.ackTimeKiller.Hint();
                        break;
                }
                return used;
            }

            internal static bool IsFinished(GameId currGameId)
            {
                bool finished = false;
                switch (currGameId)
                {
                    case GameId.VisualMemory_Letters:
                        finished = Controllers.vmLetters.IsCurrentGameFinished();
                        break;
                    case GameId.VisualMemory_Colors:
                        finished = Controllers.vmColors.IsCurrentGameFinished();
                        break;
                    case GameId.Acknowledge_History:
                        finished = Controllers.ackHistory.IsCurrentGameFinished();
                        break;
                    case GameId.Acknowledge_Flags:
                        finished = Controllers.ackFlags.IsCurrentGameFinished();
                        break;
                    case GameId.TimeKiller:
                        finished = Controllers.ackTimeKiller.IsCurrentGameFinished();
                        break;
                }
                return finished;
            }

            internal static bool Check(GameId currGameId)
            {
                bool finished = false;
                switch (currGameId)
                {
                    case GameId.VisualMemory_Letters:
                        finished = Controllers.vmLetters.Check();
                        break;
                    case GameId.VisualMemory_Colors:
                        finished = Controllers.vmColors.Check();
                        break;
                    case GameId.Acknowledge_History:
                        finished = Controllers.ackHistory.Check();
                        break;
                    case GameId.Acknowledge_Flags:
                        finished = Controllers.ackFlags.Check();
                        break;
                    case GameId.TimeKiller:
                        finished = Controllers.ackTimeKiller.Check();
                        break;
                }
                return finished;
            }

            internal static bool Advance(GameId currGameId)
            {
                bool canAdvance = false;
                switch (currGameId)
                {
                    case GameId.Acknowledge_Flags:
                        canAdvance = Controllers.ackFlags.Advance();
                        break;
                    case GameId.TimeKiller:
                        canAdvance = Controllers.ackTimeKiller.Advance();
                        break;
                }

                return canAdvance;
            }
        }



        private void Start()
        {
            _database = Database.Instance;
            Controllers.Init();

            //TMP> Reset progress
            _database.Coins = 30;
            _database.Hints = 0;
            for(int a=0; a < 10; ++a)
                _database.SetGameProgress(a, 1);
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
            return Controllers.Check(currGameId);
        }

        public bool IsCurrModuleFinished()
        {
            return Controllers.IsFinished(currGameId);
        }


        public void StartGame(GameId gameId, GameLanguage gameLanguage = GameLanguage.None)
        {
            Debug.Log("GlobalController: Starting game...");
            currGameId = gameId;
            currGameLanguage = gameLanguage;


            Controllers.StartGame(gameId);
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

            Controllers.StopGame(currGameId, reason);

            GameFinished?.Invoke(reason);
        }

        internal bool Advance()
        {
            return Controllers.Advance(currGameId);
        }

        internal void Pause(bool pause)
        {
            _gameRunning = !pause;
        }

        internal bool Hint()
        {
            bool used = Controllers.Hint(currGameId);

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
