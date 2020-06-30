/*
    Author: Ghercioglo "Romeon0" Roman
    Desc: ACK - Acknowledge
 */

using Assets.Scripts.Games;
using Assets.Scripts.Games.Abstract;
using UnityEngine;

namespace Assets.Scripts.Games.ACK_History
{
    public class ACK_HistoryModel : AbstractModel
    {
        private CatWord[] words;
        private CatWord currWord;
        protected int gameId;

        public override void StartGame()
        {
            int progress = Database.Instance.GetGameProgress(gameId);
            Debug.LogFormat("Letters game started. Progress: {0}; Words: {1}", progress, words.Length);
        }

        public override void Create()
        {
            CategorizedWordsDictionary dictionary = Resources.Load<CategorizedWordsDictionary>("GameData/GuessWord");
            gameId = (int)GameId.Acknowledge_History;

            int progress = Database.Instance.GetGameProgress(gameId); 
            foreach(CatWordRow row in dictionary.words)
            {
                if(row.category == "History")
                {
                    words = row.words;
                }
            }
            currWord = words[progress];
            Debug.LogFormat("Progress: {0}; Words: {1}; CurrWord: {2}", progress, dictionary.words.Length, currWord.word);
        }

        public CatWord GetCurrentWord()
        {
            return currWord;
        }

        public int GetRemainedWordsCount()
        {
            return words.Length - Database.Instance.GetGameProgress(gameId);
        }

        public override void StopGame()
        {
            
        }
    }
}
