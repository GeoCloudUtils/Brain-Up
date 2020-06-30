/*
    Author: Ghercioglo "Romeon0" Roman
    Desc: ACK - Acknowledge
 */

using Assets.Scripts.Games.Abstract;
using Assets.Scripts.Games.VM_Letters;
using Assets.Scripts.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Assets.Scripts.Games.ACK_Flags
{
    public class ACK_FlagsView : AbstractView<ACK_FlagsModel>
    {
        private ACK_FlagsModel _model;
        public CardsGameScreen gameScreen;

        public override void Create(ACK_FlagsModel model)
        {
            this._model = model;
        }

        public override bool Hint()
        {
            return false;
        }

        public override void StartGame(Action endCallback = null)
        {
            List<Tuple<int, Sprite, string>> data = _model.GetData();
            bool[] answers = new bool[data.Count];

            gameScreen.SetProgress(_model.progress, _model.questionsCount);

            int correctIndex = new System.Random().Next(0, data.Count);
            answers[correctIndex] = true;

            Sprite[] flags = new Sprite[data.Count]; 
            for (int a = 0; a < data.Count; ++a)
                flags[a] = data[a].Item2;

            string question = "What is the flag of " + data[correctIndex].Item3 + "?";
            gameScreen.InitScreen(flags, question, answers);
            gameScreen.Show(true);
            endCallback?.Invoke();
        }

        public override void StopGame()
        {
            
        }

        internal void Advance()
        {
            gameScreen.SetProgress(_model.progress, _model.questionsCount);
        }
    }
}
