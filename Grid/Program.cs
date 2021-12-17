using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;

namespace Grid
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Trace.WriteLine("Grid.exe's current directory is " + Environment.CurrentDirectory);

            /*
            //.Net version: Apporach 1
            RegistryKey installed_versions = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP");
            string[] version_names = installed_versions.GetSubKeyNames();
            bool v2_exist = false;
            foreach (string version in version_names)
            {
                if (version.StartsWith("v2"))
                {
                    v2_exist = true;
                    break;
                }
            }
            if (!v2_exist)
            {
                MessageBox.Show("UYARI : Bilgisayarýnýzda .Net Framework 2 kurulu deðil!");
                Environment.Exit(0);
                //Application.Exit();
            }

            */

            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("tr-TR");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("tr-TR");

            // splash screen, which is terminated in FormMain
            MySplashForm.ShowSplash();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new XtraForm1());
        }
    }
}