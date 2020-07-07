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
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Games.CapitalsGame
{
    [Serializable]
    public class ModelCapitals : SingleInstanceObject<ModelTimeKiller>,
        ModelAbstract
    {
        protected TripleValueList allData;
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
            data = GenerateQuestion();
        }


        public void StartGame()
        {

        }

        public void StopGame()
        {
            progress = 0;
        }

        public Question GetData()
        {
            return data;
        }

        public void SetDictionary(TripleValueList data)
        {
            allData = data;
        }

        private Question GenerateQuestion() 
        {
            TripleValueListRow[] allQuestions = new TripleValueListRow[4];
            int[] indexes = new int[4];

            for(int a=0; a < 4; ++a)
            {
                int attempts = 30;
                bool isOk;
                int index;
                do
                {
                    --attempts;
                    index = GlobalRandomizer.Next(0, allData.rows.Length);
                    isOk = true;
                    foreach (int i in indexes)
                        if (i == index)
                        {
                            isOk = false;
                            break;
                        }
                } while (!isOk && attempts > 0);
                Debug.LogFormat("Index:{0}; Attempts remained: {1}; TotalDat: {2}", index, attempts, allData.rows.Length);
                allQuestions[a] = allData.rows[index];
            }

            return new Question("Capital of " + allQuestions[0].item1 + "?", 
                    new string[] {
                        allQuestions[0].item2,
                        allQuestions[1].item2,
                        allQuestions[2].item2,
                        allQuestions[3].item2 
                });
        }

        internal bool Advance()
        {
            if (progress >= ControllerGlobal.Instance.GetMaxLevelForCurrGame()) return false;

            progress = progress + 1;
            return true;
        }
    }
}
