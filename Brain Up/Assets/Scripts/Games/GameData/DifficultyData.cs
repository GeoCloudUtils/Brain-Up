using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Games.GameData.DifficultyData
{
    [Serializable]
    public class DifficultyDataRowPair
    {
        public GameDifficulty difficulty;
        public int nrMaxLevels;
    }

    [Serializable]
    public class DifficultyDataRow
    {
        public GameId gameId;
        public int totalLevels;
        public DifficultyDataRowPair[] data;
    }

    [Serializable]
    [CreateAssetMenu(fileName = "DifficultyData", menuName = "ScriptableObjects/DifficultyData", order = 1)]
    public class DifficultyData : ScriptableObject
    {
        public DifficultyDataRow[] rows;
    }

    //private Dictionary<GameId, KeyValuePair<GameDifficulty,int>[]> MaxLevels = new Dictionary<GameId, KeyValuePair<GameDifficulty, int>>()
    //{
    //    { GameId., 2000 },
    //    { GameId.Acknowledge_History, 2000 },
    //    { GameId.Acknowledge_History, 2000 },
    //    { GameId.Acknowledge_History, 2000 },
    //    { GameId.Acknowledge_History, 2000 },
    //};
}
