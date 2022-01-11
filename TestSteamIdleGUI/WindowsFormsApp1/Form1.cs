using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestSteamIdleGui
{
    public partial class MainWindow : Form
    {
        static readonly string AppIdsTxt = "apps.txt";
        static readonly string IdleExeName = "TestSteamIdle";
        static readonly string IdleExe = IdleExeName + ".exe";

        static readonly string path = AppDomain.CurrentDomain.BaseDirectory;
        static readonly string pathTxt = Path.Combine(path, AppIdsTxt);
        static readonly string pathExe = Path.Combine(path, IdleExe);
        readonly ProcessStartInfo startInfo = new ProcessStartInfo(pathExe);

        public MainWindow()
        {
            InitializeComponent();
        }

        void OutputText(string text) => OutputBox.AppendText(text + "\n");

        void PaintText(string text, int r, int g, int b)
        {
            int index = OutputBox.Text.IndexOf(text);
            int lenght = text.ToString().Length;

            OutputBox.Select(index, lenght);
            OutputBox.SelectionColor = Color.FromArgb(255, r, g, b);
        }

        // apps.txt
        void LoadAppList()
        {
            foreach (string line in File.ReadLines(pathTxt))
            {
                List<string> data = line.Split(',').ToList();
                if (data.Count > 0)
                    AppList.Items.Add(data[0], bool.Parse(data[1]));
            }

            AppCounter.Text = "Apps: " + AppList.Items.Count;
            idleCounter.Text = "To idle: " + AppList.CheckedItems.Count;
        }

        private void SaveAppList()
        {
            List<string> data = new List<string>();

            for (int i=0; i < AppList.Items.Count; i++)
            {
                var id = int.Parse(AppList.Items[i].ToString());
                bool state = AppList.GetItemChecked(i);

                data.Add(id + "," + state.ToString());
            }

            System.IO.File.WriteAllLines("apps.txt", data);

            AppCounter.Text = "Apps: " + AppList.Items.Count;
            idleCounter.Text = "To idle: " + AppList.CheckedItems.Count;
        }


        // init
        private void MainWindow_Load(object sender, EventArgs e)
        {
            if (!File.Exists(pathTxt))
                File.Create(pathTxt).Close();

            LoadAppList();

            OutputText("Initialized.");

            if (Process.GetProcessesByName("steam").Length == 0)
            {
                string error_msg = "Your steam client is not running.";
                OutputText(error_msg);
                PaintText(error_msg, 255, 128, 128);
                return;
            }

            string success_msg = "Steam client is running.";
            OutputText(success_msg);
            PaintText(success_msg, 128, 255, 128);

            int apps_running = Process.GetProcessesByName(IdleExeName).Length;
            if (apps_running != 0)
                OutputText("currently there are " + apps_running.ToString() + " apps idling.");
        }

        // start idle button
        void StartIdle()
        {
            OutputText("\nStarting Idle...");

            foreach (object AppID in AppList.CheckedItems)
            {
                startInfo.Arguments = AppID.ToString();
                Process.Start(startInfo);

                OutputText("\t• Started idle process for app " + AppID.ToString());
                PaintText(AppID.ToString(), 128, 255, 128);
            }
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (AppList.CheckedItems.Count > 31)
            {
                string error_msg = "App limit exceeded you cannot run more than 31 apps at once.";
                OutputText(error_msg);
                PaintText(error_msg, 255, 128, 128);

                return;
            }

            if (!File.Exists(pathExe))
            {
                string error_msg = IdleExeName + ".exe is missing.";
                OutputText(error_msg);
                PaintText(error_msg, 255, 128, 128);
                return;
            }

            if (Process.GetProcessesByName("steam").Length == 0)
            {
                string error_msg = "Your steam client is not running.";
                OutputText(error_msg);
                PaintText(error_msg, 255, 128, 128);
                return;
            }

            // stop any existing idle process
            StopIdle();

            StartIdle();

            string total_apps = AppList.CheckedItems.Count.ToString();
            OutputText("\nIdling " + total_apps + " app(s).");
        }

        // stop idle button
        void StopIdle()
        {
            foreach (var process in Process.GetProcessesByName(IdleExeName))
                process.Kill();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            if (Process.GetProcessesByName(IdleExeName).Length != 0)
            {
                StopIdle();

                OutputText("\nStopped existing idle process.");
            }
        }

        // input field
        private void InputField_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        // add app button
        private void AddAppButton_Click(object sender, EventArgs e)
        {
            if (InputField.Text == "")
                return;

            AppList.Items.Add(InputField.Text, true);

            OutputText("Added app " + InputField.Text);

            int index = OutputBox.Text.IndexOf(InputField.Text);
            int lenght = InputField.Text.Length;

            OutputBox.Select(index, lenght);
            OutputBox.SelectionColor = Color.FromArgb(255, 100, 200, 100);

            SaveAppList();
            InputField.Clear();
        }

        // remove app button
        private void RemoveAppButton_Click(object sender, EventArgs e)
        {
            if (AppList.SelectedItem==null)
                return;

            AppList.Items.Remove(AppList.SelectedItem);
            SaveAppList();
        }

        private void AppList_SelectedIndexChanged(object sender, EventArgs e) => SaveAppList();

        private void ClearLogButton_Click(object sender, EventArgs e) => OutputBox.Clear();
    }
}
