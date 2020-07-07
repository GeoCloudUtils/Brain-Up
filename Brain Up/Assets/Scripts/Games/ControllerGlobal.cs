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
using Assets.Scripts.Games.CapitalsGame;
using Assets.Scripts.Games.GameData.DifficultyData;
using Assets.Scripts.Games.HistoryGame;

namespace Assets.Scripts.Games
{
    [Serializable]
    public enum GameId
    {
        RepeatLetters = 0,
        RepeatColors = 1,
        Acknowledge_History = 2,
        SelectFlag = 3,
        Acknowledge_Countries = 4,
        GuessWord = 5,
        TimeKiller = 6,
        Numbers_Sum = 7,
        Numbers_Product = 8,
        Numbers_Expression = 9,
    }

    public static class GameName
    {
        public static string GetById(GameId id)
        {
            switch (id)
            {
                case GameId.RepeatLetters: return "Repeat Letters";
                case GameId.RepeatColors: return "Repeat Colors";
                case GameId.Acknowledge_History: return "History";
                case GameId.SelectFlag: return "Select Flag";
                case GameId.Acknowledge_Countries: return "Countries";
                case GameId.GuessWord: return "Guess Word";
                case GameId.TimeKiller: return "TimeKiller";
                case GameId.Numbers_Sum: return "Sum";
                case GameId.Numbers_Product: return "Product";
                case GameId.Numbers_Expression: return "Expression";
                default: break;
            }
            return null;
        }
    }

    public static class GameDifficultyName
    {
        public static string GetById(GameDifficulty id)
        {
            switch (id)
            {
                case GameDifficulty.Welcome: return "Welcome";
                case GameDifficulty.Easy: return "Easy";
                case GameDifficulty.NotSoEasy: return "Not so Easy";
                case GameDifficulty.Medium: return "Medium";
                case GameDifficulty.Hard: return "Hard";
                case GameDifficulty.Any: return "Any";
                default: break;
            }
            return null;
        }
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
        Any=-1,
        Welcome=0,
        Easy = 1,
        NotSoEasy=2,
        Medium = 3,
        Hard = 4
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

    [Serializable]
    public enum Continent
    {
        Any = 0,
        America = 1,
        Europe = 2,
        Australia = 3,
        Africa = 4,
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
        public GameDifficulty currDifficulty = GameDifficulty.Easy;
        public Continent currContinent = Continent.Any;
        public int currProgress = 0;
        //
        private bool _gameRunning = false;
        private bool timerEnabled = false;
        private Database _database;
        private DifficultyData levelsData;
        //getters & setters
        public int HintsUsed { get; set; }
        public int Attempts { get; set; }
        [SerializeField] public int COINS_FOR_AD;
        [SerializeField] public int TIME_FOR_AD;

        //events
        public Action<GameEndReason> GameFinished;
        public Action<bool, bool> GameStarted;

        private ControllerAbstract _currController;


        //Classes
        public class Controllers
        {
            public static ControllerGuessWord vmLetters;
            public static ControllerRepeatColors vmColors;
            public static ControllerRepeatLetters ackHistory;
            public static ControllerSelectFlag ackFlags;
            public static ControllerTimeKiller ackTimeKiller;
        }

        private new void Awake()
        {
            base.Awake();
            levelsData = Resources.Load<DifficultyData>("GameData/LevelsData");
        }

        private void Start()
        {
            _database = Database.Instance;

            //TMP> Reset progress
            _database.Coins = 30;
            _database.Hints = 0;
            for (int a = 0; a < 10; ++a)
                _database.SetGameProgress(a, 1);
            _database.BoughtItems.Clear();
            int[] diffs = (int[])typeof(GameDifficulty).GetEnumValues();
            int[] games = (int[])typeof(GameId).GetEnumValues();
            for(int a=0; a < games.Length; ++a)
            {
                for (int b = 0; b < diffs.Length; ++b)
                {
                    if (diffs[b] < 0) continue;

                    int progress = -1;
                    if (b == 0)
                        progress = 0;
                    _database.SetGameProgressForDifficulty(games[a], diffs[b], progress);
                }
            }
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


        public void StartGame(GameId gameId, GameLanguage gameLanguage = GameLanguage.English)
        {
            Debug.Log("GlobalController: Starting game... GameId: " + gameId);
            
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
                case GameId.Acknowledge_History:
                    _currController = ControllerHistory.Instance; break;
                case GameId.Acknowledge_Countries:
                    _currController = ControllerCapitals.Instance; break;
                case GameId.Numbers_Sum:
                    _currController = ControllerNumbersSum.Instance; break;
                case GameId.Numbers_Product:
                    _currController = ControllerNumbersProduct.Instance; break;
                case GameId.Numbers_Expression:
                    _currController = ControllerNumbersExpression.Instance; break;
            }


            currGameId = gameId;
            currGameLanguage = gameLanguage;

            _currController.GetModel().GameId = gameId;
            GameScreenGlobal.Instance._lastScreen = _currController.GetView().GetScreen() ;

            Action<bool,bool> action = (enableTimer, enableCheckbar) =>
            {
                var global = ControllerGlobal.Instance;
                Debug.LogFormat("Game Started. EnableTimer: {0}; GameId: {1}; Diff: {2}",
                    enableTimer, 
                    global.currGameId,
                    global.currDifficulty);
                if (enableTimer)
                {
                    global.time = global.model.gameDuration;
                    global._gameRunning = true;
                }
                timerEnabled = enableTimer;
                global.GameStarted?.Invoke(enableTimer, enableCheckbar);
            };
            _currController.StartGame(action);
        }

        internal void Prolong(Action<bool> resultCallback)
        {
            Action<bool> globalCallback = (watched) =>
            {
                Debug.Log("Watched Rewarded Ad? " + watched);
                if (watched == true)
                {
                    GameScreenGlobal.Instance._lastScreen.Show(true);
                    time += timeForWatchAd;
                    _gameRunning = true;
                }
                resultCallback.Invoke(watched);
            };

            WatchAd(globalCallback);
        }

        internal void RestartGame()
        {
            _gameRunning = false;
            StartGame(currGameId, currGameLanguage);
        }

        internal void StopGame(GameEndReason reason)
        {
            GameScreenGlobal.Instance.Show(false);
            currProgress = 0;
            _gameRunning = false;
            if(_currController!=null)
                _currController.StopGame();
            GameFinished?.Invoke(reason);
        }

        internal bool Advance()
        {
            
            Type type = _currController.GetType().GetInterface(nameof(IAdvancingGame));
            if(type == null)
                throw new UnityException();

            if (type == null)
            {
                Debug.LogWarning(_currController.GetType() + " have not IAdvancingGame interface!");
                return false;
            }

            bool canAdvance = ((IAdvancingGame)_currController).Advance();

            if (!canAdvance)
            {
                StopGame(GameEndReason.Exit);
                if (currDifficulty != GameDifficulty.Hard)//Unlock next stage
                {
                    Database.Instance.SetGameProgressForDifficulty(
                        (int)currGameId,
                        (int)currDifficulty + 1, 0);
                }
                GameScreenGlobal.Instance.ShowDiffLevelFinishedScreen(GameName.GetById(currGameId), GameDifficultyName.GetById(currDifficulty));
            }
            else
            {
                ++currProgress;

                int last = 0;
                if (currDifficulty == GameDifficulty.Any)
                {
                    last = Database.Instance.GetGameProgress((int)currGameId);
                    if (currProgress > last)
                        Database.Instance.SetGameProgress((int)currGameId, currProgress);
                }
                else
                {
                    last = Database.Instance.GetGameProgressForDifficulty((int)currGameId, (int)currDifficulty);
                    if (currProgress > last)
                        Database.Instance.SetGameProgressForDifficulty((int)currGameId, (int)currDifficulty, currProgress);
                }
            }
           


            return canAdvance;
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
            GameScreenGlobal.Instance.ShowLoadingAdScreen(true);
            callback += (watched) =>
            {
                GameScreenGlobal.Instance.ShowLoadingAdScreen(false);
            };

            GoogleAdmobModel.Instance.ShowRewarded(callback);
        }

        public void SetGameDifficulty(GameDifficulty difficulty)
        {
            currDifficulty = difficulty;
        }

        public void SetContinent(Continent continent)
        {
            currContinent = continent;
        }

        //private Dictionary<GameId, KeyValuePair<GameDifficulty,int>[]> MaxLevels = new Dictionary<GameId, KeyValuePair<GameDifficulty, int>>()
        //{
        //    { GameId., 2000 },
        //    { GameId.Acknowledge_History, 2000 },
        //    { GameId.Acknowledge_History, 2000 },
        //    { GameId.Acknowledge_History, 2000 },
        //    { GameId.Acknowledge_History, 2000 },
        //};

        internal int GetMaxLevel(GameId gameId, GameDifficulty difficulty)
        {
            foreach(var game in levelsData.rows)
                if (game.gameId == gameId)
                    foreach (var diff in game.data)
                    {
                        if(diff.difficulty == difficulty)
                            return diff.nrMaxLevels;
                    }
            Debug.LogWarningFormat("Data not found for game={0} with difficulty={1}", gameId, difficulty);
            return 0;
        }

        internal int GetMaxLevelForCurrGame()
        {
            return GetMaxLevel(currGameId, currDifficulty);
        }

        internal DifficultyDataRow GetLevelDataFor(GameId gameId)
        {
            foreach (var game in levelsData.rows)
                if (game.gameId == gameId)
                    return game;
            Debug.LogWarningFormat("Level Data not found for game={0}", gameId);
            return null;
        }

       
    }
}
