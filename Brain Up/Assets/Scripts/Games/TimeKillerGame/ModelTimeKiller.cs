/*
    Author: Ghercioglo "Romeon0" Roman
    Desc: ACK - Acknowledge
 */

using Assets.Scripts.Framework.Other;
using Assets.Scripts.Games;
using Assets.Scripts.Games.Abstract;
using Assets.Scripts.Games.GameData;
using Assets.Scripts.Games.GameData.MultipleAnswersQuestion;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Games.TimeKillerGame
{
    [Serializable]
    public class ModelTimeKiller : SingleInstanceObject<ModelTimeKiller>,
        ModelAbstract
    {
        protected MultipleAnswersQuestion rawData;
        protected Question allData;
        private int correctAnswer;
        public int progress = 1;
        public Database _database;
        public GameId GameId { get; set; }

        private void Start()
        {
            _database = Database.Instance;
        }

        public void StartGame()
        {

        }


        public void Create()
        {
            int progress = Database.Instance.GetGameProgress((int)GameId);
            allData = rawData.questions[progress].Clone();
        }

        public void StopGame()
        {
            
        }

        public Question GetData()
        {
            return allData;
        }

        public void SetData(MultipleAnswersQuestion questions)
        {
            rawData = questions;
        }


        internal bool Advance()
        {
            int currProgress = _database.GetGameProgress((int)GameId);
            if (currProgress >= rawData.questions.Length) return false;

            progress = currProgress + 1;
            _database.SetGameProgress((int)GameId, progress);
            return true;
        }
    }
}
