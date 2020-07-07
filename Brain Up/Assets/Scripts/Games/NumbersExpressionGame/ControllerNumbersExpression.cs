/*
    Author: Ghercioglo "Romeon0" Roman
 */

using Assets.Scripts.Framework.Other;
using Assets.Scripts.Games.Abstract;
using Assets.Scripts.Games.Other;
using System;
using UnityEngine;

namespace Assets.Scripts.Games.RepeatLettersGame
{
    public class ControllerNumbersExpression : SingleInstanceObject<ControllerNumbersExpression>,
        ControllerAbstract, ICheckingGame, IAdvancingGame
    {
        //Vars
        public ModelNumbersExpression Model;
        public ViewNumbersExpression View;
        //getters & setters
        public bool EnableTimer { get; set; }
        public int HintsUsed {get;set;}
        public int Attempts {get;set; }
        //events
        public Action<GameEndReason> GameFinished;


        public void StartGame(Action<bool, bool> callback)
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

        public void StopGame()
        {
            View.StopGame();
            Model.StopGame();
        }

        internal bool IsCurrentGameFinished()
        {
            return false;
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
