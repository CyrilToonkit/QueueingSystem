using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace QueueingLib
{
    public static class TimeHelper
    {
        static List<Stopwatch> _watches = new List<Stopwatch>();
        static Stopwatch _watch = new Stopwatch();

        public static Stopwatch CurrentWatch
        {
            get
            {
                if (_watches.Count > 0)
                {
                    return _watches[_watches.Count - 1];
                }

                return null;
            }
        }

        public static void StartProcess()
        {
            _watches.Add(new Stopwatch());
            CurrentWatch.Start();
        }

        public static string StopAndPrintProcess(string inProcessName)
        {
            return Pad(_watches.Count - 1) + string.Format("END PROCESS {0} ({1})", inProcessName, TimeHelper.FromSeconds(StopProcess()));
        }

        private static string Pad(int p)
        {
            string pad = "";

            for (; p > 0; p--)
            {
                pad += " ";
            }

            return pad;
        }

        public static float StopProcess()
        {
            if (CurrentWatch == null)
            {
                return 0f;
            }
            CurrentWatch.Stop();
            float seconds = (float)CurrentWatch.Elapsed.TotalSeconds;
            _watches.Remove(CurrentWatch);
            return seconds;
        }

        public static string FromSeconds(float seconds)
        {
            string sTime = string.Empty;

            int hours = (int)Math.Floor((double)seconds / 3600);
            int minutes = (int)Math.Floor((seconds - hours * 3600) / 60);
            seconds = seconds - hours * 3600 - minutes * 60;

            if (hours > 0) { sTime += string.Format("{0}h ", hours); }
            if (minutes > 0) { sTime += string.Format("{0}m ", minutes); }

            return sTime + string.Format("{0:0.00}s", seconds);
        }
    }
}
