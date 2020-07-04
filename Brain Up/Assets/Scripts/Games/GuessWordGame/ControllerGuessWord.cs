/*
    Author: Ghercioglo "Romeon0" Roman
 */
using Assets.Scripts.Framework.Other;
using Assets.Scripts.Games.Abstract;
using System;
using UnityEngine;

namespace Assets.Scripts.Games.GuessWordGame
{
    public class ControllerGuessWord : SingleInstanceObject<ControllerGuessWord>,
        ControllerAbstract, ICheckingGame, IAdvancingGame
    {
        //Vars
        private Database _database;
        //getters & setters
        public bool EnableTimer { get; set; }
        public ModelGuessWord Model;
        public ViewGuessWord View;
        public int HintsUsed {get;set;}
        public int Attempts {get;set; }
        ModelAbstract ControllerAbstract.GetModel() => Model;
        ViewAbstract ControllerAbstract.GetView() => View;


        private void Start()
        {
            _database = Database.Instance;
            EnableTimer = true;
        }

        public void StartGame(Action<bool,bool> callback)
        {
            HintsUsed = 0;
            Attempts = 0;

            Model.Create();
            View.Create(Model);

            View.StartGame(() =>
              {
                  Model.StartGame();
                  callback?.Invoke(EnableTimer, true);
              });
        }

        internal void RestartGame()
        {
            StartGame(null);
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

        public bool Check()
        {
            if (View.IsWordCorrect())
            {
                Debug.Log("Is correct!");
                int id = (int)ControllerGlobal.Instance.currGameId;
                return true;
            }
            else
            {
                Debug.Log("Is NOT correct!");
                Attempts += 1;
                return false;
            }
        }

        public bool Advance()
        {
            bool canAdvance = Model.Advance();
            if (canAdvance)
                View.Advance();
            return canAdvance;
        }

       

    }
}
