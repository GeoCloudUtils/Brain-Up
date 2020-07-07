using Assets.Scripts.Games;
using Assets.Scripts.Games.__Other;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Screens
{
    public class Test_1 : MonoBehaviour
    {
        public Sprite[] testSprites;
        public bool[] testAnswers;
        public Texture2D flagsTexture_1;
        public Texture2D flagsTexture_2;
        public Image[] flags;
        const int flagsOnRow = 8;
        const int flagsCountOnOneTexture = 12 * flagsOnRow;

        private void Start()
        {
            //int firsTextureFlags = 12 * 9;
            //int secondTextureFlags = 13 * 9 + 2;
            //Texture2D texture = flagsTexture_2;
            //for (int a = 0; a < 14; ++a)
            //{
            //    for (int b = 0; b < 9; ++b)
            //    {
            //        int row = a;
            //        int col = b;
            //        Rect rect = new Rect(236 * col, texture.height - 160 * (row + 1), 226, 160);
            //        Debug.LogFormat("Row: {0}; Col; {1}; Rect: {2}", row, col, rect);
            //        flags[9 * a + b].sprite = Sprite.Create(texture, rect, new Vector2(0f, 0f), 100);
            //    }
            //}

            List<Tuple<int, Sprite>> list = GenerateFlags(100);
            int count = 0;
            foreach (Tuple<int, Sprite> tuple in list)
            {
                flags[count++].sprite = tuple.Item2;
            }
        }

        private List<Tuple<int,Sprite>> GenerateFlags(int count)
        {
            var list = new List<Tuple<int, Sprite>>();

            int flagsCount = flagsCountOnOneTexture + flagsCountOnOneTexture;
            for(int a=0; a < count; ++a)
            {
                Texture2D texture;
                int absoluteIndex = 0; 
                int index = 0;

                bool isOk = false;
                do
                {
                    absoluteIndex = GlobalRandomizer.Next(0, flagsCount);
                    if (absoluteIndex < flagsCountOnOneTexture)
                    {
                        index = absoluteIndex;
                        texture = flagsTexture_1;
                    }
                    else
                    {
                        index = absoluteIndex - flagsCountOnOneTexture;
                        texture = flagsTexture_2;
                    }

                    var result = list.Find((o) => { return o.Item1 == absoluteIndex; });
                    if (result == null)//if not exists in list
                        isOk = true;
                }
                while (!isOk);

               // Debug.LogFormat("Index: {0}; AbsIndex: {1}", index, absoluteIndex);

                int row = index / flagsOnRow;
                int col = index % flagsOnRow;
                Rect rect = new Rect(236 * col, texture.height - 160 * (row + 1), 226, 160);
                Sprite sprite = Sprite.Create(texture, rect, new Vector2(0f, 0f), 100);
                list.Add(new Tuple<int, Sprite>(absoluteIndex, sprite));
            }

            return list;
        }
    }
}
