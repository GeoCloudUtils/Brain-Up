/*
    Author: Ghercioglo "Romeon0" Roman
 */

using Assets.Scripts.Framework.Other;
using Assets.Scripts.Games.Abstract; 
using Assets.Scripts.Games.GameData.MultipleAnswersQuestion;
using Assets.Scripts.Games.RepeatLettersGame;
using System;
using UnityEngine;

namespace Assets.Scripts.Games.TimeKillerGame
{
    [Serializable]
    public class ControllerTimeKiller : SingleInstanceObject<ControllerTimeKiller>,
        ControllerAbstract, IAdvancingGame
    {
       //Vars
        private Database _database;
        protected ControllerGlobal _globalController;
        private MultipleAnswersQuestion allData;
        //getters & setters
        [SerializeField] public ModelTimeKiller Model;
        [SerializeField] public ViewTimeKiller View;
        public bool EnableTimer { get; set; }
        public int HintsUsed {get;set;}
        public int Attempts {get;set; }
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
            if(allData==null)
                allData = Resources.Load<MultipleAnswersQuestion>("GameData/Questions_General");
            return allData;
        }

        public void StartGame(Action<bool,bool> callback)
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

        internal void RestartGame()
        {
            StartGame(null);
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
