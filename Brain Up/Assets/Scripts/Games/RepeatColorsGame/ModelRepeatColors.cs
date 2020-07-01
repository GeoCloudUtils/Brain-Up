/*
    Author: Ghercioglo Roman
    Desc: VM - Visual Memory
 */

using Assets.Scripts.Framework.Other;
using Assets.Scripts.Games.Abstract;
using UnityEngine;

namespace Assets.Scripts.Games.RepeatColorsGame
{
    public class ModelRepeatColors : SingleInstanceObject<ModelRepeatColors>, ModelAbstract
    {
        public Sprite[] allColors;
        private Sprite[] currColors;
        private int gameId;

        public void StartGame()
        {
            int progress = Database.Instance.GetGameProgress(gameId);
            int count = 3 + progress / 15;
            if (count > 14)
                count = 14;
            Debug.LogFormat("Colors game started. Progress: {0}; Colors count: {1}", progress, count);
        }

        public void Create()
        {
            gameId = (int)GameId.RepeatColors;
            int progress = Database.Instance.GetGameProgress(gameId);

            int count = 3 + progress / 15;
            if (count > 14)
                count = 14;

            currColors = GenerateColors(count);
        }

        public void StopGame()
        {

        }

        public Sprite[] GetCurrentWord()
        {
            return currColors;
        }

        private Sprite[] GenerateColors(int count)
        {
            currColors = new Sprite[count];
            System.Random rand = new System.Random();
            int lastIndex = -1;
            for(int a =0; a < count; ++a)
            {
                int index = -2;
                do {
                    index = rand.Next(0, allColors.Length);
                    currColors[a] = allColors[index];
                } while (lastIndex == index);
                lastIndex = index;
            }

            return currColors;
        }
    }
}
