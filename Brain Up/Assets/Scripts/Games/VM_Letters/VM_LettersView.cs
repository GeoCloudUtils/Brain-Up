/*
    Author: Ghercioglo "Romeon0" Roman
 */
using Assets.Scripts.Games.Abstract;
using Assets.Scripts.Screens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Games.VM_Letters
{
    public class VM_LettersView : AbstractView<VM_LettersModel>
    {
        [Header("References")]
        public Word word;
        public GameObject showLettersScreen;
        public LettersGameScreen gameScreen;
        public TMP_Text description;
        public TMP_Text bigLetter;
        [Header("Settings")]
        public float changeLetterInterval = 1f;
        //
        protected VM_LettersModel model;


        public override void Create(VM_LettersModel model)
        {
            this.model = model;
        }


        public override void StartGame(Action endCallback = null)
        {

            WordRow wordRow = model.GetCurrentWord();

            description.text = "";

            string w = wordRow.word.ToUpper();
            char[] letters = w.ToCharArray();
            word.SetText(w);
            word.HideAllLetters();

            StartCoroutine(ShowLettersInRow(letters, endCallback));
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

            System.Random rand = new System.Random();
            int index = unknownLettersIndexes[rand.Next(0, unknownLettersIndexes.Count)];

            word.SetLetter(index,word.correctWord[index]);
            return true;
        }




        public bool IsWordCorrect()
        {
            return word.IsCorrect();
        }

        protected IEnumerator ShowLettersInRow(char[] letters, Action endCallback)
        {
            showLettersScreen.SetActive(true);

            foreach (char let in letters)
            {
                bigLetter.text = let.ToString();
                yield return new WaitForSeconds(changeLetterInterval);
            }

            gameScreen.Show(true);
            showLettersScreen.SetActive(false);

            endCallback?.Invoke();

            yield return null;
        }
    }
}
