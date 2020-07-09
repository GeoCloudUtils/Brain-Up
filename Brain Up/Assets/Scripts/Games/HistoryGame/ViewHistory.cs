﻿/*
    Author: Ghercioglo "Romeon0" Roman
    Desc: ACK - Acknowledge
 */

using Assets.Scripts.Framework.Other;
using Assets.Scripts.Games.Abstract;
using Assets.Scripts.Games.GameData.MultipleAnswersQuestion;
using Assets.Scripts.Games.GameData.Question_;
using Assets.Scripts.LettersGames;
using Assets.Scripts.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Assets.Scripts.Games.HistoryGame
{
    public class ViewHistory : SingleInstanceObject<ViewHistory>,
        ViewAbstract
    {
        //Vars
        public GameScreenMultipleAnswers gameScreen;
        //Properties
        public ModelHistory Model { get; protected set; }



        public void Create(ModelHistory model)
        {
            this.Model = model;
        }

        public ModelAbstract GetModel()
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

        public void SetModel(ModelAbstract model)
        {
            throw new NotImplementedException();
        }

        public void StartGame(Action endCallback = null)
        {
            Question data = Model.GetData();
            bool[] answers = new bool[data.answers.Length];
            answers[0] = true;

            data.answers.Shuffle((index_1, index_2) =>
            {
                answers.Swap(index_1, index_2);
            }, GlobalRandomizer.SEED);

            for (int a=0; a < answers.Length; ++a)
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
           gameScreen.SetProgress(Model.progress, ControllerGlobal.Instance.GetMaxLevelForCurrGame());
        }


    }
}