/*
    Author: Ghercioglo "Romeon0" Roman
    Desc: ACK - Acknowledge
 */

using Assets.Scripts.Framework.Other;
using Assets.Scripts.Games;
using Assets.Scripts.Games.Abstract;
using Assets.Scripts.Games.Gamedata.TripleValueList;
using Assets.Scripts.Games.GameData;
using Assets.Scripts.Games.GameData.MultipleAnswersQuestion;
using Assets.Scripts.Games.GameData.Question_;
using Assets.Scripts.Games.TimeKillerGame;
using Assets.Scripts.LettersGames;
using Assets.Scripts.Screens;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Games.CapitalsGame
{
    [Serializable]
    public class ViewCapitals : SingleInstanceObject<ViewCapitals>,
        ViewAbstract
    {

        //Vars
        public GameScreenMultipleAnswers gameScreen;
        //Properties
        public ModelCapitals Model;




        public void Create(ModelCapitals model)
        {
            this.Model = model;
        }

        public ModelCapitals GetModel()
        {
            return Model;
        }

        public GameScreenAbstract GetScreen()
        {
            return gameScreen;
        }

        public bool Hint()
        {
            return false;
        }



        public void StartGame(Action endCallback = null)
        {
            Question data = Model.GetData();
            bool[] answers = new bool[data.answers.Length];
            answers[0] = true;

            data.answers.Shuffle((index_1, index_2) =>
            {
                answers.Swap(index_1, index_2);
            });

            for (int a = 0; a < answers.Length; ++a)
                Debug.LogFormat("Answer {0}: {1}", a, answers[a]);

            gameScreen.InitScreen(data.answers, data.question, answers);
            gameScreen.SetProgress(Model.progress, ControllerGlobal.Instance.GetMaxLevelForCurrGame());
            gameScreen.Show(true);

            endCallback.Invoke();
        }

        public void StopGame()
        {
            gameScreen.gameInProgress = false;
        }

        internal void Advance()
        {
            gameScreen.SetProgress(Model.progress+1, ControllerGlobal.Instance.GetMaxLevelForCurrGame());
        }

        ModelAbstract ViewAbstract.GetModel()
        {
            return Model;
        }

        public void SetModel(ModelAbstract model)
        {
            Model = (ModelCapitals)model;
        }
    }
}
