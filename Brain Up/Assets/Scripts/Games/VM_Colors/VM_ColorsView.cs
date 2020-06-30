/*
    Author: Ghercioglo "Romeon0" Roman
 */
using Assets.Scripts.Games.Abstract;
using Assets.Scripts.Screens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Games.VM_Colors
{
    public class VM_ColorsView : AbstractView<VM_ColorsModel>
    {
        [Header("References")]
        public ImagesWord word;
        public GameObject showColorsScreen;
        public LettersGameScreen gameScreen;
        public Image bigColor;
        [Header("Settings")]
        public float changeColorInterval = 1f;
        //
        private VM_ColorsModel model;

        public override void Create(VM_ColorsModel model)
        {
            this.model = model;
        }


        public override void StartGame(Action endCallback = null)
        {

            Sprite[] currWord = model.GetCurrentWord();

            word.SetImages(currWord);
            word.HideAllLetters();

            StartCoroutine(ShowLettersInRow(currWord, endCallback));
        }

        public override void StopGame()
        {
            gameScreen.Show(false);
            if(word.currSelectedLetterIndex!=-1)
                word.DeselectLetter(word.currSelectedLetterIndex);
            word.currSelectedLetterIndex = -1;
        }

        public override bool Hint()
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
