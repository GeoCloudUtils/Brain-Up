/*
    Author: Ghercioglo "Romeon0" Roman
    Desc: ACK - Acknowledge
 */

using Assets.Scripts.Framework.Other;
using Assets.Scripts.Games;
using Assets.Scripts.Games.Abstract;
using Assets.Scripts.Games.Gamedata.WordsDictionary;
using Assets.Scripts.Games.GameData.CategorizedWordsDictionary;
using Assets.Scripts.Games.Other;
using Assets.Scripts.LettersGames;
using SimpleExpressionEngine;
using System;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Games.RepeatLettersGame
{
    public class ModelNumbersSum : SingleInstanceObject<ViewRepeatLetters>, ModelAbstract
    {
        //Vars
        public double CorrectAnswer { get; protected set; }
        public int LevelsCount { get; protected set; }
        public int Progress { get; protected set; }
        protected string currWord;
        protected ReflectionContext ctx;
        //Properties
        public GameId GameId { get; set; }

        //Classes
        protected class ReflectionContextLibrary
        {
            public ReflectionContextLibrary() { }
        }

        private void Start()
        {
            var lib = new ReflectionContextLibrary();
            ctx = new ReflectionContext(lib);
        }

        public void StartGame()
        {

        }

        public void Create()
        {
            currWord = GenerateData();
            CorrectAnswer = double.Parse(string.Format("{0:0.0}", Parser.Parse(currWord).Eval(ctx)));
            LevelsCount = ControllerGlobal.Instance.GetMaxLevelForCurrGame();
        }

        protected string GenerateData()
        {
            int nrNumbers = 3;
            int min = 0, max = 0;
            GameDifficulty diff = ControllerGlobal.Instance.currDifficulty;
            switch (diff)
            {
                case GameDifficulty.Welcome: nrNumbers = 3; min = 1; max = 9; break;
                case GameDifficulty.Easy: nrNumbers = 5; min = 1; max = 9; break;
                case GameDifficulty.NotSoEasy: nrNumbers = 6; min = 20; max = 40; break;
                case GameDifficulty.Medium: nrNumbers = 6; min = 20; max = 99; break;
                case GameDifficulty.Hard: nrNumbers = 7; min = 30; max = 99; break;
                case GameDifficulty.Any: nrNumbers = 5; min = 50; max = 99; break;
            }
            StringBuilder builder = new StringBuilder();
            for (int a = 0; a < nrNumbers; ++a)
            {
                int number = GlobalRandomizer.Next(min, max);
                builder.Append(number);
                builder.Append("+");
            }
            builder.Remove(builder.Length - 1, 1);

            Debug.LogFormat("Progress: {0}; NrNumbers: {1}; CurrWord: {2}", Progress, nrNumbers, builder.ToString());

            return builder.ToString();
        }

        public void StopGame()
        {
            Progress = 0;
        }

        public string GetCurrentWord()
        {
            return currWord;
        }

        public bool Advance()
        {
            Progress = Progress + 1;
            int maxLevel = ControllerGlobal.Instance.GetMaxLevelForCurrGame();
            if (Progress >= maxLevel) return false;
            return true;
        }
    }
}
