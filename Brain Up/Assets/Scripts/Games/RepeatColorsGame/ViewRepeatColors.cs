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
    public class ViewRepeatColors : SingleInstanceObject<ViewRepeatColors>, ViewAbstract
    {
        [Header("References")]
        public ImagesWord word;
        public GameScreenColors gameScreen;
        public GameObject showColorScreen;
        public Image bigColor;
        [Header("Settings")]
        public float changeColorInterval = 1f;
        private int progress = 1;
        //
        public ModelRepeatColors Model;

        public void Create(ModelRepeatColors model)
        {
            this.Model = model;
        }


        public void StartGame(Action endCallback = null)
        {

            Sprite[] currWord = Model.GetCurrentWord();

            word.SetImages(currWord);
            word.HideAllLetters();

            var global = ControllerGlobal.Instance;
            int max = global.GetMaxLevelForCurrGame();
            gameScreen.SetProgress(progress, max);

            StartCoroutine(ShowLettersInRow(currWord, endCallback));
        }

        public void StopGame()
        {
            if(word.currSelectedLetterIndex!=-1)
                word.DeselectLetter(word.currSelectedLetterIndex);
            word.currSelectedLetterIndex = -1;
            progress = 1;
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
            GameScreenGlobal.Instance.Show(false);
            showColorScreen.SetActive(true);

            foreach (Sprite let in letters)
            {
                bigColor.sprite = let;
                yield return new WaitForSeconds(changeColorInterval);
            }

            showColorScreen.SetActive(false);
            gameScreen.Show(true);
            GameScreenGlobal.Instance.Show(true);

            endCallback?.Invoke();

            yield return null;
        }

        public GameScreenAbstract GetScreen()
        {
            return gameScreen;
        }

        public ModelAbstract GetModel()
        {
            return Model;
        }

        public void SetModel(ModelAbstract model)
        {
            Model = (ModelRepeatColors)model;
        }

        public void Advance()
        {
            ++progress;
        }
    }
}
