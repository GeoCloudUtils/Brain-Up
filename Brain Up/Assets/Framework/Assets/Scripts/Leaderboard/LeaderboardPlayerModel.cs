using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Framework.Assets.Scripts.Leaderboard
{
    public class LeaderboardPlayerModel
    {
        public string name;
        public Texture2D avatar;
        public int rank; 
        public int score;

        public LeaderboardPlayerModel(string name, Texture2D avatar, int rank, int score)
        {
            this.name = name;
            this.avatar = avatar;
            this.rank = rank;
            this.score = score;
        }

        public LeaderboardPlayerModel()
        {

        }
    }
}
