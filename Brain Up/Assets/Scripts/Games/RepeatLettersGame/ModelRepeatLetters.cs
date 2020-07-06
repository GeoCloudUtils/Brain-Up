/*
    Author: Ghercioglo "Romeon0" Roman
    Desc: ACK - Acknowledge
 */

using Assets.Scripts.Framework.Other;
using Assets.Scripts.Games;
using Assets.Scripts.Games.Abstract;
using Assets.Scripts.Games.Gamedata.WordsDictionary;
using Assets.Scripts.Games.GameData.CategorizedWordsDictionary;
using Assets.Scripts.Games.Other;
using Assets.Scripts.LettersGames;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Games.RepeatLettersGame
{
    public class ModelRepeatLetters : SingleInstanceObject<ViewRepeatLetters>, ModelAbstract
    {
        public WordRow[] words;
        private WordRow currWord;
        public int progress = 0;

        public GameId GameId { get; set; }

        public void StartGame()
        {
            int progress = Database.Instance.GetGameProgress((int)GameId);
            Debug.LogFormat("Letters game started. Progress: {0}; Words: {1}", progress, words.Length);
        }

        public void Create()
        {
            WordsDictionary dictionary = Resources.Load<WordsDictionary>("GameData/OrderedLetters");

            int min = 0, max = 0;
            GameDifficulty diff = ControllerGlobal.Instance.currDifficulty;
            switch (diff)
            {
                case GameDifficulty.Welcome: min = 0; max = 44; break;
                case GameDifficulty.Easy: min = 45; max = 89; break;
                case GameDifficulty.NotSoEasy: min = 90; max = 134; break;
                case GameDifficulty.Medium: min = 135; max = 169; break;
                case GameDifficulty.Hard: min = 170; max = 239; break;
            }
            words = dictionary.words.Slice(min, max).ToArray<WordRow>();

            currWord = words[progress];
            Debug.LogFormat("Progress: {0}; Words: {1}; CurrWord: {2}", progress, dictionary.words.Length, currWord.word);
        }

        public void StopGame()
        {
            progress = 0;
        }

        public WordRow GetCurrentWord()
        {
            return currWord;
        }

        public int GetRemainedWordsCount()
        {
            return words.Length - Database.Instance.GetGameProgress((int)GameId);
        }

        public bool Advance()
        {
            if (progress >= words.Length - 1) return false;
            progress = progress + 1;
            return true;
        }
    }
}
