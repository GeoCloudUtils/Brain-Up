/*
    Author: Ghercioglo "Romeon0" Roman
    Desc: ACK - Acknowledge
 */

using Assets.Scripts.Framework.Other;
using Assets.Scripts.Games;
using Assets.Scripts.Games.Abstract;
using Assets.Scripts.Games.GameData;
using Assets.Scripts.Games.GameData.MultipleAnswersQuestion;
using Assets.Scripts.Games.GameData.Question_;
using Assets.Scripts.Games.TimeKillerGame;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Games.HistoryGame
{
    [Serializable]
    public class ModelHistory : ModelTimeKiller,
        ModelAbstract
    {
        private int currGameId = 0;
        private int currDifficulty = 0;

        public new void Create()
        {
            GameDifficulty diff = ControllerGlobal.Instance.currDifficulty;
            currGameId = (int)GameId;
            currDifficulty = (int)diff;
            int progress = Database.Instance.GetGameProgressForDifficulty(currGameId, currDifficulty);
            Debug.Log("PRogress: " + progress);
            data = allData.questions[progress].Clone();
        }


        internal new bool Advance()
        {
            int currProgress = Database.Instance.GetGameProgressForDifficulty(currGameId, currDifficulty);
            if (currProgress >= allData.questions.Length-1) return false;

            progress = currProgress + 1;
            Database.Instance.SetGameProgressForDifficulty(currGameId, currDifficulty, progress);
            return true;
        }
    }
}
