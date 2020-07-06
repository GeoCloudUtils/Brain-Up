using Assets.Scripts.Games;
using Assets.Scripts.Games.__Other;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Screens
{
    public class GameScreenCards : MonoBehaviour, GameScreenAbstract
    {
        public GameObject screen;
        public TMP_Text questionText;
        public TMP_Text progress;
        public StatesImage[] cards;
        public GameObject[] hearts;
        public string wikipediaLink;
        //
        private bool[] answers;
        private int currHeartsCount = 0;

        private void Start()
        {
            int index = 0;
            foreach(StatesImage image in cards)
            {
                image.index = index++;
                image.CheckState += OnCheckState;
                image.SetState(0);
            }
        }

        public void Show(bool show)
        {
            screen.SetActive(show);

            if (!show)
                currHeartsCount = 0;
        }

        public void InitScreen(Sprite[] images, string question, bool[] answers)
        {
            //First time
            if (currHeartsCount == 0)
            {
                currHeartsCount = 3;
                foreach (GameObject obj in hearts)
                {
                    if (!obj.activeSelf)
                        obj.SetActive(true);
                }
            }

            int cardsCount = cards.Length;
            if (answers.Length > cardsCount || answers.Length <= 0)
                Debug.LogError("answers must be of length=" + cardsCount);
            if (images.Length > cardsCount || images.Length <= 0)
                Debug.LogError("images must be of length=" + cardsCount);

            int count = Math.Min(images.Length, cards.Length);

            for (int a = 0; a < count; ++a)
            {
                cards[a].image.sprite = images[a];
                cards[a].SetState(0);
            }

            questionText.text = question;
            this.answers = answers;
        }

        internal void SetProgress(int curr, int max)
        {
            if(max == -1)//infinite
                this.progress.text = string.Format("Level {0}", curr);
            else
                this.progress.text = string.Format("Progress: {0}/{1}", curr, max);
        }

        public void OnWikipediaClicked()
        {
            Application.OpenURL("https://en.wikipedia.org/wiki/Gallery_of_sovereign_state_flags");
        }


        private int OnCheckState(StatesImage img)
        {
            //Already selected
            int currState = img.GetState();
            if (currState != 0) return currState;

            //Selected. 1 - correct answer, 2 wrong answer
            int newState = answers[img.index] == true ? 1 : 2;

            //If wrong answer: decrease hearts and check game end
            if (newState == 2)
            {
                hearts[hearts.Length - currHeartsCount].SetActive(false);
                --currHeartsCount;
                
                if (currHeartsCount == 0)
                {
                    Debug.Log("All attempts exceeded. Defeat!");
                    ControllerGlobal.Instance.StopGame(GameEndReason.NoAttempts);
                    return 0;
                }
            }
            else
            {
                bool canAdvance = ControllerGlobal.Instance.Advance();

                if (!canAdvance)
                {
                    //Stage completed
                }
                else
                    ControllerGlobal.Instance.RestartGame();

                return 0;
            }

            return newState;
        }

    }
}
