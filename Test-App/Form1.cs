using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Test_App
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        string fileNames, filePath;

        private void Changed(object sender, FileSystemEventArgs e)
        {
            if (fileNames.Contains(e.Name))
            {
                string NotificationText = e.FullPath.ToString() + " is changed!";
                ShowNotification(NotificationText);
            }
          



        }



        private void Deleted(object sender, FileSystemEventArgs e)

        {
            if (fileNames.Contains(e.Name))
            {
                string NotificationText = e.FullPath.ToString() + " is Deleted!";
                ShowNotification(NotificationText);
            }
          

        }


        private void Renamed(object sender, FileSystemEventArgs e)

        {

            if (fileNames.Contains(e.Name))
            {
                string NotificationText = e.FullPath.ToString() + " is Renamed!";
                ShowNotification(NotificationText);
            }



        }


        private void FileCreated(object sender, FileSystemEventArgs e)

        {
            if (fileNames.Contains(e.Name))
            {
                string NotificationText = e.FullPath.ToString() + " is Add!";
                ShowNotification(NotificationText);
            }
        }


        delegate void SetTextCallback(string text);

        private void ShowNotification(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.lblNotification.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(ShowNotification);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.lblNotification.Text = text;
            }
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog x = new OpenFileDialog();
            x.Multiselect = true;
            x.ShowDialog();
            string[] arrayFileNames = x.SafeFileNames;
            foreach (string fileName in arrayFileNames)
                fileNames += fileName + " - ";

            filePath = x.FileName.Replace(x.SafeFileName, "");

            lblFilePath.Text = filePath;

            lblSelectedFiles.Text = fileNames;
        }

        private void btnWatcher_Click(object sender, EventArgs e)
        {

            FileSystemWatcher fwatcher = new FileSystemWatcher();

            fwatcher.Path = filePath;

            fwatcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName;

            fwatcher.Changed += new FileSystemEventHandler(Changed);
            fwatcher.Created += new FileSystemEventHandler(FileCreated);
            fwatcher.Deleted += new FileSystemEventHandler(Deleted);
            fwatcher.Renamed += new RenamedEventHandler(Renamed);

            fwatcher.Filter = "*.txt";


            fwatcher.EnableRaisingEvents = true;
        }

      


    }
}
