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
        ControllerAbstract, ICheckingGame
    {
        //Vars
        protected Database _database;
        //getters & setters
        public bool EnableTimer { get; set; }
        public ModelGuessWord Model;
        public ViewGuessWord View;
        public int HintsUsed {get;set;}
        public int Attempts {get;set; }

        //events
        public Action<GameEndReason> GameFinished;


        private void Start()
        {
            _database = Database.Instance;
            EnableTimer = true;
        }

        public void StartGame(Action<bool,bool> callback)
        {
            Model.Create();
            View.Create(Model);

            HintsUsed = 0;
            Attempts = 0;
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

        public bool Check()
        {
            if (View.IsWordCorrect())
            {
                Debug.Log("Is correct!");
                int id = (int)GameId.RepeatLetters;
                int progress = _database.GetGameProgress(id);
                _database.SetGameProgress(id, progress + 1);
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
            return Model.GetRemainedWordsCount()==0;
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
    }
}
