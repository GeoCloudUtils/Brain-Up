/*
    Author: Ghercioglo "Romeon0" Roman
 */
using Assets.Scripts.Games.Abstract;
using System;
using UnityEngine;

namespace Assets.Scripts.Games.VM_Letters
{
    public class VM_LettersController : AbstractController<VM_LettersController>
    {
        public VM_LettersModel model;
        public VM_LettersView view;
        protected Database _database;

        //getters & setters
        public int HintsUsed {get;set;}
        public int Attempts {get;set; }

        //events
        public Action<GameEndReason> GameFinished;


        private void Start()
        {
            _database = Database.Instance;
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
            if (view.IsWordCorrect())
            {
                Debug.Log("Is correct!");
                int id = (int)GameId.VisualMemory_Letters;
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
            return model.GetRemainedWordsCount()==0;
        }

        public new void StopGame()
        {
            view.StopGame();
            model.StopGame();
        }
    }
}
