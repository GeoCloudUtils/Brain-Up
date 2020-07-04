using Assets.Scripts;
using Assets.Scripts.Games;
using Assets.Scripts.Screens;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Games.Other
{

    [RequireComponent(typeof(Button))]
    public class UIDifficultyLevel : MonoBehaviour
    {
        public Image levelProgressBar;
        public TMP_Text levelProgressCount;
        public GameObject levelFinished;
        public GameObject levelLocked;
        public GameDifficulty difficulty;
        public bool locked = false;


        public void SetDifficulty(GameId gameId)
        {
            int levelsCompleted = Database.Instance.GetGameProgressForDifficulty((int)gameId, (int)difficulty);
            int levelsMax = ControllerGlobal.Instance.GetMaxLevel(gameId, difficulty);

            Debug.LogFormat("Diff {0} progress: {1}/{2}", difficulty, levelsCompleted, levelsMax);

            if (levelsCompleted + 1 >= levelsMax)//level completed
            {
                levelProgressBar.fillAmount = 1;
                levelProgressCount.gameObject.SetActive(false);
                levelFinished.SetActive(true);
                levelLocked.SetActive(false);
                locked = true;
            }
            else if (levelsCompleted == -1)//level locked
            {
                levelProgressBar.fillAmount = 0;
                levelProgressCount.gameObject.SetActive(false);
                levelFinished.SetActive(false);
                levelLocked.SetActive(true);
                locked = true;
            }
            else //level in progress
            {
                float progress = levelsCompleted / (float)levelsMax;
                levelProgressBar.fillAmount = progress;
                levelProgressCount.gameObject.SetActive(true);
                levelProgressCount.text = levelsCompleted + "/" + levelsMax;
                levelFinished.SetActive(false);
                levelLocked.SetActive(false);
                locked = false;
            }
        }
    }
}