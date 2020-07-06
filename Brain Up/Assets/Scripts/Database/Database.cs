using Assets.Scripts.Framework.Database;
using Assets.Scripts.Games;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Database : AbstractDatabase<Database, DatabaseData>
    {
        private new DatabaseData data;

        #region Events
        public Action<int, int> onCoinsCountChanged = null;
        public Action<int> onLevelChanged = null;
        public Action<int, int> onHintsCountChanged=null;

        #endregion


        #region Getters & Setters

        public int Coins
        {
            get => data.coins;
            set
            {
                if (value < 0)
                    value = 0;
                onCoinsCountChanged?.Invoke(data.coins, value);
                data.coins = value;
            }
        }

        public int Level
        {
            get
            {
                return data.level;
            }
            set
            {
                onLevelChanged?.Invoke(value);
                data.level = value;
            }
        }


        public int Hints
        {
            get => data.hints;
            set
            {
                onHintsCountChanged?.Invoke(data.hints, value);
                data.hints = value;
            }
        }


        public int[] Stars
        {
            get => data.starPerLevel;
        }

        public List<int> BoughtItems => data.boughtItems;
        #endregion


        private new void Awake()
        {
            base.Awake();
            Load();
        }


        private new void OnDestroy()
        {
            base.OnDestroy();
        }

        private new void OnApplicationQuit()
        {
            base.OnApplicationQuit();
            Save();
        }

        private void OnApplicationFocus(bool focus)
        {
            if (!focus)
                Save();
        }




        #region Public API

        public new bool Load()
        {
            data = (DatabaseData)base.Load();
            data.hints = Mathf.Clamp(data.hints, 0, 200);

            Debug.Log("Coins count: " + data.coins);
            Debug.Log("Levels: " + data.level);
            Debug.Log("Hints count: " + data.hints);
            print("Load db path: " + databaseFile);


            var currGamesCount = Enum.GetValues(typeof(GameId)).Length;
            int lastGamesCount = data.gamesProgress.Length;
            Debug.LogFormat("currGamesCount: {1}; lastGamesCount: {0}", lastGamesCount, currGamesCount);
            if (currGamesCount > lastGamesCount)
            {
                int[] newData = new int[currGamesCount];
                for (int a = 0; a < lastGamesCount; ++a)
                    newData[a] = data.gamesProgress[a];
                data.gamesProgress = newData;
            }

            int newGamesCount = data.gamesProgress.Length;
            Debug.LogFormat("newGamesCount: {0}", newGamesCount);

            return data != null;
        }


        internal void SetStarsForLevel(int level, int nrStars)
        {
            if (level > Stars.Length)
                Array.Resize(ref data.starPerLevel, level + 10);
            data.starPerLevel[level] = nrStars;
        }

        internal void SetGameProgress(int gameId, int progress)
        {
            data.gamesProgress[gameId] = progress;
        }

        internal int GetGameProgress(int gameId)
        {
            return data.gamesProgress[gameId];
        }

        internal void SetGameProgressForDifficulty(int gameId, int difficulty, int progress)
        {
            data.gamesProgressPerDifficulty[gameId, difficulty] = progress;
        }


        internal void IncreaseGameProgressForDifficulty(int gameId, int difficulty)
        {
            int curr = GetGameProgressForDifficulty(gameId, difficulty);
            SetGameProgressForDifficulty(gameId, difficulty, curr+1);
        }

        internal int GetGameProgressForDifficulty(int gameId, int difficulty)
        {
            return data.gamesProgressPerDifficulty[gameId,difficulty];
        }

        internal void SetAsBought(int itemId)
        {
            if (IsItemBought(itemId))
                return;
            BoughtItems.Add(itemId);
        }


        internal bool IsItemBought(int itemId)
        {
            return BoughtItems.Exists((id) => id == itemId);
        }

        internal void SetAsNonBought(int itemId)
        {
            if (!IsItemBought(itemId))
                return;
            BoughtItems.Remove(itemId);
        }


        #endregion

    }
}
