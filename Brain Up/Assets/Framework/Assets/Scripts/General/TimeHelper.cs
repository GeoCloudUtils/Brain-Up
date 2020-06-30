using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Framework.General
{
    class TimeHelper
    {
        //INFO: Format time to format MM:SS (minutes:seconds)
        public static string FormatMMSS(double time)
        {
            TimeSpan ts = TimeSpan.FromSeconds(time);
            return $"{ts.Minutes:00}:{ts.Seconds:00}"; ;
        }
    }
}
