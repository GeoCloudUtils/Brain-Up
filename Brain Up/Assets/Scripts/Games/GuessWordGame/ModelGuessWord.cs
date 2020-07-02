/*
    Author: Ghercioglo Roman
    Desc: VM - Visual Memory
 */

using Assets.Scripts.Framework.Other;
using Assets.Scripts.Games.Abstract;
using Assets.Scripts.Games.Gamedata.WordsDictionary;
using Assets.Scripts.Games.GameData.CategorizedWordsDictionary;
using UnityEngine;

namespace Assets.Scripts.Games.GuessWordGame
{
    public class ModelGuessWord : SingleInstanceObject<ModelGuessWord>, ModelAbstract
    {
        private CategorizedWordsDictionary dictionary;
        private CatWord currWord;

        public GameId GameId { get; set; }

        public void StartGame()
        {
            int progress = Database.Instance.GetGameProgress((int)GameId);
            Debug.LogFormat("Letters game started. Progress: {0}; Words: {1}", progress, dictionary.words.Length);
        }

        public void Create()
        {
            dictionary = Resources.Load<CategorizedWordsDictionary>("GameData/GuessWord");
            GameId = (int)GameId.RepeatLetters;
            int progress = Database.Instance.GetGameProgress((int)GameId);

            foreach(CatWordRow row in dictionary.words){
                if (row.category == "History")
                {
                    currWord = row.words[progress];
                    break;
                }
            }
        }

        public void StopGame()
        {

        }

        public CatWord GetCurrentWord()
        {
            return currWord;
        }

        public int GetRemainedWordsCount()
        {
            return dictionary.words.Length - Database.Instance.GetGameProgress((int)GameId);
        }

    }
}
