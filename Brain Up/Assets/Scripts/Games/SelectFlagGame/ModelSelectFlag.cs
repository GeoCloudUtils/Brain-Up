/*
    Author: Ghercioglo "Romeon0" Roman
    Desc: ACK - Acknowledge
 */

using Assets.Scripts.Framework.Other;
using Assets.Scripts.Games;
using Assets.Scripts.Games.Abstract;
using Assets.Scripts.Games.Gamedata.TripleValueList;
using Assets.Scripts.Games.GameData.SimpleWordsDictionary;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Games.SelectFlagGame
{
    public class ModelSelectFlag : SingleInstanceObject<ControllerSelectFlag>, ModelAbstract
    {
        private TripleValueList allData;
        public Texture2D flagsTexture_1;
        public Texture2D flagsTexture_2;
        public int progress = 1;
        public int questionsCount = 10;
        private List<Tuple<int, Sprite, string>> data;
        private const int FLAGS_ON_ROW = 8;
        private const int FLAGS_COUNT_ON_TEXTURE = 12 * FLAGS_ON_ROW;
        private const int FLAGS_COUNT = 4;

        public GameId GameId { get; set; }

        private void Start()
        {
            allData = Resources.Load<TripleValueList>("GameData/CountriesData");
        }

        public void StartGame()
        {
        }

        public void Create()
        {
            if(allData == null)
                allData = Resources.Load<TripleValueList>("GameData/CountriesData");

            List<Tuple<int, Sprite>> flags = GenerateFlags(FLAGS_COUNT);
            data = new List<Tuple<int, Sprite, string>>();
            for(int a=0; a < flags.Count; ++a)
            {
                Tuple<int, Sprite> flag = flags[a];
                data.Add(new Tuple<int, Sprite, string>(flag.Item1, 
                    flag.Item2, 
                    (string)allData.rows[flag.Item1].item1.Clone()));
            }

            ControllerGlobal global = ControllerGlobal.Instance;
            questionsCount = global.GetMaxLevel(global.currGameId, global.currDifficulty);
        }

        public void StopGame()
        {
            progress = 1;
        }

        public List<Tuple<int, Sprite, string>> GetData()
        {
            return data;
        }

        public void SetDictionary(TripleValueList data)
        {
            allData = data;
        }

        private List<Tuple<int, Sprite>> GenerateFlags(int count)
        {
            var list = new List<Tuple<int, Sprite>>();

            int flagsCount = FLAGS_COUNT_ON_TEXTURE + FLAGS_COUNT_ON_TEXTURE;
            for (int a = 0; a < count; ++a)
            {
                Texture2D texture;
                int absoluteIndex = 0;
                int index = 0;

                bool isOk = false;
                do
                {
                    absoluteIndex = GlobalRandomizer.Next(0, flagsCount);
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

                Debug.LogFormat("Index: {0}; AbsIndex: {1}", index, absoluteIndex);

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
