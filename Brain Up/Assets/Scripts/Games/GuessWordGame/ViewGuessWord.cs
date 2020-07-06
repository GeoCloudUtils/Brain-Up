/*
    Author: Ghercioglo "Romeon0" Roman
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
using System.Runtime.Remoting.Messaging;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Games.GuessWordGame
{
    public class ViewGuessWord : SingleInstanceObject<ViewGuessWord>,
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
        //Vars
        public ModelGuessWord Model;


        //Getters & Setters
        public GameScreenAbstract GetScreen() => gameScreen;
        public ModelAbstract GetModel() => Model;
        public void Create(ModelGuessWord model)=> this.Model = model;
        public bool IsWordCorrect() => word.IsCorrect();
        public void SetModel(ModelAbstract model) => Model = (ModelGuessWord)model;



        public void StartGame(Action endCallback = null)
        {
            CatWord wordRow = Model.GetCurrentWord();

            description.text = wordRow.description;

            string w = wordRow.word.ToUpper();
            char[] letters = w.ToCharArray();
            word.SetText(w);
            word.HideAllLetters();

            gameScreen.SetProgress(Model.progress + 1, -1);
            gameScreen.Show(true);

            endCallback?.Invoke();
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

            System.Random rand = new System.Random();
            int index = unknownLettersIndexes[rand.Next(0, unknownLettersIndexes.Count)];

            word.SetLetter(index,word.correctWord[index]);
            return true;
        }

        internal void Advance()
        {
            
        }
    }
}
