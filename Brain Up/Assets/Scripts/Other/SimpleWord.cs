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
    public class SimpleWord : MonoBehaviour
    {
        public TMP_Text[] letters;
        public string currWord = "HELLO BITCH";
        public Color selectedColor;
        public Color normalColor;
        public int currSelectedLetterIndex = -1;
        protected int maxLetters = -1;
        protected Image[] images;
        protected bool[] hiddenLetters;

        protected void Start()
        {
            if (letters == null || letters.Length == 0) Debug.LogError(nameof(letters) + " is not assigned!");

            maxLetters = letters.Length;
        }

        public void SetText(string word)
        {
            if (maxLetters == -1)
                maxLetters = letters.Length;

            if (maxLetters < word.Length)
                Debug.LogErrorFormat("To much letters in Word!!! Max: {0}; Have: {1}", maxLetters, word.Length);

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
            this.currWord = word;

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
            foreach (char c in currWord)
            {
                HideLetter(counter++);
            }
        }

        public void HideLetter(int index)
        {
            if (index >= currWord.Length)
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
            if (index >= currWord.Length)
                Debug.LogError("Letter with index = " + index + " not exists!");
            letters[index].text = letter.ToString();
        }

        public void ShowLetter(int index)
        {
            if (index >= currWord.Length)
                Debug.LogError("Letter with index = " + index + " not exists!");
            letters[index].text = currWord.ElementAt(index).ToString();
            hiddenLetters[index] = false;
        }


        public int GetLettersCount()
        {
            return currWord.Length;
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
    }
}
