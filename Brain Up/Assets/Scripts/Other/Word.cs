/*
    Author: Ghercioglo "Romeon0" Roman
 */
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Games.Other
{
    public class Word : SimpleWord
    {

        internal bool IsCorrect()
        {
            int index = 0;
            foreach (char c in currWord)
            {
                if (c.ToString() != letters[index++].text)
                    return false;
            }
            return true;
        }

        public List<int> GetIncorrectLettersIndexes()
        {
            List<int> correct = new List<int>();
            int index = 0;
            foreach (char c in currWord)
            {
               // Debug.LogFormat("Let: {0} {1} {2}", c.ToString(), letters[index].text, c.ToString() != letters[index].text);
                if (c.ToString() != letters[index].text)
                    correct.Add(index);
                index++;
            }
            return correct;
        }

        public void OnLetterClicked(int index)
        {
            SelectLetter(index);
        }
    }
}
