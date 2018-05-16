using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace QueueingLib
{
    public class JobEventArgs : EventArgs
    {
        List<Job> _jobs;
        public List<Job> Jobs
        {
            get { return _jobs; }
        }

        public JobEventArgs(List<Job> inJobs)
        {
            _jobs = inJobs; 
        } 
    }

    public enum QueueStatuses
    {
        Waiting, Paused, Running, Finished, Error
    }

    public class Queue
    {
        #region EVENTS

        // -- JobAdded EVENT --
        // The delegate
        public delegate void JobAddedDelegate(object sender, JobEventArgs data);
        // The event
        public event JobAddedDelegate JobAdded;
        // The method which fires the Event
        protected void OnJobAdded(object sender, JobEventArgs data)
        {
            // Check if there are any Subscribers   
            if (JobAdded != null)
            {
                // Call the Event
                JobAdded(sender, data);
            }
        }

        // -- JobChanged EVENT --
        // The delegate
        public delegate void JobChangedDelegate(object sender, JobEventArgs data);
        // The event
        public event JobChangedDelegate JobChanged;
        // The method which fires the Event
        protected void OnJobChanged(object sender, JobEventArgs data)
        {
            // Check if there are any Subscribers   
            if (JobChanged != null)
            {
                // Call the Event
                JobChanged(sender, data);
            }
        }

        public delegate void QueueEndingDelegate(object sender, EventArgs data);
        // The event
        public event QueueEndingDelegate QueueEnding;
        // The method which fires the Event
        protected void OnQueueEnding(object sender, EventArgs data)
        {
            // Check if there are any Subscribers   
            if (QueueEnding != null)
            {
                // Call the Event
                QueueEnding(sender, data);
            }
        }

        #endregion

        //Singleton declaration
        private Queue()
        {

        }

        private static Queue instance;

        public static Queue GetInstance()
        {
            if (instance == null)
            {
                instance = new Queue();
            }

            return instance;
        }

        bool _isLive = true;
        public bool IsLive
        {
            get { return _isLive; }
        }

        int _updateDepth = 0;

        bool _play = true;
        public bool Play
        {
            get { return _play; }
            set
            {
                _play = value;
                if (value)
                {
                    EvaluateQueue();
                }
            }
        }

        bool _cancel = false;
        public bool Cancel
        {
            get { return _cancel; }
            set { _cancel = value; }
        }

        List<Thread> _threads = new List<Thread>();
        List<Job> _jobs = new List<Job>();
        List<Job> _awaitingJobs = new List<Job>();
        Dictionary<string, object> _data = new Dictionary<string, object>();

        int _activeJobsCount = 0;
        object _lockObject = new object();

        public QueueStatuses Status
        {
            get { return _isLive ? _activeJobsCount > 0 ? QueueStatuses.Running : QueueStatuses.Waiting : QueueStatuses.Paused; }
        }

        public int NbJobs
        {
            get
            {
                lock (_lockObject)
                {
                    return _jobs.Count;
                }
            }
        }

        public int NbFinishedJobs
        {
            get
            {
                int nbFinished = 0;
                lock (_lockObject)
                {
                    foreach (Job job in _jobs)
                    {
                        if (job._IsSuccessful)
                        {
                            nbFinished++;
                        }
                    }
                }
                return nbFinished;
            }
        }

        int _jobsTotal = 0;
        public int JobsTotal
        {
            get { return _jobsTotal; }
            set {_jobsTotal = value; }
        }

        int _jobsDone = 0;

        float _complexityPerSecond = 0f;
        public float ComplexityPerSecond
        {
            get { return _complexityPerSecond; }
        }

        public static void BeginUpdate()
        {
            instance._isLive = false;
            instance._updateDepth++;
        }

        public static void EndUpdate()
        {
            if (!instance._isLive)
            {
                instance._updateDepth--;
                if (instance._updateDepth == 0)
                {
                    instance._isLive = true;

                    if (instance._awaitingJobs.Count > 0)
                    {
                        instance.OnJobAdded(instance, new JobEventArgs(instance._awaitingJobs));
                        instance._awaitingJobs.Clear();
                    }

                    instance.EvaluateQueue();
                }
            }
            else
            {
                instance._updateDepth = 0;
            }
        }

        public static void Add(Job newJob)
        {
            try
            {
                newJob.Id = instance.JobsTotal;
                instance.JobsTotal++;
                ThreadStart tsDelegate = new ThreadStart(newJob.Start);
                newJob.Starting += new Job.StartingDelegate(JobStarting);
                newJob.Completed += new Job.EndingDelegate(JobCompleted);
                Thread t = new Thread(tsDelegate);
                t.Name = newJob.Name;
                instance._threads.Add(t);
                instance._jobs.Add(newJob);

                if (instance._isLive)
                {
                    instance.OnJobAdded(instance, new JobEventArgs(new List<Job> { newJob }));
                    instance.EvaluateQueue();
                }
                else
                {
                    instance._awaitingJobs.Add(newJob);
                }
            }
            catch (Exception e) { MessageBox.Show(e.Message); }
        }

        /// <summary>
        /// Gets triggered when the job has started.
        /// </summary>
        static void JobStarting(Job inJob)
        {
            instance.OnJobChanged(instance, new JobEventArgs(new List<Job> { inJob }));
        }

        /// <summary>
        /// Gets trigerred when the job has ended.
        /// </summary>
        static void JobCompleted(Job inJob, bool isSuccess)
        {
            /* When changing the global download count, be sure to make it thread safe via locking. */
            lock (instance._lockObject)
            {
                instance._activeJobsCount--;
            }

            instance.OnJobChanged(instance, new JobEventArgs(new List<Job> { inJob }));
            instance.RemoveFromPool(inJob);
            instance._complexityPerSecond = ((instance._complexityPerSecond * instance._jobsDone) + inJob.ComplexityPerSecond) / (instance._jobsDone + 1);
            instance._jobsDone++;
            instance.EvaluateQueue();
        }

        /// <summary>
        /// Inspects the internal pool and starts off more downloads threads, if freed.
        /// </summary>
        private void EvaluateQueue()
        {
            if (_play)
            {
                int j = 0;
                int limit = 1;
                int iCount = 0;
                lock (_lockObject)
                {
                    iCount = _jobs.Count;
                }
                if (iCount != 0)
                {
                    if (_cancel)
                    {
                        lock (_lockObject)
                        {
                            foreach (Job job in _jobs)
                            {
                                if (job._IsStarted == false)
                                {
                                    job.Cancelled = true;
                                }
                            }
                        }
                    }

                    //Start threads
                    foreach (Thread thread in _threads)
                    {
                        lock (_lockObject)
                        {
                            Job job = ((Job)_jobs[j]);
                            if (job._IsStarted == false)
                            {
                                thread.Start();
                                _activeJobsCount++;
                            }
                            if (_activeJobsCount == limit)
                            {
                                break;
                            }
                            j++;
                        }
                    }
                }
                else
                {
                    //All the jobs came to an end
                    instance.OnQueueEnding(instance, new EventArgs());
                    instance._data.Clear();
                    instance._complexityPerSecond = 0f;
                    instance._jobsDone = 0;
                    _cancel = false;
                }
            }
        }

        /// <summary>
        /// Removes the given job and its thread from the pool.
        /// </summary>
        private void RemoveFromPool(Job inJob)
        {
            int i = 0;
            foreach (Job job in _jobs)
            {
                if (job == inJob)
                {
                    //Remove the job from pool
                    lock (_lockObject)
                    {
                        _threads.Remove(_threads[i]);
                        _jobs.Remove(job);
                        break;
                    }
                }
                i++;
            }
        }

        public static void AddData(string inKey, object inValue)
        {
            if (instance._data.ContainsKey(inKey))
            {
                instance._data[inKey] = inValue;
            }
            else
            {
                instance._data.Add(inKey, inValue);
            }
        }

        public static object GetData(string inKey)
        {
            if (instance._data.ContainsKey(inKey))
            {
                return instance._data[inKey];
            }

            return null;
        }

        /// <summary>
        /// Kills all active threads.
        /// </summary>
        private void KillThreads()
        {
            foreach (Thread t in _threads)
            {
                if (t.IsAlive)
                {
                    t.Abort();
                }
            }
            _threads = new List<Thread>();
            _jobs = new List<Job>();
        }

        public static void ProcessBox(string inTitle, string inMessage)
        {
            throw new NotImplementedException();
        }
    }
}
