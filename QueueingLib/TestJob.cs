using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace QueueingLib
{
    public class TestJob : IJobLauncher
    {

        #region IJobLauncher Members

        Random rnd = new Random();

        public bool DoJob(string inJobName, object[] inArgs)
        {
            //Wait for 2 seconds 
            Thread.Sleep(500);

            return true;
        }

        public string GetJobName(string inJobName)
        {
            return string.Format("Testjob {0}", Environment.TickCount);
        }

        public int GetJobComplexity(string inJobName)
        {
            return 1;
        }

        public string GetCaption(string inJobName)
        {
            return "Wait for 2 seconds and have 50% chances of crashing";
        }

        #endregion
    }
}
