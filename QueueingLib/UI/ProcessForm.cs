using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace QueueingLib
{

    /// <summary>
    /// Source : http://www.codeproject.com/Articles/5454/A-Pretty-Good-Splash-Screen-in-C
    /// </summary>
    public partial class ProcessForm : Form
    {
        public ProcessForm()
        {
            InitializeComponent();
        }

        public string Message
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }

        // Threading
        static ProcessForm _processForm = null;
        static Thread _thread = null;

        // ************* Static Methods *************** //

        // A static method to create the thread and 
        // launch the SplashScreen.
        static public void ShowSplashScreen()
        {
            // Make sure it is only launched once.
            if (_processForm != null)
                return;
            _thread = new Thread(new ThreadStart(ProcessForm.ShowForm));
            _thread.IsBackground = true;
            _thread.SetApartmentState(ApartmentState.STA);
            _thread.Start();
        }

        public static void ShowProcess(string inTitle, string inMessage)
        {
            ShowProcess(inTitle, inMessage, null);
        }

        public static void ShowProcess(string inTitle, string inMessage, Form inParentForm)
        {
            // Make sure it is only launched once.
            if (_processForm != null)
                return;
            _processForm = new ProcessForm();
            _processForm.Text = inTitle;
            _processForm.Message = inMessage;

            if (inParentForm != null)
            {
                Point centerLoc = new Point(inParentForm.Location.X + (inParentForm.Width - _processForm.Width) / 2,
                    inParentForm.Location.Y + (inParentForm.Height - _processForm.Height) / 2);

                _processForm.StartPosition = FormStartPosition.Manual;
                _processForm.Location = centerLoc;
            }

            _thread = new Thread(new ThreadStart(ProcessForm.ShowForm));
            _thread.IsBackground = true;
            _thread.SetApartmentState(ApartmentState.STA);
            _thread.Start();
        }

        // A property returning the splash screen instance
        static public ProcessForm ProcessBox
        {
            get
            {
                return _processForm;
            }
        }

        // A private entry point for the thread.
        static private void ShowForm()
        {
            Application.Run(_processForm);
        }

        // A static method to close the SplashScreen
        static public void CloseForm()
        {
            if (_processForm != null && _processForm.IsDisposed == false)
            {
                // Make it start going away.
                _thread = null;  // we do not need these any more.
                _processForm.Invoke(new QueueingLib.QueueUCtrl.VoidCallback(_processForm.Close));
                _processForm = null;
            }
        }

        // A static method to close the SplashScreen
        static public void ChangeMessage(string inMessage)
        {
            if (_processForm != null && _processForm.IsDisposed == false)
            {
                _processForm.Invoke(new QueueingLib.QueueUCtrl.RegularCallback(_processForm.SetMessage), new object[] { inMessage, new EventArgs() });
            }
        }

        private void SetMessage(object sender, EventArgs inArgs)
        {
            textBox1.Text = (string)sender;
        }
    }
}
