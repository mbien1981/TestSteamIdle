using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
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
            SortedDictionary<int, bool> apps = new SortedDictionary<int, bool>();

            foreach (string line in File.ReadLines(pathTxt))
            {
                List<string> data = line.Split(',').ToList();
                if (data.Count > 0)
                    apps.Add(int.Parse(data[0]), bool.Parse(data[1]));
            }

            foreach (KeyValuePair<int, bool> kvp in apps)
            {
                AppInfo app = new AppInfo(kvp.Key, GetAppName(kvp.Key));

                AppList.Items.Add(app, kvp.Value);
            }

            AppCounter.Text = "Apps: " + AppList.Items.Count;
        }

        private void SaveAppList()
        {
            List<string> data = new List<string>();

            for (int i = 0; i < AppList.Items.Count; i++)
            {
                AppInfo app = (AppInfo)AppList.Items[i];
                bool state = AppList.GetItemChecked(i);

                data.Add(app.Id + "," + state.ToString());
            }

            System.IO.File.WriteAllLines("apps.txt", data);
        }

        private static string GetAppName(int appid)
        {
            // Ignore exception: Can not create SSL/TLS secure channel
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            //

            string name = "<unknown>";
            string url = "https://store.steampowered.com/api/appdetails?filters=basic&appids=" + appid.ToString();
            using (WebClient client = new WebClient())
            {
                string response = client.DownloadString(url);
                try
                {
                    Regex reg_exp = new Regex("[^ -~]+");
                    // app name without unicode characters
                    name = Regex.Match(reg_exp.Replace(response, ""), ",\"name\":\"(.+?)\",").Groups[1].Value;
                }
                catch
                {
                }
            }
            return name;
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

            foreach (AppInfo app in AppList.CheckedItems)
            {
                startInfo.Arguments = app.Id.ToString();
                Process.Start(startInfo);

                OutputText("\t• Started idle process for app " + app.ToString());
                PaintText(app.ToString(), 128, 255, 128);
            }
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (Process.GetProcessesByName("steam").Length == 0)
            {
                string error_msg = "Your steam client is not running.";
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

            if (AppList.CheckedItems.Count > 31)
            {
                string error_msg = "App limit exceeded you cannot run more than 31 apps at once.";
                OutputText(error_msg);
                PaintText(error_msg, 255, 128, 128);

                return;
            }

            if (AppList.CheckedItems.Count > 1)
                SetSteamStatus("invisible");

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

                Thread.Sleep(2500);
                if (AppList.CheckedItems.Count > 1)
                    SetSteamStatus("online");
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

            AppInfo app = new AppInfo(int.Parse(InputField.Text), GetAppName(int.Parse(InputField.Text)));
            AppList.Items.Add(app, true);

            OutputText("Added app " + app.Name);
            PaintText(app.Name, 128, 255, 128);

            SaveAppList();
            AppList.Items.Clear();
            LoadAppList();

            InputField.Clear();
        }

        // remove app button
        private void RemoveAppButton_Click(object sender, EventArgs e)
        {
            if (AppList.SelectedItem == null)
                return;

            AppList.Items.Remove(AppList.SelectedItem);
            SaveAppList();
        }

        private void AppList_SelectedIndexChanged(object sender, EventArgs e) => SaveAppList();

        private void ClearLogButton_Click(object sender, EventArgs e) => OutputBox.Clear();

        // update steam status
        void SetSteamStatus(string status)
        {
            Process.Start("steam://friends/status/" + status);
            OutputText("Steam status set to " + status + ".");
            PaintText(status, 255, 255, 128);
        }

        private void SetInvisibleButton_Click(object sender, EventArgs e) => SetSteamStatus("invisible");
        

        private void SetOnlineButton_Click(object sender, EventArgs e) => SetSteamStatus("online");
    }

    public class AppInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public AppInfo(int id, string name = "<unknown>")
        {
            this.Id = id;
            this.Name = name;
        }
        public override string ToString() => Name.ToString();
    }
}
