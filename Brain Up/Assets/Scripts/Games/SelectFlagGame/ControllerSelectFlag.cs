/*
    Author: Ghercioglo "Romeon0" Roman
 */

using Assets.Scripts.Framework.Other;
using Assets.Scripts.Games.Abstract;
using System;
using UnityEngine;

namespace Assets.Scripts.Games.SelectFlagGame
{
    public class ControllerSelectFlag : SingleInstanceObject<ControllerSelectFlag>,
        ControllerAbstract, IAdvancingGame
    {
        //getters & setters
        public bool EnableTimer { get; set; }
        public ModelSelectFlag Model;
        public ViewSelectFlag View;
        public int HintsUsed {get;set;}
        public int Attempts {get;set; }
        //events
        public Action<GameEndReason> GameFinished;


        private void Start()
        {
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
