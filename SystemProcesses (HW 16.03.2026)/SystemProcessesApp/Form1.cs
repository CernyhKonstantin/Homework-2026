using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace SystemProcessesApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string fileName = $"ProcessList_{timestamp}.txt";

            using StreamWriter writer = new StreamWriter(fileName);

            Process[] processes = Process.GetProcesses();

            foreach (Process proc in processes)
            {
                try
                {
                    string line = $"{proc.ProcessName}\t{proc.Id}";
                    listViewProcesses.Items.Add(new ListViewItem(new string[] { proc.ProcessName, proc.Id.ToString() }));
                    writer.WriteLine(line);
                }
                catch
                {
                }
            }

            writer.Flush();
        }

        private void OpenDouMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://dou.ua",
                UseShellExecute = true
            });
        }
    }
}