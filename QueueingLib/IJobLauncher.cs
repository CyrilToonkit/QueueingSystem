using System;
using System.Collections.Generic;
using System.Text;

namespace QueueingLib
{
    public interface IJobLauncher
    {
        bool DoJob(string inJobName, object[] inArgs);

        string GetJobName(string inJobName);

        string GetCaption(string inJobName);

        int GetJobComplexity(string inJobName);
    }
}
