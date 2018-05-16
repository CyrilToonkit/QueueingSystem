using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace QueueingLib
{
    public partial class QueueUCtrl : UserControl
    {
        public delegate void VoidCallback();
        public delegate void RegularCallback(object sender, EventArgs data);
        public delegate void JobCallback(object sender, JobEventArgs data);
        Queue _queue = null;

        Dictionary<int, JobUCtrl> _jobs = new Dictionary<int, JobUCtrl>();

        List<Job> _activeJobs = new List<Job>();

        public QueueUCtrl()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
        }

        public void Bind(Queue inQueue)
        {
            _queue = inQueue;
            
            //Register Events
            _queue.JobAdded += new Queue.JobAddedDelegate(_queue_JobAdded);
            _queue.JobChanged += new Queue.JobChangedDelegate(_queue_JobChanged);
        }

        void _queue_JobAdded(object sender, JobEventArgs data)
        {
            /*
            foreach (Job job in data.Jobs)
            {
                AddJobCtrl(job);
            }
             */
            _activeJobs.AddRange(data.Jobs);
            RefreshValues();
        }

        void _queue_JobChanged(object sender, JobEventArgs data)
        {
            /*
            foreach (Job job in data.Jobs)
            {
                if (_jobs.ContainsKey(job.Id))
                {
                    _jobs[job.Id].RefreshValues();
                }
            }*/
            RefreshValues();
        }

        /*
        private void AddJobCtrl(Job job)
        {
            JobUCtrl ctrl = new JobUCtrl(job);
            _jobsPanel.Controls.Add(ctrl);
            ctrl.Dock = DockStyle.Top;
            ctrl.BringToFront();

            _jobs.Add(job.Id, ctrl);
            DropFinished();
        }

        private void DropFinished()
        {
            _jobsPanel.SuspendLayout();
            foreach (JobUCtrl job in _jobs.Values)
            {
                if (job.Job._IsFinished)
                {
                    job.BringToFront();
                }
            }
            _jobsPanel.ResumeLayout(true);
        }
        */

        public void RefreshValues()
        {
            if (InvokeRequired)
            {
                VoidCallback p = new VoidCallback(RefreshValues);
                this.Invoke(p);
            }
            else
            {
                dataGridView1.DataSource = null;           

                //int nbJobs = _jobs.Count;
                int nbJobs = _activeJobs.Count;
                int nbFinishedJobs = 0;

                long totalComplexity = 0;
                long doneComplexity = 0;

                foreach (Job job in _activeJobs)
                {
                    totalComplexity += job.Complexity;

                    if (job.Status == QueueStatuses.Finished || job.Status == QueueStatuses.Error)
                    {
                        nbFinishedJobs++;
                        doneComplexity += job.Complexity;
                    }
                }
                /*
                foreach (JobUCtrl job in _jobs.Values)
                {
                    totalComplexity += job.Job.Complexity;

                    if (job.IsFinished)
                    {
                        nbFinishedJobs++;
                        doneComplexity += job.Job.Complexity;
                    }
                }
                */
                if (nbFinishedJobs == nbJobs)
                {
                    Clear();
                    nbJobs = nbFinishedJobs = 0;

                    SetButtons(false);
                }
                else
                {
                    SetButtons(true);
                    _activeJobs.Sort();
                    dataGridView1.DataSource = _activeJobs;

                    int counter = 0;
                    foreach(DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (_activeJobs[counter].Status == QueueStatuses.Finished)
                        {
                            row.DefaultCellStyle.BackColor = Color.Gray;
                        }
                        else if (_activeJobs[counter].Status == QueueStatuses.Error)
                        {
                            row.DefaultCellStyle.BackColor = Color.Red;
                        }
                        else if (_activeJobs[counter].Status == QueueStatuses.Running)
                        {
                            row.DefaultCellStyle.BackColor = Color.PaleGreen;
                        }

                        counter++;
                    }
                }

                //Caption
                string StatusText = _queue.Status.ToString();
                
                float remainingSeconds = (float)(totalComplexity - doneComplexity) / _queue.ComplexityPerSecond;
                if (remainingSeconds > 0 &&  !float.IsInfinity(remainingSeconds))
                {
                    StatusText += " " + TimeHelper.FromSeconds(remainingSeconds);
                }

                //DropFinished();

                _queueStatusLabel.Text = StatusText;
                _queueCountLabel.Text = string.Format("{0}/{1} jobs finished ({2}%)", nbFinishedJobs, nbJobs, nbJobs == 0 ? 0 : (nbFinishedJobs * 100) / nbJobs);
                _queueProgressBar.Value = (int)(100 * ((float)doneComplexity / (totalComplexity != 0 ? totalComplexity : 100)));
            }
        }

        private void SetButtons(bool inValue)
        {
            PauseBT.Enabled = true;
            CancelBT.Enabled = true;
        }

        private void Clear()
        {
            /*
            _jobs.Clear();
            _jobsPanel.Controls.Clear();
             * */
            _activeJobs.Clear();
        }

        private void PauseBT_Click(object sender, EventArgs e)
        {
            PauseBT.Image = _queue.Play ? QueueingLib.Properties.Resources.PlayQueue : QueueingLib.Properties.Resources.PauseQueue;
            _queue.Play = !_queue.Play;
        }

        private void CancelBT_Click(object sender, EventArgs e)
        {
            _queue.Cancel = true;
        }
    }
}
