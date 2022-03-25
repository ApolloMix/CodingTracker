using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker
{
    public class CodingTracker
    {
        public static DateTime StartTime { get; private set; }
        public static DateTime EndTime { get; private set; }
        public static TimeSpan Duration { get; private set; }



        public static void StartSession()
        {
            StartTime = DateTime.Now;
        }

        public static void EndSession()
        {
            EndTime = DateTime.Now;
            Duration = EndTime - StartTime;
        }
    }
}
