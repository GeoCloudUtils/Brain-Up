/*
    Author: Ghercioglo "Romeon0" Roman
    Desc: ACK - Acknowledge
 */

using Assets.Scripts.Framework.Other;
using Assets.Scripts.Games.Abstract;
using Assets.Scripts.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Assets.Scripts.Games.SelectFlagGame
{
    public class ViewSelectFlag : SingleInstanceObject<ControllerSelectFlag>, 
        ViewAbstract
    {
        //Vars
        public GameScreenCards gameScreen;
        //Properties
        public ModelSelectFlag Model;


        public void Create(ModelSelectFlag model)
        {
            this.Model = model;
        }

        public ModelSelectFlag GetModel()
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
            List<Tuple<int, Sprite, string>> data = Model.GetData();
            bool[] answers = new bool[data.Count];

            gameScreen.SetProgress(Model.progress, Model.questionsCount);

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

        public void StopGame()
        {
            
        }

        ModelAbstract ViewAbstract.GetModel()
        {
            return Model;
        }

        public void SetModel(ModelAbstract model)
        {
            Model = (ModelSelectFlag)model;
        }

        internal void Advance()
        {
            gameScreen.SetProgress(Model.progress, Model.questionsCount);
        }


    }
}
