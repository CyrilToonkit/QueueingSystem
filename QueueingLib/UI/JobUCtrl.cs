using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace QueueingLib
{
    public partial class JobUCtrl : UserControl
    {
        delegate void VoidCallback();
        Job _job;
        public Job Job
        {
            get { return _job; }
            set { _job = value; }
        }

        public bool IsFinished
        {
            get { return _job._IsStarted && !_job._IsOnGoing; }
        }

        public JobUCtrl()
        {
            InitializeComponent();
        }

        public JobUCtrl(Job inJob)
        {
            InitializeComponent();
            _job = inJob;
            Name = _job.Name;
            RefreshValues();
        }

        public void RefreshValues()
        {
            if (InvokeRequired)
            {
                VoidCallback p = new VoidCallback(RefreshValues);
                this.Invoke(p);
            }
            else
            {
                _jobLabel.Text = _job.Name;
                _jobCaptionLabel.Text = _job.Caption;

                //Progress
                if (_job._IsOnGoing)
                {
                    _jobProgressBar.Style = ProgressBarStyle.Marquee;
                }
                else
                {
                    _jobProgressBar.Style = ProgressBarStyle.Continuous;

                    if (_job._IsSuccessful)
                    {
                        _jobProgressBar.Value = 100;
                        BackColor = Color.Gainsboro;
                    }
                    else if (_job._IsStarted)
                    {
                        BackColor = Color.Red;
                    }
                }
            }
        }
    }
}
