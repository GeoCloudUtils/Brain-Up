/*
    Author: Ghercioglo "Romeon0" Roman
 */

using Assets.Scripts.Framework.Other;
using Assets.Scripts.Games.Abstract;
using Assets.Scripts.Games.RepeatLettersGame;
using System;
using UnityEngine;

namespace Assets.Scripts.Games.TimeKillerGame
{
    public class ControllerTimeKiller : SingleInstanceObject<ControllerTimeKiller>,
        ControllerAbstract<ModelTimeKiller, ViewTimeKiller>
    {
       //Vars
        private Database _database;
        //getters & setters
        public ModelTimeKiller Model { get; set; }
        public ViewTimeKiller View { get; set; }
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

        public void StartGame(Action<bool,bool> callback)
        {
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

        internal bool Advance()
        {
            bool canAdvance = Model.Advance();
            if (canAdvance)
                View.Advance();
            return canAdvance;
        }
    }
}
