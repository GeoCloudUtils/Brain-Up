using System;
using System.Collections.Generic;
using System.Linq;
/*
    Author: Ghercioglo Roman (Romeon0)
 */

using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    class ExecuteTimeTest
    {
        public static void Check(Action func)
        {
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();

            watch.Start();
            func();
            watch.Stop();

            TimeSpan time = TimeSpan.FromMilliseconds(watch.ElapsedMilliseconds);
            Debug.LogFormat("ExecuteTimeTest end. Elapsed time: {0} minutes {1} seconds {0} miliseconds.", time.Minutes, time.Seconds, time.Milliseconds);
        }

    }
}
