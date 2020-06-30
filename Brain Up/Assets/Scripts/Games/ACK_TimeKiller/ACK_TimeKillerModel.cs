/*
    Author: Ghercioglo "Romeon0" Roman
    Desc: ACK - Acknowledge
 */

using Assets.Scripts.Games;
using Assets.Scripts.Games.Abstract;
using Assets.Scripts.Games.GameData;
using Assets.Scripts.Games.GameData.MultipleAnswersQuestion;
using Assets.Scripts.Games.VM_Letters;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Games.ACK_TimeKiller
{
    public class ACK_TimeKillerModel : AbstractModel
    {
        private MultipleAnswersQuestion rawData;
        protected int gameId;
        private Question data;
        private int correctAnswer;
        public int progress = 1;
        public Database _database;
        private void Start()
        {
            rawData = Resources.Load<MultipleAnswersQuestion>("GameData/Questions_History_Easy");
            _database = Database.Instance;
            gameId = (int)GameId.TimeKiller;
        }

        public override void StartGame()
        {

        }


        public override void Create()
        {
            int progress = Database.Instance.GetGameProgress(gameId);
            data = rawData.questions[progress].Clone();
        }

        public override void StopGame()
        {
            
        }

        public Question GetData()
        {
            return data;
        }

        internal bool Advance()
        {
            int currProgress = _database.GetGameProgress(gameId);
            if (currProgress >= rawData.questions.Length) return false;

            progress = currProgress + 1;
            _database.SetGameProgress((int)GameId.TimeKiller, progress);
            return true;
        }
    }
}
