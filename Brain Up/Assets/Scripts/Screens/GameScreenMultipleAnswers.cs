﻿/*
    Author: Ghercioglo Roman
 */
using Assets.Scripts.Games;
using Assets.Scripts.Games.__Other;
using Assets.Scripts.Games.GuessWordGame;
using System;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Screens
{
    public class GameScreenMultipleAnswers : MonoBehaviour, GameScreenAbstract
    {
        [Header("References")]
        public GameObject screen;
        public DialogWin winScreen;
        public TMP_Text questionText;
        public TMP_Text progressText;
        //
        private bool[] answers;
        protected Database _database;
        protected ControllerGlobal globalController;
        private ControllerGuessWord controller;
        protected int gameId;
        public StatesImage[] options;
        public bool gameInProgress = false;
        public const int COINS_PER_WRONG_ANSWER = -20;
        public const int COINS_PER_CORRECT_ANSWER = 10;


        protected void Start()
        {
            if (screen == null) Debug.LogError(nameof(screen) + " is not assigned!");
            if (winScreen == null) Debug.LogError(nameof(winScreen) + " is not assigned!");

            globalController = ControllerGlobal.Instance;
            controller = (ControllerGuessWord)ControllerGuessWord.Instance;
            _database = Database.Instance;

            gameId = (int)GameId.TimeKiller;

            int index = 0;
            foreach (StatesImage image in options)
            {
                image.index = index++;
                image.CheckState += OnCheckState;
                image.SetState(0);
            }
        }

        internal void SetProgress(int progress, int max)
        {
            if(max == -1)//infinite almost
                progressText.text = string.Format("Level {0}", progress);
            else
                progressText.text = string.Format("Level {0}/{1}", progress, max);

        }

        public void Show(bool show)
        {
            screen.SetActive(show);
        }

        public void OnReloadClicked()
        {
            globalController.RestartGame();
        }

        public void OnGamesListClicked()
        {
            Show(false);
            globalController.StopGame(GameEndReason.Exit);
        }

        public void InitScreen(string[] texts, string question, bool[] answers)
        {
            //First time
            if (!gameInProgress)
            {
                gameInProgress = true;
            }

            Debug.LogFormat("Init Screen. Question:{0};", question);
            for (int a = 0; a < answers.Length; ++a)
                Debug.LogFormat("Correct {0}: {1}", a, answers[a]);

            int cardsCount = options.Length;
            if (answers.Length > cardsCount || answers.Length <= 0)
                Debug.LogError("answers must be of length=" + cardsCount);
            if (texts.Length > cardsCount || texts.Length <= 0)
                Debug.LogError("images must be of length=" + cardsCount);

            int count = Math.Min(texts.Length, options.Length);

            for (int a = 0; a < count; ++a)
            {
                options[a].text.text = texts[a];
                options[a].SetState(0);
            }

            questionText.text = question;
            this.answers = answers;
        }

        private int OnCheckState(StatesImage img)
        {
            //Already selected
            int currState = img.GetState();
            if (currState != 0) return currState;

            //Selected. 1 - correct answer, 2 wrong answer
            int newState =  answers[img.index] == true ? 1 : 2;

            //If wrong answer: decrease hearts and check game end
            if (newState == 2)
            {
                
                _database.Coins += COINS_PER_WRONG_ANSWER;

                if (_database.Coins <= 0)
                {
                    ControllerGlobal.Instance.StopGame(GameEndReason.NoCoins);
                    return 0;
                }
            }
            else
            {
                bool canAdvance = ControllerGlobal.Instance.Advance();

                if (!canAdvance)
                {
                    //stage completed
                }
                else
                {
                    _database.Coins += COINS_PER_CORRECT_ANSWER;
                    ControllerGlobal.Instance.RestartGame();
                }

                return 0;
            }

            return newState;
        }
    }
}
