/*
    Author: Ghercioglo "Romeon0" Roman
    Desc: ACK - Acknowledge
 */
using System;
using UnityEngine;

namespace Assets.Scripts.Games.GameData.Question_
{
    [Serializable]
    public class Question
    {
        public string question;
        [SerializeField] public string[] answers;

        public Question(string question, string[] answers)
        {
            this.question = (string)question.Clone();
            this.answers = (string[])answers.Clone();
        }

        public Question Clone()
        {
            return new Question(question, answers);
        }
    }
}
