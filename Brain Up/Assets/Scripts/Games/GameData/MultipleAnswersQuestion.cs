/*
    Author: Ghercioglo "Romeon0" Roman
    Desc: ACK - Acknowledge
 */
using System;
using UnityEngine;

namespace Assets.Scripts.Games.GameData.MultipleAnswersQuestion
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

        internal Question Clone()
        {
            return new Question(question, answers);
        }
    }

    [CreateAssetMenu(fileName = "MultipleAnswersQuestion", menuName = "ScriptableObjects/MultipleAnswersQuestion", order = 1)]
    public class MultipleAnswersQuestion : ScriptableObject
    {
        public Question[] questions;
    }
}
