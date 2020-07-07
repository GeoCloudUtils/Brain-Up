/*
    Author: Ghercioglo "Romeon0" Roman
    Desc: ACK - Acknowledge
 */

using Assets.Scripts.Framework.Other;
using Assets.Scripts.Games.Abstract;
using Assets.Scripts.Games.Gamedata.WordsDictionary;
using Assets.Scripts.Games.GameData.CategorizedWordsDictionary;
using Assets.Scripts.Games.Other;
using Assets.Scripts.Screens;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace Assets.Scripts.Games.RepeatLettersGame
{
    public class ViewRepeatLetters : SingleInstanceObject<ViewRepeatLetters>,
        ViewAbstract
    {
        [Header("References")]
        public Word word;
        public GameObject showLettersScreen;
        public GameScreenLetters gameScreen;
        public TMP_Text description;
        public TMP_Text bigLetter;
        public Keyboard keyboard;
        [Header("Settings")]
        public float changeLetterInterval = 1f;
        //Properties
        public ModelRepeatLetters Model { get; protected set; }


        public void Create(ModelRepeatLetters model)
        {
            this.Model = model;
            keyboard.SetLetters("ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray());
        }

        public void StartGame(Action endCallback = null)
        {

            int progress = Model.progress;
            WordRow wordInfo = Model.GetCurrentWord();
            description.text = "";

            string w = wordInfo.word.ToUpper();
            char[] letters = w.ToCharArray();
            word.SetText(w);
            word.HideAllLetters();
            Debug.LogFormat("HistoryController: word {0}; Progress: {1}", w, Model.progress);
            gameScreen.SetProgress(Model.progress, Model.words.Length);
            gameScreen.Show(true);

            StartCoroutine(ShowLettersInRow(letters, endCallback));
        }

        public void StopGame()
        {
            gameScreen.Show(false);
            if (word.currSelectedLetterIndex != -1)
                word.DeselectLetter(word.currSelectedLetterIndex);
            word.currSelectedLetterIndex = -1;
        }

        public bool Hint()
        {
            List<int> unknownLettersIndexes = word.GetIncorrectLettersIndexes();
            if (unknownLettersIndexes.Count == 0) return false;

            int index = unknownLettersIndexes[GlobalRandomizer.Next(0, unknownLettersIndexes.Count)];

            word.SetLetter(index, word.currWord[index]);
            return true;
        }

        internal void Advance()
        {
            
        }

        ModelAbstract ViewAbstract.GetModel()
        {
            return Model;
        }

        public void SetModel(ModelAbstract model)
        {
            Model = (ModelRepeatLetters)model;
        }

        public bool IsWordCorrect()
        {
            return word.IsCorrect();
        }

        protected IEnumerator ShowLettersInRow(char[] letters, Action endCallback)
        {
            GameScreenGlobal.Instance.Show(false);
            showLettersScreen.SetActive(true);

            foreach (char let in letters)
            {
                bigLetter.text = let.ToString();
                yield return new WaitForSeconds(changeLetterInterval);
            }

            gameScreen.Show(true);
            showLettersScreen.SetActive(false);
            GameScreenGlobal.Instance.Show(true);

            endCallback?.Invoke();

            yield return null;
        }

        public GameScreenAbstract GetScreen()
        {
            return gameScreen;
        }

        public ModelRepeatLetters GetModel()
        {
            return Model;
        }
    }
}
