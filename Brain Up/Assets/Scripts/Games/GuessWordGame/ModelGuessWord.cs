/*
    Author: Ghercioglo Roman
    Desc: VM - Visual Memory
 */

using Assets.Scripts.Framework.Other;
using Assets.Scripts.Games.Abstract;
using Assets.Scripts.Games.Gamedata.WordsDictionary;
using UnityEngine;

namespace Assets.Scripts.Games.GuessWordGame
{
    public class ModelGuessWord : SingleInstanceObject<ModelGuessWord>, ModelAbstract
    {
        private WordsDictionary dictionary;
        private WordRow currWord;
        private int gameId;

        public void StartGame()
        {
            int progress = Database.Instance.GetGameProgress(gameId);
            Debug.LogFormat("Letters game started. Progress: {0}; Words: {1}", progress, dictionary.words.Length);
        }

        public void Create()
        {
            dictionary = Resources.Load<WordsDictionary>("GameData/OrderedLetters");
            gameId = (int)GameId.RepeatLetters;
            int progress = Database.Instance.GetGameProgress(gameId);
            currWord = dictionary.words[progress];
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
            return dictionary.words.Length - Database.Instance.GetGameProgress(gameId);
        }

    }
}
