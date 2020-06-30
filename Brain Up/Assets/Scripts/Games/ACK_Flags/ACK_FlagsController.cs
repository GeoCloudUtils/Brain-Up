/*
    Author: Ghercioglo "Romeon0" Roman
 */

using Assets.Scripts.Games.Abstract;
using Assets.Scripts.Games.VM_Letters;
using System;
using UnityEngine;

namespace Assets.Scripts.Games.ACK_Flags
{
    public class ACK_FlagsController : AbstractController<ACK_FlagsController>
    {
        public ACK_FlagsModel model;
        public ACK_FlagsView view;
        private Database _database;

        //getters & setters
        public int HintsUsed {get;set;}
        public int Attempts {get;set; }

        //events
        public Action<GameEndReason> GameFinished;


        private void Start()
        {
            _database = Database.Instance;
            enableTimer = false;
        }

        public new void StartGame(Action<bool,bool> callback)
        {
            model.Create();
            view.Create(model);

            HintsUsed = 0;
            Attempts = 0;
            view.StartGame(() =>
              {
                  model.StartGame();
                  callback?.Invoke(enableTimer, true);
              });
        }

        internal void RestartGame()
        {
            StartGame(null);
        }

        internal void StopGame(GameEndReason reason)
        {
            model.StopGame();
            view.StopGame();
            GameFinished?.Invoke(reason);
        }

        public new bool Hint()
        {
            bool used = view.Hint();
            if (used)
                HintsUsed += 1;
            return used;
        }

        internal bool Check()
        {
            return false;
        }

        internal bool IsCurrentGameFinished()
        {
            return false;
        }

        public new void StopGame()
        {
            view.StopGame();
            model.StopGame();
        }


        internal bool Advance()
        {
            bool canAdvance = model.Advance();
            Debug.Log("FlagsController::CanAdvance: " + canAdvance);
            if (canAdvance)
                view.Advance();
            return canAdvance;
        }
    }
}
