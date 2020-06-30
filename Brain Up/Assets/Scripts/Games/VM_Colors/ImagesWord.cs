/*
    Author: Ghercioglo "Romeon0" Roman
 */
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Games.VM_Colors
{

    public class ImagesWord : MonoBehaviour
    {
        public ClickableImage[] letters;
        public Sprite[] correctImages;
        private int maxLetters = -1;
        private bool[] hiddenLetters;

        public Color selectedColor;
        public Color normalColor;
        public int currSelectedLetterIndex = -1;


        void Start()
        {
            if (letters == null || letters.Length == 0) Debug.LogError(nameof(letters) + " is not assigned!");

            maxLetters = letters.Length;
        }

        public void SetImages(Sprite[] sprites)
        {
            if (maxLetters == -1)
                maxLetters = correctImages.Length;

            if (maxLetters < correctImages.Length)
                Debug.LogError("To much letters in Word!!! Max: " + maxLetters);

            int colsInRow = this.letters[0].transform.parent.childCount;
            Debug.Log("Color::colsInRow: " + colsInRow + "; sprites: " + letters[0].transform.parent.name);
            int count = 0;
            for (int a = 0; a < this.letters.Length; ++a)
            {
                if (a < sprites.Length)
                {
                    this.letters[a].SetImage(sprites[a]);
                    this.letters[a].Select(false);
                    ++count;
                    letters[a].gameObject.SetActive(true);
                }
                else
                    letters[a].gameObject.SetActive(false);

                if (((a + 1) % colsInRow) == 0)
                {
                    this.letters[a].transform.parent.gameObject.SetActive(count != 0);
                    count = 0;
                }
            }
            correctImages = sprites;

            hiddenLetters = new bool[correctImages.Length];
        }

        internal void HideAllLetters()
        {
            int counter = 0;
            foreach (Sprite img in correctImages)
            {
                HideLetter(counter++);
            }
        }

        public void HideLetter(int index)
        {
            if (index >= letters.Length)
                Debug.LogError("Letter with index = " + index + " not exists!");
            letters[index].ShowImage(false);
            hiddenLetters[index] = true;
        }

        public bool[] GetHiddenLetters()
        {
            return hiddenLetters;
        }

        public List<int> GetIncorrectLettersIndexes()
        {
            List<int> incorrect = new List<int>();
            int index = 0;
            foreach (Sprite s in correctImages)
            {
                ClickableImage img = letters[index];
                if (s != img.GetImage() || !img.image.gameObject.activeInHierarchy)
                    incorrect.Add(index);
                index++;
            }
            return incorrect;
        }

        public void SetLetter(int index, Sprite letter)
        {
            if (index >= letters.Length)
                Debug.LogError("Letter with index = " + index + " not exists!");
            letters[index].SetImage(letter);
            letters[index].gameObject.SetActive(true);
        }

        internal bool IsCorrect()
        {
            int index = 0;
            foreach (Sprite s in correctImages)
            {
                if (s != letters[index++].GetImage())
                    return false;
            }
            return true;
        }

        public void ShowLetter(int index)
        {
            if (index >= correctImages.Length)
                Debug.LogError("Letter with index = " + index + " not exists!");
            letters[index].ShowImage(true);
            hiddenLetters[index] = false;
        }


        public int GetLettersCount()
        {
            return correctImages.Length;
        }

        public void SelectLetter(int index)
        {
            if (currSelectedLetterIndex != -1)
                DeselectLetter(currSelectedLetterIndex);

            letters[index].Select(true);
            currSelectedLetterIndex = index;
        }

        public void DeselectLetter(int index)
        {
            letters[index].Select(false);
        }


        public void OnLetterClicked(int index)
        {
            SelectLetter(index);
        }
    }
}