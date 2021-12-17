using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Grid
{
    public partial class MySplashForm : Form
    {
        private static Thread _splashThread;
        private static MySplashForm _splashForm;

        public MySplashForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Show the Splash Screen (Loading...)
        /// </summary>
        public static void ShowSplash()
        {
            if (_splashThread == null)
            {
                // show the form in a new thread
                _splashThread = new Thread(new ThreadStart(DoShowSplash));
                _splashThread.IsBackground = true;
                _splashThread.Start();
            }
        }

        // called by the thread
        private static void DoShowSplash()
        {
            if (_splashForm == null)
                _splashForm = new MySplashForm();

            // create a new message pump on this thread (started from ShowSplash)
            Application.Run(_splashForm);
        }

        /// <summary>
        /// Close the splash (Loading...) screen
        /// </summary>
        public static void CloseSplash()
        {
            // need to call on the thread that launched this splash
            if (_splashForm.InvokeRequired)
                _splashForm.Invoke(new MethodInvoker(CloseSplash));
            else
                Application.ExitThread();
        }

    }
}