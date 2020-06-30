/*
    Author: Ghercioglo Roman
    Desc: VM - Visual Memory
 */

using Assets.Scripts.Games.Abstract;
using UnityEngine;

namespace Assets.Scripts.Games.VM_Letters
{
    public class VM_LettersModel : AbstractModel
    {
        public WordsDictionary dictionary;
        private WordRow currWord;
        private int gameId;

        public override void StartGame()
        {
            int progress = Database.Instance.GetGameProgress(gameId);
            Debug.LogFormat("Letters game started. Progress: {0}; Words: {1}", progress, dictionary.words.Length);
        }

        public override void Create()
        {
            dictionary = Resources.Load<WordsDictionary>("GameData/OrderedLetters");
            gameId = (int)GameId.VisualMemory_Letters;
            int progress = Database.Instance.GetGameProgress(gameId);
            currWord = dictionary.words[progress];
        }

        public override void StopGame()
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
