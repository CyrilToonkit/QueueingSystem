using System;
using System.Collections.Generic;
using System.IO;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Text;
using System.Net;
using System.Threading;
using System.Diagnostics;

namespace QueueingLib
{
    /// <summary>
    /// Job encapsulates the operation.
    /// </summary>
    public class Job : IComparable
    {
        public Job()
        {
            _jobLauncher = new TestJob();
            _name = _jobLauncher.GetJobName("");
        }

        public Job(IJobLauncher inLauncher, string inCommand, object[] inArgs)
        {
            _jobLauncher = inLauncher;
            _commandName = inCommand;
            _args = inArgs;
            _name = inLauncher.GetJobName(inCommand);
        }

        Stopwatch _watch = new Stopwatch();


        public float Duration
        {
            get { return (float)_watch.ElapsedMilliseconds / 1000f; }
        }

        public float ComplexityPerSecond
        {
            get { return (float)Complexity / Duration; }
        }

        IJobLauncher _jobLauncher;
        internal IJobLauncher JobLauncher
        {
            get { return _jobLauncher; }
            set { _jobLauncher = value; }
        }

        string _name = "Undefined";
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        int _id = 0;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        string _commandName = "Default";
        public string CommandName
        {
            get { return _commandName; }
            set { _commandName = value; }
        }

        object[] _args = new object[0]{};
        public object[] Args
        {
            get { return _args; }
        }

        public string Caption
        {
            get
            {
                if (_IsOnGoing)
                {
                    return _jobLauncher.GetCaption(_commandName);
                }

                return Status == QueueStatuses.Error ? "Error (" + _message + ")" : Status.ToString();
            }
        }

        public int Complexity
        {
            get { return _jobLauncher.GetJobComplexity(_commandName); }
        }

        bool _cancelled;
        public bool Cancelled
        {
            get { return _cancelled; }
            set { _cancelled = value; }
        }

        string _message;
        public string Message
        {
            get { return _message; }
        }

        public bool _IsStarted = false;
        public bool _IsOnGoing = false;
        public bool _IsFinished = false;
        public bool _IsSuccessful = false;

        public QueueStatuses Status
        {
            get
            {
                if (_IsStarted)
                {
                    if (_IsOnGoing)
                    {
                        return QueueStatuses.Running;
                    }
                    else
                    {
                        if (_IsSuccessful)
                        {
                            return QueueStatuses.Finished;
                        }
                        else
                        {
                            return QueueStatuses.Error;
                        }
                    }
                }

                return QueueStatuses.Waiting;
            }
        }

        public delegate void StartingDelegate(Job job);
        public delegate void EndingDelegate(Job job, bool isSuccess);

        public event StartingDelegate Starting;
        public event EndingDelegate Completed;

        /// <summary>
        /// Starts the job.
        /// </summary>
        public void Start()
        {
            _IsStarted = true;
            _IsOnGoing = true;
            _IsSuccessful = false;

            _watch.Start();

            Starting(this);

            //Job Implementation
            try
            {
                if (!_cancelled)
                {
                    DoJob();
                    _IsSuccessful = true;
                }
                else
                {
                    _IsSuccessful = false;
                    _message = "Cancelled by user";
                }
            }
            catch (Exception e)
            {
                _IsSuccessful = false;
                _message = e.Message;
            }
            finally
            {
                _IsFinished = true;
                _IsOnGoing = false;
                /* raise a completed event. */
                Completed(this, _IsSuccessful);
                _watch.Stop();
            }
        }

        protected virtual void DoJob()
        {
            _jobLauncher.DoJob(_commandName, _args);
        }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            Job otherJob = obj as Job;
            if (otherJob == null)
            {
                return -1;
            }

            if (Status == QueueStatuses.Finished || Status == QueueStatuses.Error)
            {
                if (otherJob.Status == QueueStatuses.Finished || otherJob.Status == QueueStatuses.Error)
                {
                    return Id.CompareTo(otherJob.Id);
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                if (otherJob.Status == QueueStatuses.Finished || otherJob.Status == QueueStatuses.Error)
                {
                    return -1;
                }
                else
                {
                    return Id.CompareTo(otherJob.Id);
                }
            }
        }

        #endregion
    }
}
