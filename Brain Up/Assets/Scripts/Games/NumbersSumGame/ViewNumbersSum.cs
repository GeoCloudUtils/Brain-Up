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
using System.Linq;
using TMPro;
using UnityEngine;


namespace Assets.Scripts.Games.RepeatLettersGame
{
    public class ViewNumbersSum : SingleInstanceObject<ViewRepeatLetters>,
        ViewAbstract
    {
        [Header("References")]
        public SimpleWord gameExpression;
        public SimpleWord showExpression;
        public SimpleWord result;
        public GameObject showExpressionScreen;
        public GameScreenNumbers gameScreen;
        public Keyboard keyboard; 
        [Header("Settings")]
        public float flipInterval = 1f;
        //Properties
        public ModelNumbersSum Model { get; protected set; }
        private string answer;


        public void Create(ModelNumbersSum model)
        {
            keyboard.SetLetters("0123456789-.".ToCharArray());
            this.Model = model;
        }

        public void StartGame(Action endCallback = null)
        {

            int progress = Model.Progress;
            string strWord = Model.GetCurrentWord();
            string strCells = strWord + "=?";

            //Set In-game expression
            gameExpression.SetText(strCells);
            gameExpression.HideAllLetters();
            gameScreen.SetProgress(Model.Progress + 1, Model.LevelsCount);
            gameScreen.Show(true);

            //Set show expression
            showExpression.SetText(strCells);
            showExpression.HideAllLetters();

            //Set result
            Debug.Log("Answer: " + Model.CorrectAnswer);
            answer = string.Format("{0:0}", Model.CorrectAnswer);
            string tmp = "";
            for (int a = 0; a < answer.Length; ++a)
                tmp += '0';
            answer = tmp;
            result.SetText(answer);
            result.HideAllLetters();
            result.SelectLetter(0);

            //show signs
            ShowSigns(strCells);

            //animate
            StartCoroutine(ShowExpression(strWord.ToCharArray(), endCallback));
        }

        protected void ShowSigns(string word)
        {
            for (int a = 0; a < word.Length; ++a)
            {
                char c = word[a];
                if (IsSpecialCharacter(c))
                {
                    gameExpression.ShowLetter(a);
                    showExpression.ShowLetter(a);
                }
                else
                    gameExpression.SetLetter(a, 'Z');
            }
        }

        public void StopGame()
        {
            gameScreen.Show(false);
            if (gameExpression.currSelectedLetterIndex != -1)
                gameExpression.DeselectLetter(gameExpression.currSelectedLetterIndex);
            gameExpression.currSelectedLetterIndex = -1;
        }

        public bool Hint()
        {
            //List<int> unknownLettersIndexes = gameWord.GetIncorrectLettersIndexes();
            //if (unknownLettersIndexes.Count == 0) return false;

            //System.Random rand = new System.Random();
            //int index = unknownLettersIndexes[rand.Next(0, unknownLettersIndexes.Count)];

            //gameWord.SetLetter(index, gameWord.currWord[index]);
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
            Model = (ModelNumbersSum)model;
        }

        public bool IsWordCorrect()
        {
            string str = "";
            for (int a = 0; a < answer.Length; ++a)
            {
                str += result.letters[a].text;
            }

            double value;

            bool succes = double.TryParse(str, out value);
            if (!succes)
                return false;


            return Model.CorrectAnswer == value;
        }

        protected IEnumerator ShowExpression(char[] letters, Action endCallback)
        {
            GameScreenGlobal.Instance.Show(false);
            showExpressionScreen.SetActive(true);

            bool[] cellsWithNumbers = new bool[letters.Length];
            for (int a = 0; a < letters.Length; ++a)
            {
                char c = letters[a];
                cellsWithNumbers[a] = !IsSpecialCharacter(c);
            }

            for (int a = 0; a < letters.Length; ++a)
            {
                int counter = 0;
                for (int b = a; b < letters.Length; ++b)
                    if (cellsWithNumbers[b])
                    {
                        showExpression.SelectLetter(b);
                        showExpression.ShowLetter(b);
                        showExpression.currSelectedLetterIndex = -1;
                        ++counter;
                    }
                    else break;

                yield return new WaitForSeconds(flipInterval);

                for (int b = a; b < letters.Length; ++b)
                    if (cellsWithNumbers[b])
                    {
                        showExpression.DeselectLetter(b);
                        showExpression.HideLetter(b);
                    }
                    else break;
                a += counter;
            }

            gameScreen.Show(true);
            showExpressionScreen.SetActive(false);
            GameScreenGlobal.Instance.Show(true);

            endCallback?.Invoke();

            yield return null;
        }

        public GameScreenAbstract GetScreen()
        {
            return gameScreen;
        }

        public ModelNumbersSum GetModel()
        {
            return Model;
        }

        protected bool IsSpecialCharacter(char c)
        {
            return c == '?' || c == '+' || c == '=';
        }
    }
}
