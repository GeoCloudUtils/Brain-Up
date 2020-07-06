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
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Games.TimeKillerGame
{
    [Serializable]
    public class ModelTimeKiller : SingleInstanceObject<ModelTimeKiller>,
        ModelAbstract
    {
        protected MultipleAnswersQuestion allData;
        protected Question data;
        private int correctAnswer;
        public int progress = 1;
        public Database _database;
        public GameId GameId { get; set; }

        private void Start()
        {
            _database = Database.Instance;
        }

        public void Create()
        {
            int progress = Database.Instance.GetGameProgress((int)GameId);
            data = allData.questions[progress].Clone();
        }

        public void StartGame()
        {

        }

        public void StopGame()
        {
            
        }

        public Question GetData()
        {
            return data;
        }

        public void SetDictionary(MultipleAnswersQuestion questions)
        {
            allData = questions;
        }


        internal bool Advance()
        {
            int currProgress = Database.Instance.GetGameProgress((int)GameId);
            if (currProgress >= allData.questions.Length-1) return false;

            progress = currProgress + 1;
            Database.Instance.SetGameProgress((int)GameId, progress);
            return true;
        }
    }
}
