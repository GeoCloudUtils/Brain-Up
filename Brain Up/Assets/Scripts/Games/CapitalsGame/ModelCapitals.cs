/*
    Author: Ghercioglo "Romeon0" Roman
    Desc: ACK - Acknowledge
 */

using Assets.Scripts.Framework.Other;
using Assets.Scripts.Games;
using Assets.Scripts.Games.Abstract;
using Assets.Scripts.Games.GameData;
using Assets.Scripts.Games.GameData.MultipleAnswersQuestion;
using Assets.Scripts.Games.TimeKillerGame;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Games.CapitalsGame
{
    [Serializable]
    public class ModelCapitals : ModelTimeKiller,
        ModelAbstract
    {
        public new void Create()
        {
            allData = GenerateQuestion();
        }

        private Question GenerateQuestion() 
        {
            Question[] allQuestions = new Question[4];
            var rand = new System.Random();
            int[] indexes = new int[4];

            Debug.Log("rawData.questions.Length: " + rawData.questions.Length);
            if (rawData.questions.Length < 4)
                Debug.LogError("Length of rawData must be more or equal than 4!!!");

            for(int a=0; a < 4; ++a)
            {
                int attempts = 30;
                bool isOk;
                int index;
                do
                {
                    --attempts;
                    index = rand.Next(0, rawData.questions.Length);
                    isOk = true;
                    foreach (int i in indexes)
                        if (i == index)
                        {
                            isOk = false;
                            break;
                        }
                } while (!isOk && attempts > 0);
                allQuestions[a] = rawData.questions[index];
            }

            return new Question("Capital of " + allQuestions[0].question + "?", 
                    new string[] {
                        allQuestions[0].answers[0],
                        allQuestions[1].answers[0],
                        allQuestions[2].answers[0],
                        allQuestions[3].answers[0] 
                });
        }
    }
}
