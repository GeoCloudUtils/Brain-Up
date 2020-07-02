/*
    Author: Ghercioglo "Romeon0" Roman
 */

using Assets.Scripts.Framework.Other;
using Assets.Scripts.Games.Abstract;
using Assets.Scripts.Games.GameData.MultipleAnswersQuestion;
using Assets.Scripts.Games.RepeatLettersGame;
using Assets.Scripts.Games.TimeKillerGame;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Games.CapitalsGame
{
    [Serializable]
    public class ControllerCapitals : SingleInstanceObject<ControllerCapitals>,
        ControllerAbstract, IAdvancingGame
    {
        //Vars
        public ModelCapitals Model;
        public ViewCapitals View;
        private Dictionary<Continent, string> continents = new Dictionary<Continent, string>();
        private Database _database;
        protected ControllerGlobal _globalController;
        //getters & setters
        public bool EnableTimer { get; set; }
        public int HintsUsed { get; set; }
        public int Attempts { get; set; }
        //events
        public Action<GameEndReason> GameFinished;

        private void Start()
        {
            _database = Database.Instance;
            EnableTimer = false;
            _globalController = ControllerGlobal.Instance;

            continents.Add(Continent.Africa, "Africa");
            continents.Add(Continent.America, "America");
            continents.Add(Continent.Australia, "Australia");
            continents.Add(Continent.Europe, "Europe");
        }

        

        protected MultipleAnswersQuestion GetData()
        {
            MultipleAnswersQuestion allContinentsData = Resources.Load<MultipleAnswersQuestion>("GameData/Capitals");

            MultipleAnswersQuestion data = ScriptableObject.CreateInstance<MultipleAnswersQuestion>();

            Continent continent = ControllerGlobal.Instance.currContinent;
            if (continent != Continent.Any)
            {
                List<Question> questions = new List<Question>();
                string currContinent = continents[continent];
                foreach (Question q in allContinentsData.questions)
                {
                    if (q.answers[1] == currContinent)
                        questions.Add(q);
                }
                data.questions = questions.ToArray();
            }
            else
            {
                data.questions = allContinentsData.questions;
            }

            return data;
        }

        public void StartGame(Action<bool, bool> callback)
        {
            Debug.Log("ControllerCapitals: StartGame start.");
            MultipleAnswersQuestion data = GetData();

            Model.SetData(data);
            Model.Create();
            View.Create(Model);

            HintsUsed = 0;
            Attempts = 0;
            View.StartGame(() =>
            {
                Model.StartGame();
                callback?.Invoke(EnableTimer, false);
            });
            Debug.Log("ControllerCapitals: StartGame end.");
        }

        internal void RestartGame()
        {
            StartGame(null);
        }

        internal void StopGame(GameEndReason reason)
        {
            Model.StopGame();
            View.StopGame();
            GameFinished?.Invoke(reason);
        }

        public bool Hint()
        {
            bool used = View.Hint();
            if (used)
                HintsUsed += 1;
            return used;
        }

        internal bool Check()
        {
            return false;
        }

        public void StopGame()
        {
            View.StopGame();
            Model.StopGame();
        }


        internal bool IsCurrentGameFinished()
        {
            return false;
        }

        public bool Advance()
        {
            bool canAdvance = Model.Advance();
            if (canAdvance)
                View.Advance();
            return canAdvance;
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
