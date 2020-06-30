/*
    Author: Ghercioglo Roman
    Desc: VM - Visual Memory
 */

using Assets.Scripts.Games.Abstract;
using UnityEngine;

namespace Assets.Scripts.Games.VM_Colors
{
    public class VM_ColorsModel : AbstractModel
    {
        public Sprite[] allColors;
        private Sprite[] currColors;
        private int gameId;

        public override void StartGame()
        {
            int progress = Database.Instance.GetGameProgress(gameId);
            int count = 3 + progress / 15;
            if (count > 14)
                count = 14;
            Debug.LogFormat("Colors game started. Progress: {0}; Colors count: {1}", progress, count);
        }

        public override void Create()
        {
            gameId = (int)GameId.VisualMemory_Colors;
            int progress = Database.Instance.GetGameProgress(gameId);

            int count = 3 + progress / 15;
            if (count > 14)
                count = 14;

            currColors = GenerateColors(count);
        }

        public override void StopGame()
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
