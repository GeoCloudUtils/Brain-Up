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
        [Header("Settings")]
        public float changeLetterInterval = 1f;
        //Properties
        public ModelRepeatLetters Model;


        public void Create(ModelRepeatLetters model)
        {
            this.Model = model;
        }

        public void StartGame(Action endCallback = null)
        {
            Debug.Log("HistoryController: Starting game...");

            int progress = Database.Instance.GetGameProgress((int)GameId.Acknowledge_History);
            WordRow wordInfo = Model.GetCurrentWord();
            description.text = "";

            string w = wordInfo.word.ToUpper();
            char[] letters = w.ToCharArray();
            word.SetText(w);
            word.HideAllLetters();
            Debug.Log("HistoryController: word - " + w);
            gameScreen.Show(true);

            StartCoroutine(ShowLettersInRow(letters, endCallback));
            Debug.Log("HistoryController: Game started.");
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

            System.Random rand = new System.Random();
            int index = unknownLettersIndexes[rand.Next(0, unknownLettersIndexes.Count)];

            word.SetLetter(index, word.correctWord[index]);
            return true;
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
