using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using QueueingLib;
using System.Threading;

namespace QueueingTest
{
    public partial class QueueingTestForm : Form
    {
        public QueueingTestForm()
        {
            InitializeComponent();
            queueUCtrl1.Bind(Queue.GetInstance());
            Queue.GetInstance().JobAdded += new Queue.JobAddedDelegate(QueueingTestForm_JobAdded);
            Queue.GetInstance().JobChanged += new Queue.JobChangedDelegate(QueueingTestForm_JobChanged);
            Queue.GetInstance().QueueEnding += new Queue.QueueEndingDelegate(QueueingTestForm_QueueEnding);
        }

        void QueueingTestForm_QueueEnding(object sender, EventArgs data)
        {
            if (InvokeRequired)
            {
                QueueUCtrl.RegularCallback p = new QueueUCtrl.RegularCallback(QueueingTestForm_QueueEnding);
                Invoke(p, new object[] { sender , data});
            }
            else
            {
                splitContainer1.Panel2Collapsed = true;
                splitContainer1.Invalidate();
            }
        }

        void QueueingTestForm_JobAdded(object sender, JobEventArgs data)
        {
            if (InvokeRequired)
            {
                QueueUCtrl.JobCallback p = new QueueUCtrl.JobCallback(QueueingTestForm_JobAdded);
                Invoke(p, new object[] { sender, data });
            }
            else
            {
                splitContainer1.Panel2Collapsed = false;
            }
        }

        void QueueingTestForm_JobChanged(object sender, JobEventArgs data)
        {
            if (InvokeRequired)
            {
                QueueUCtrl.JobCallback p = new QueueUCtrl.JobCallback(QueueingTestForm_JobChanged);
                Invoke(p, new object[] { sender, data });
            }
            else
            {
                foreach (Job job in data.Jobs)
                {
                    if (job._IsFinished && !job._IsSuccessful)
                    {
                        MessageBox.Show(job.Message, job.Name + " encountered an error");
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Queue.Add(new Job());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Queue.BeginUpdate();

            for (var i = 0; i < 3000; i++)
            {
                Queue.Add(new Job());
            }

            Queue.EndUpdate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel2Collapsed = !splitContainer1.Panel2Collapsed;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ProcessForm.ShowProcess("Test process", "This is just a test process that'll take 5 seconds...",this);
            Thread.Sleep(5000);
            ProcessForm.CloseForm();
        }
    }
}
