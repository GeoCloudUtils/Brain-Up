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
        public CategorizedWordsDictionary allData;
        public int progress=0;
        //
        private CatWordRow data;
        
        //Props
        public GameId GameId { get; set; }


        public void Create()
        {
            if(allData==null)
                allData = Resources.Load<CategorizedWordsDictionary>("GameData/GuessWord");

            foreach (CatWordRow row in allData.words)
            {
                if (row.category == "History")
                {
                    data = row;
                    break;
                }
            }
        }

        public void StartGame()
        {

        }

        public void StopGame()
        {
            progress = 0;
        }

        public CatWord GetCurrentWord()
        {
            return data.words[progress];
        }


        internal bool Advance()
        {
            progress += 1;
            Debug.LogFormat("Avance::GuessWordModel: {0} {1}", progress,allData.words.Length);
            if (progress >= data.words.Length) return false;
            return true;
        }

    }
}
