/*
    Author: Ghercioglo Roman
    Desc: VM - Visual Memory
 */

using Assets.Scripts.Framework.Other;
using Assets.Scripts.Games.Abstract;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Games.RepeatColorsGame
{
    public class ModelRepeatColors : SingleInstanceObject<ModelRepeatColors>, ModelAbstract
    {
        public Sprite[] allColors;
        private Sprite[] currColors;
        private int currDifficulty;
        private int progress = 0;
        public GameId GameId { get; set; }

        public void StartGame()
        {
            Debug.LogFormat("Colors game started. Colors count: {0}", currColors.Length);
        }

        public void Create()
        {
            GameDifficulty diff = ControllerGlobal.Instance.currDifficulty;

            int count = 3;
            if (diff == GameDifficulty.Welcome) count = 3;
            else if (diff == GameDifficulty.Easy) count = 4;
            else if (diff == GameDifficulty.NotSoEasy) count = 6;
            else if (diff == GameDifficulty.Medium) count = 10;
            else if (diff == GameDifficulty.Hard) count = 14;

            currColors = GenerateColors(count);
            currDifficulty = (int)diff;
        }

        public void StopGame()
        {
            progress = 0;
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

        public bool Advance()
        {
            int maxLevel = ControllerGlobal.Instance.GetMaxLevelForCurrGame();
            progress = progress + 1;
            if (progress >= maxLevel) return false;

            return true;
        }
    }
}
