/*
    Author: Ghercioglo "Romeon0" Roman
    Desc: ACK - Acknowledge
 */

using Assets.Scripts.Framework.Other;
using Assets.Scripts.Games;
using Assets.Scripts.Games.Abstract;
using Assets.Scripts.Games.Gamedata.WordsDictionary;
using Assets.Scripts.Games.GameData.CategorizedWordsDictionary;
using UnityEngine;

namespace Assets.Scripts.Games.RepeatLettersGame
{
    public class ModelRepeatLetters : SingleInstanceObject<ViewRepeatLetters>, ModelAbstract
    {
        private WordRow[] words;
        private WordRow currWord;

        public GameId GameId { get; set; }

        public void StartGame()
        {
            int progress = Database.Instance.GetGameProgress((int)GameId);
            Debug.LogFormat("Letters game started. Progress: {0}; Words: {1}", progress, words.Length);
        }

        public void Create()
        {
            WordsDictionary dictionary = Resources.Load<WordsDictionary>("GameData/OrderedLetters");
            words = dictionary.words;

            int progress = Database.Instance.GetGameProgress((int)GameId);
            currWord = words[progress];
            Debug.LogFormat("Progress: {0}; Words: {1}; CurrWord: {2}", progress, dictionary.words.Length, currWord.word);
        }

        public void StopGame()
        {

        }

        public WordRow GetCurrentWord()
        {
            return currWord;
        }

        public int GetRemainedWordsCount()
        {
            return words.Length - Database.Instance.GetGameProgress((int)GameId);
        }

       
    }
}
