/*
    Author: Ghercioglo "Romeon0" Roman
 */
using Assets.Scripts.Framework.Other;
using Assets.Scripts.Games.Abstract;
using System;
using UnityEngine;

namespace Assets.Scripts.Games.RepeatColorsGame
{
    public class ControllerRepeatColors : SingleInstanceObject<ControllerRepeatColors>,
        ControllerAbstract, ICheckingGame, IAdvancingGame
    {
        //Vars
        private Database _database;
        //getters & setters
        public ModelRepeatColors Model;
        public ViewRepeatColors View;
        public bool EnableTimer { get; set; }
        public int HintsUsed {get;set;}
        public int Attempts {get;set; }
        //events
        public Action<GameEndReason> GameFinished;


        private void Start()
        {
            _database = Database.Instance;
            EnableTimer = false;
        }

        public void StartGame(Action<bool, bool> callback)
        {
            Model.Create();
            View.Create(Model);

            HintsUsed = 0;
            Attempts = 0;
            View.StartGame(() =>
              {
                  Model.StartGame();
                  callback?.Invoke(EnableTimer,true);
                  Debug.Log("Colors game started!");
              });
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
            Debug.Log("Colors game STOPPED!");
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
                return true;
            }
            else
            {
                Debug.Log("Is NOT correct!");
                Attempts += 1;
                return false;
            }
        }

        internal bool IsCurrentGameFinished()
        {
            return false;
        }

        public void StopGame()
        {
            View.StopGame();
            Model.StopGame();
        }

        ModelAbstract ControllerAbstract.GetModel()
        {
            return Model;
        }

        ViewAbstract ControllerAbstract.GetView()
        {
            return View;
        }

        public bool Advance()
        {
            bool canAdvance = Model.Advance();
            Debug.Log("Advancing... Can? " + canAdvance);
            if (canAdvance)
                View.Advance();
            return canAdvance;
        }
    }
}
