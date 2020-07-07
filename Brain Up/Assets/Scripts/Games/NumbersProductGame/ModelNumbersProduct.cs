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
    public class ModelNumbersProduct : ModelNumbersSum
    {
        public new void Create()
        {
            currWord = GenerateData();
            CorrectAnswer = double.Parse(string.Format("{0:0.0}", Parser.Parse(currWord).Eval(ctx)));
            LevelsCount = ControllerGlobal.Instance.GetMaxLevelForCurrGame();
        }

        protected new string GenerateData()
        {
            int nrNumbers = 3;
            int min = 0, max = 0;
            GameDifficulty diff = ControllerGlobal.Instance.currDifficulty;
            switch (diff)
            {
                case GameDifficulty.Welcome: nrNumbers = 2; min = 2; max = 9; break;
                case GameDifficulty.Easy: nrNumbers = 3; min = 2; max = 9; break;
                case GameDifficulty.NotSoEasy: nrNumbers = 4; min = 5; max = 30; break;
                case GameDifficulty.Medium: nrNumbers = 5; min = 10; max = 40; break;
                case GameDifficulty.Hard: nrNumbers = 6; min = 10; max = 60; break;
                case GameDifficulty.Any: nrNumbers = 5; min = 10; max = 59; break;
            }
            StringBuilder builder = new StringBuilder();
            for (int a = 0; a < nrNumbers; ++a)
            {
                int number = GlobalRandomizer.Next(min, max);
                builder.Append(number);
                builder.Append("*");
            }
            builder.Remove(builder.Length - 1, 1);

            Debug.LogFormat("Progress: {0}; NrNumbers: {1}; CurrWord: {2}", Progress, nrNumbers, builder.ToString());

            return builder.ToString();
        }
    }
}
