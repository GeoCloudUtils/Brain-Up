/*
    Author: Ghercioglo "Romeon0" Roman
 */

using Assets.Scripts.Framework.Other;
using Assets.Scripts.Games.Abstract;
using Assets.Scripts.Games.GameData.MultipleAnswersQuestion;
using Assets.Scripts.Games.RepeatLettersGame;
using System;
using UnityEngine;

namespace Assets.Scripts.Games.TimeKillerGame
{
    [Serializable]
    public class ControllerHistory : ControllerTimeKiller
    {
        private new void Start()
        {
            base.Start();
        }

        protected new MultipleAnswersQuestion GetData()
        {
            MultipleAnswersQuestion data = null;
            if (_globalController.currDifficulty == GameDifficulty.Easy)
                data = Resources.Load<MultipleAnswersQuestion>("GameData/Questions_History_Easy");
            else if (_globalController.currDifficulty == GameDifficulty.Medium)
                data = Resources.Load<MultipleAnswersQuestion>("GameData/Questions_History_Medium");
            else if (_globalController.currDifficulty == GameDifficulty.Hard)
                data = Resources.Load<MultipleAnswersQuestion>("GameData/Questions_History_Hard");
            else
                Debug.LogError("Unknown difficulty!");
            return data;
        }

        public new void StartGame(Action<bool, bool> callback)
        {
            base.StartGame(callback);
        }
    }
}
