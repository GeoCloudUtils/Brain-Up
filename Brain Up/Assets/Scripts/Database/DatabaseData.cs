using Assets.Scripts.Framework.Database;
using System.Collections.Generic;

namespace Assets.Scripts
{
    public class DatabaseData : AbstractDatabaseData
    {
        public int coins = 0;
        public int level = 1;
        public int hints = 3;
        public int[] starPerLevel = new int[200];
        public int[] gamesProgress = new int[20];
        public int[,] gamesProgressPerDifficulty = new int[20,5];
        public List<int> boughtItems = new List<int>();
    }
}
