/*
    Author: Ghercioglo "Romeon0" Roman
 */
using Assets.Scripts.Framework.Other;
using Assets.Scripts.Games.Abstract;
using Assets.Scripts.Screens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Games.RepeatColorsGame
{
    public class ViewRepeatColors : SingleInstanceObject<ViewRepeatColors>, ViewAbstract<ModelRepeatColors>
    {
        [Header("References")]
        public ImagesWord word;
        public GameObject showColorsScreen;
        public GameScreenLetters gameScreen;
        public Image bigColor;
        [Header("Settings")]
        public float changeColorInterval = 1f;
        //
        public ModelRepeatColors Model { get; set; }

        public void Create(ModelRepeatColors model)
        {
            this.Model = model;
        }


        public void StartGame(Action endCallback = null)
        {

            Sprite[] currWord = Model.GetCurrentWord();

            word.SetImages(currWord);
            word.HideAllLetters();

            StartCoroutine(ShowLettersInRow(currWord, endCallback));
        }

        public void StopGame()
        {
            gameScreen.Show(false);
            if(word.currSelectedLetterIndex!=-1)
                word.DeselectLetter(word.currSelectedLetterIndex);
            word.currSelectedLetterIndex = -1;
        }

        public bool Hint()
        {
            List<int> unknownLettersIndexes = word.GetIncorrectLettersIndexes();
            if (unknownLettersIndexes.Count == 0) return false;

            Debug.Log("Unknown count: " + unknownLettersIndexes.Count);
            foreach (int i in unknownLettersIndexes)
                Debug.Log("Unknown: " + i);

            System.Random rand = new System.Random();
            int index = unknownLettersIndexes[rand.Next(0, unknownLettersIndexes.Count)];

            word.SetLetter(index, word.correctImages[index]);
            return true;
        }




        internal bool IsWordCorrect()
        {
            return word.IsCorrect();
        }

        private IEnumerator ShowLettersInRow(Sprite[] letters, Action endCallback)
        {
            showColorsScreen.SetActive(true);

            foreach (Sprite let in letters)
            {
                bigColor.sprite = let;
                yield return new WaitForSeconds(changeColorInterval);
            }

            gameScreen.Show(true);
            showColorsScreen.SetActive(false);

            endCallback?.Invoke();

            yield return null;
        }
    }
}
