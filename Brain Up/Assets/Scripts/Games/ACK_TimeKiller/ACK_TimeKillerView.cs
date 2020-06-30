/*
    Author: Ghercioglo "Romeon0" Roman
    Desc: ACK - Acknowledge
 */

using Assets.Scripts.Games.Abstract;
using Assets.Scripts.Games.GameData.MultipleAnswersQuestion;
using Assets.Scripts.Games.VM_Letters;
using Assets.Scripts.LettersGames;
using Assets.Scripts.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Assets.Scripts.Games.ACK_TimeKiller
{
    public class ACK_TimeKillerView : AbstractView<ACK_TimeKillerModel>
    {
        private ACK_TimeKillerModel _model;
        public MultipleAnswersScreen gameScreen;

        public override void Create(ACK_TimeKillerModel model)
        {
            this._model = model;
        }

        public override bool Hint()
        {
            return false;
        }

        public override void StartGame(Action endCallback = null)
        {
            Question data = _model.GetData();
            bool[] answers = new bool[data.answers.Length];
            answers[0] = true;

            data.answers.Shuffle((index_1, index_2) =>
            {
                answers.Swap(index_1, index_2);
            });

            for (int a=0; a < answers.Length; ++a)
                Debug.LogFormat("Answer {0}: {1}", a, answers[a]);

            gameScreen.InitScreen(data.answers, data.question, answers);
            gameScreen.SetProgress(_model.progress);

            gameScreen.Show(true);
        }

        public override void StopGame()
        {
            gameScreen.gameInProgress = false;
        }

        internal void Advance()
        {
           gameScreen.SetProgress(_model.progress);
        }
    }
}
