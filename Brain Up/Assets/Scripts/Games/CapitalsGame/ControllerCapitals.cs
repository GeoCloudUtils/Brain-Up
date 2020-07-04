/*
    Author: Ghercioglo "Romeon0" Roman
 */

using Assets.Scripts.Framework.Other;
using Assets.Scripts.Games.Abstract;
using Assets.Scripts.Games.Gamedata.TripleValueList;
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
        private Dictionary<Continent, string> continentsNames = new Dictionary<Continent, string>();
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

            if (continentsNames.Count == 0)
            {
                continentsNames.Add(Continent.Africa, "Africa");
                continentsNames.Add(Continent.America, "America");
                continentsNames.Add(Continent.Australia, "Australia");
                continentsNames.Add(Continent.Europe, "Europe");
            }
        }

        

        protected TripleValueList GetData()
        {
            if (_database == null)
            {
                Start();
            }

            TripleValueList allContinentsData = Resources.Load<TripleValueList>("GameData/CountriesData");
            allContinentsData = allContinentsData.Clone();
            TripleValueList data = ScriptableObject.CreateInstance<TripleValueList>();
            var globalController = ControllerGlobal.Instance;
            Continent[] continents;
            if (globalController.currDifficulty == GameDifficulty.Welcome
                || globalController.currDifficulty == GameDifficulty.Easy)
                continents = new Continent[] { Continent.Europe };
            else if (globalController.currDifficulty == GameDifficulty.NotSoEasy
                || globalController.currDifficulty == GameDifficulty.Medium)
                continents = new Continent[] { Continent.Europe, Continent.America };
            else
                continents = null;

            Debug.Log("Getting Data for continents:");
            if (continents != null)
            {
                List<TripleValueListRow> rows = new List<TripleValueListRow>();
                string[] currContinents = new string[continents.Length];
                int counter = 0;
                foreach(Continent c in continents)
                {
                    currContinents[counter++] = continentsNames[c];
                    Debug.Log("-> " + continentsNames[c]);
                }

                foreach (TripleValueListRow q in allContinentsData.rows)
                {
                    foreach (string contName in currContinents)
                        if (q.item3 == contName)
                        {
                            rows.Add(q);
                            break;
                        }
                }
                data.rows = rows.ToArray();
            }
            else
            {
                Debug.Log("-> All continents.");
                data.rows = (TripleValueListRow[])allContinentsData.rows;
            }

            Debug.Log("-> Countries: " + data.rows.Length);

            return data;
        }

        public void StartGame(Action<bool, bool> callback)
        {
            Debug.Log("ControllerCapitals: StartGame start.");
            TripleValueList data = GetData();

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
