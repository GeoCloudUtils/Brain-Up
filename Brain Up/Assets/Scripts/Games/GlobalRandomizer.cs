using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Games
{
    public static class GlobalRandomizer
    {
        public static readonly int SEED = 35908509;
        private static System.Random rand = new System.Random(SEED);

        public static int Next(int min, int max)
        {
            return rand.Next(min, max);
        }
    }
}
