/*
    Author: Ghercioglo "Romeon0" Roman
    Desc: ACK - Acknowledge
 */

using Assets.Scripts.Games;
using Assets.Scripts.Games.Abstract;
using Assets.Scripts.Games.VM_Letters;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Games.ACK_Flags
{
    public class ACK_FlagsModel : AbstractModel
    {
        private SimpleWordsDictionary countriesFlags;
        protected int gameId;
        public Texture2D flagsTexture_1;
        public Texture2D flagsTexture_2;
        private List<Tuple<int, Sprite, string>> data;
        public int progress = 1;
        public int questionsCount = 10;
        private const int FLAGS_ON_ROW = 8;
        private const int FLAGS_COUNT_ON_TEXTURE = 12 * FLAGS_ON_ROW;
        private const int FLAGS_COUNT = 4;

        private void Start()
        {
            countriesFlags = Resources.Load<SimpleWordsDictionary>("GameData/CountriesFlags");
        }

        public override void StartGame()
        {
        }

        public override void Create()
        {
            List<Tuple<int, Sprite>> flags = GenerateFlags(FLAGS_COUNT);
            data = new List<Tuple<int, Sprite, string>>();
            for(int a=0; a < flags.Count; ++a)
            {
                Tuple<int, Sprite> flag = flags[a];
                data.Add(new Tuple<int, Sprite, string>(flag.Item1, 
                    flag.Item2, 
                    countriesFlags.words[flag.Item1]));
            }
        }

        public override void StopGame()
        {
            progress = 1;
        }

        public List<Tuple<int, Sprite, string>> GetData()
        {
            return data;
        }

        private List<Tuple<int, Sprite>> GenerateFlags(int count)
        {
            var list = new List<Tuple<int, Sprite>>();

            int flagsCount = FLAGS_COUNT_ON_TEXTURE + FLAGS_COUNT_ON_TEXTURE;
            var rand = new System.Random();
            for (int a = 0; a < count; ++a)
            {
                Texture2D texture;
                int absoluteIndex = 0;
                int index = 0;

                bool isOk = false;
                do
                {
                    absoluteIndex = rand.Next(0, flagsCount);
                    if (absoluteIndex < FLAGS_COUNT_ON_TEXTURE)
                    {
                        index = absoluteIndex;
                        texture = flagsTexture_1;
                    }
                    else
                    {
                        index = absoluteIndex - FLAGS_COUNT_ON_TEXTURE;
                        texture = flagsTexture_2;
                    }

                    var result = list.Find((o) => { return o.Item1 == absoluteIndex; });
                    if (result == null)//if not exists in list
                        isOk = true;
                }
                while (!isOk);

                // Debug.LogFormat("Index: {0}; AbsIndex: {1}", index, absoluteIndex);

                int row = index / FLAGS_ON_ROW;
                int col = index % FLAGS_ON_ROW;
                Rect rect = new Rect(236 * col, texture.height - 160 * (row + 1), 226, 160);
                Sprite sprite = Sprite.Create(texture, rect, new Vector2(0f, 0f), 100);
                list.Add(new Tuple<int, Sprite>(absoluteIndex, sprite));
            }

            return list;
        }

        
        internal bool Advance()
        {
            if (progress >= questionsCount) return false;
            progress += 1;
            return true;
        }
    }
}
