using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.LettersGames
{
    public static class ListExtensions
    {
        public static void Shuffle<T>(this IList<T> list, Action<int, int> callback=null)
        {
            Random rand = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rand.Next(n + 1);
                list.Swap(k, n);
                callback?.Invoke(k, n);
            }
        }

        public static void Swap<T>(this IList<T> list, int a, int b)
        {
            T value = list[a];
            list[a] = list[b];
            list[b] = value;
        }
    }
}
