using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using QueueingLib;

namespace QueueingTest
{
    public class TestJob : IJobLauncher
    {
        #region IJobLauncher Members

        public bool DoJob(string inJobName, object[] inArgs)
        {
            //Wait for .5 seconds 
            //Thread.Sleep(500);

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
            return "Wait for 2 seconds";
        }

        #endregion
    }
}
