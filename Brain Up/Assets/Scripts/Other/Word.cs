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
    public class Word : MonoBehaviour
    {
        public TMP_Text[] letters;
        private int maxLetters = -1;
        public string correctWord = "HELLO BITCH";
        private Image[] images;
        private bool[] hiddenLetters;

        public Color selectedColor;
        public Color normalColor;
        public int currSelectedLetterIndex = -1;


        void Start()
        {
            if (letters == null || letters.Length == 0) Debug.LogError(nameof(letters) + " is not assigned!");

            maxLetters = letters.Length;
        }

        public void SetText(string word)
        {
            if (maxLetters == -1)
                maxLetters = word.Length;

            if (maxLetters < word.Length)
                Debug.LogError("To much letters in Word!!! Max: " + maxLetters);

            word = word.ToUpper();
            int colsInRow = letters[0].transform.parent.childCount;
            char[] chars = word.ToCharArray();
            int count = 0;
            for (int a = 0; a < letters.Length; ++a)
            {
                if (a < chars.Length)
                {
                    letters[a].text = chars[a].ToString();
                    ++count;
                }

                if (((a + 1) % colsInRow) == 0)
                {
                    letters[a].transform.parent.gameObject.SetActive(count != 0);
                    count = 0;
                }
            }
            correctWord = word;

            hiddenLetters = new bool[letters.Length];
            images = new Image[letters.Length];
            int counter = 0;
            foreach (TMP_Text text in letters)
            {
                images[counter++] = text.transform.parent.GetComponent<Image>();
            }
        }

        internal void HideAllLetters()
        {
            int counter = 0;
            foreach (char c in correctWord)
            {
                HideLetter(counter++);
            }
        }

        public void HideLetter(int index)
        {
            if (index >= correctWord.Length)
                Debug.LogError("Letter with index = " + index + " not exists!");
            letters[index].text = "";
            hiddenLetters[index] = true;
        }

        public bool[] GetHiddenLetters()
        {
            return hiddenLetters;
        }

        public void SetLetter(int index, char letter)
        {
            if (index >= correctWord.Length)
                Debug.LogError("Letter with index = " + index + " not exists!");
            letters[index].text = letter.ToString();
        }

        internal bool IsCorrect()
        {
            int index = 0;
            foreach (char c in correctWord)
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
            foreach (char c in correctWord)
            {
                Debug.LogFormat("Let: {0} {1} {2}", c.ToString(), letters[index].text, c.ToString() != letters[index].text);
                if (c.ToString() != letters[index].text)
                    correct.Add(index);
                index++;
            }
            return correct;
        }

        public void ShowLetter(int index)
        {
            if (index >= correctWord.Length)
                Debug.LogError("Letter with index = " + index + " not exists!");
            letters[index].text = correctWord.ElementAt(index).ToString();
            hiddenLetters[index] = false;
        }


        public int GetLettersCount()
        {
            return correctWord.Length;
        }

        public void SelectLetter(int index)
        {
            if (currSelectedLetterIndex != -1)
                DeselectLetter(currSelectedLetterIndex);

            images[index].color = selectedColor;
            currSelectedLetterIndex = index;
        }

        public void DeselectLetter(int index)
        {
            images[index].color = normalColor;
        }


        public void OnLetterClicked(int index)
        {
            SelectLetter(index);
        }
    }
}
