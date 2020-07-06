/*
    Author: Ghercioglo "Romeon0" Roman
 */

using Assets.Scripts.Framework.Other;
using Assets.Scripts.Games.Abstract;
using Assets.Scripts.Games.GameData.MultipleAnswersQuestion;
using Assets.Scripts.Games.RepeatLettersGame;
using Assets.Scripts.Games.TimeKillerGame;
using Assets.Scripts.LettersGames;
using System;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Games.HistoryGame
{
    [Serializable]
    public class ControllerHistory : SingleInstanceObject<ControllerHistory>,
        ControllerAbstract, IAdvancingGame
    {
        //Vars
        private Database _database;
        protected ControllerGlobal _globalController;
        //getters & setters
        [SerializeField] public ModelHistory Model;
        [SerializeField] public ViewHistory View;
        public bool EnableTimer { get; set; }
        public int HintsUsed { get; set; }
        public int Attempts { get; set; }
        //events
        public Action<GameEndReason> GameFinished;

        protected void Start()
        {
            _database = Database.Instance;
            EnableTimer = false;
            _globalController = ControllerGlobal.Instance;
        }

        protected MultipleAnswersQuestion GetData()
        {
            _database = Database.Instance;
            EnableTimer = false;
            _globalController = ControllerGlobal.Instance;

            MultipleAnswersQuestion allData = null;
            MultipleAnswersQuestion data = null;
            int min=0, max=0;
            if (_globalController.currDifficulty == GameDifficulty.Welcome)
            {
                allData = Resources.Load<MultipleAnswersQuestion>("GameData/Questions_History_Easy");
                min = 0; max = allData.questions.Length / 2;
            }
            else if (_globalController.currDifficulty == GameDifficulty.Easy)
            {
                allData = Resources.Load<MultipleAnswersQuestion>("GameData/Questions_History_Easy");
                min = allData.questions.Length / 2; max = allData.questions.Length - 1;
            }
            else if (_globalController.currDifficulty == GameDifficulty.NotSoEasy)
            {
                allData = Resources.Load<MultipleAnswersQuestion>("GameData/Questions_History_Medium");
                min = 0; max = allData.questions.Length/2;
            }
            else if (_globalController.currDifficulty == GameDifficulty.Medium)
            {
                allData = Resources.Load<MultipleAnswersQuestion>("GameData/Questions_History_Medium");
                min = allData.questions.Length / 2; max = allData.questions.Length - 1;
            }
            else if (_globalController.currDifficulty == GameDifficulty.Hard)
            {
                allData = Resources.Load<MultipleAnswersQuestion>("GameData/Questions_History_Hard");
                min = 0; max = allData.questions.Length - 1;
            }
            else
                Debug.LogError("Unknown difficulty!");

            data = ScriptableObject.CreateInstance<MultipleAnswersQuestion>();
            data.questions = allData.questions.Slice(min, max).ToArray();
            Debug.LogFormat("Difficulty: {0}; Took elements {1} from {2} to {3}",
                _globalController.currDifficulty, data.questions.Length, min, max);


            return data;
        }

        public void StartGame(Action<bool, bool> callback)
        {
            MultipleAnswersQuestion data = GetData();

            Model.SetDictionary(data);
            Model.Create();
            View.Create(Model);

            HintsUsed = 0;
            Attempts = 0;
            View.StartGame(() =>
            {
                Model.StartGame();
                callback?.Invoke(EnableTimer, false);
            });
        }

        public bool Advance()
        {
            bool canAdvance = Model.Advance();
            if (canAdvance)
                View.Advance();
            return canAdvance;
        }

        public void StopGame()
        {
            View.StopGame();
            Model.StopGame();
        }


        public bool Hint()
        {
            bool used = View.Hint();
            if (used)
                HintsUsed += 1;
            return used;
        }

        ModelAbstract ControllerAbstract.GetModel()
        {
            return Model;
        }

        ViewAbstract ControllerAbstract.GetView()
        {
            return View;
        }
    }
}
