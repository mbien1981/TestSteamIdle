using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace TestSteamIdleGui
{
    public partial class MainWindow : Form
    {
        static readonly string ResourceFolder = "Resources/";
        static readonly string CacheFolder = "apps/";
        static readonly string CacheExt = ".cache";
        static readonly string IdleExeName = "TestSteamIdle";
        static readonly string IdleExe = IdleExeName + ".exe";
        static readonly string IcoName = IdleExeName + ".ico";

        static readonly string path = AppDomain.CurrentDomain.BaseDirectory;
        static readonly string pathCache = Path.Combine(path, CacheFolder);
        static readonly string pathResources = Path.Combine(path, ResourceFolder);
        static readonly string pathIco = Path.Combine(pathResources, IcoName);
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
        public void SaveAppList()
        {
            for (int i = 0; i < AppList.Items.Count; i++)
            {
                AppInfo app = (AppInfo)AppList.Items[i];

                string filepath = pathCache + app.Id.ToString() + CacheExt;
                if (File.Exists(filepath))
                    File.Delete(filepath);

                var stream = File.Create(filepath);

                bool state = AppList.GetItemChecked(i);

                new BinaryFormatter().Serialize(stream, new AppInfo(app.Id, state, app.Name));
                stream.Close();
            }
        }

        public void LoadAppList()
        {
            SortedDictionary<string, AppInfo> apps = new SortedDictionary<string, AppInfo>();

            string[] CacheFiles = Directory.GetFiles(pathCache);
            foreach (string filepath in CacheFiles)
            {
                var stream = File.OpenRead(filepath);
                var app = (AppInfo)new BinaryFormatter().Deserialize(stream);
                stream.Close();

                apps.Add(app.Name, app);
            }

            foreach (KeyValuePair<string, AppInfo> item in apps)
            {
                AppList.Items.Add(item.Value, item.Value.State);
            }

            AppCounter.Text = "Apps: " + AppList.Items.Count;
        }

        private string GetAppName(int appid)
        {
            // Ignore exception: Can not create SSL/TLS secure channel
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            //
            
            Regex reg_exp = new Regex("[^ -~]+");

            string name = string.Empty;
            string url = "https://store.steampowered.com/api/appdetails?filters=basic&appids=" + appid.ToString();
            using (WebClient client = new WebClient())
            {
                string response = client.DownloadString(url);
                try
                {
                    name = Regex.Match(reg_exp.Replace(response, ""), ",\"name\":\"(.+?)\",").Groups[1].Value;
                }
                catch
                {
                }

            }

            return name != "" ? name : appid.ToString() + " | <unknown>";
        }

        // dark title bar - https://stackoverflow.com/a/64927217
        [DllImport("DwmApi")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);

        protected override void OnHandleCreated(EventArgs e)
        {
            if (DwmSetWindowAttribute(Handle, 19, new[] { 1 }, 4) != 0)
                DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4);
        }

        // init
        private void MainWindow_Load(object sender, EventArgs e)
        {
            OnHandleCreated(e);

            NotifyIcon.Visible = false;

            Directory.CreateDirectory(CacheFolder);

            LoadAppList();

            OutputText("Initialized.");

            if (Process.GetProcessesByName("steam").Length == 0)
            {
                string error_msg = "Your steam client is not running.\n";
                OutputText(error_msg);
                PaintText(error_msg, 255, 128, 128);
                return;
            }

            string success_msg = "Steam client is running.\n";
            OutputText(success_msg);
            PaintText(success_msg, 128, 255, 128);

            int apps_running = Process.GetProcessesByName(IdleExeName).Length;
            if (apps_running != 0)
                OutputText("currently there are " + apps_running.ToString() + " apps idling.");
        }

        // start idle button
        void StartIdle()
        {
            OutputText("\nThe following apps have been set to idle:");

            foreach (AppInfo app in AppList.CheckedItems)
            {
                startInfo.Arguments = app.Id.ToString();
                Process.Start(startInfo);

                OutputText("\t• " + app.ToString());
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

            int id = int.Parse(InputField.Text);
            
            AppInfo app = new AppInfo(id, true, GetAppName(id));
            AppList.Items.Add(app, app.State);

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

            AppInfo app = (AppInfo)AppList.SelectedItem;

            string filepath = pathCache + app.Id.ToString() + CacheExt;
            if (File.Exists(filepath))
                File.Delete(filepath);

            AppList.Items.Remove(AppList.SelectedItem);

            string app_msg =  app.Id.ToString() + " | " + app.Name;
            OutputText("Removed app: " + app_msg);
            PaintText(app_msg, 255, 128, 128);


            SaveAppList();
        }

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

        private void AppList_SelectedIndexChanged(object sender, EventArgs e) => SaveAppList();

        private void SaveButton_Click(object sender, EventArgs e) => SaveAppList();

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                NotifyIcon.Visible = true;
                this.Hide();
                e.Cancel = true;
            }
        }


        private void MaximizeWindow()
        {
            NotifyIcon.Visible = false;
            this.Show();
        }

        // icon tray
        private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e) => MaximizeWindow();

        // context menu
        private void maximizeToolStripMenuItem_Click(object sender, EventArgs e) => MaximizeWindow();

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotifyIcon.Visible = false;
            Application.Exit();
        }

    }

    [Serializable]
    public class AppInfo
    {
        public int Id { get; set; }
        public bool State { get; set; }
        public string Name { get; set; }

        public AppInfo(int id, bool state = true, string name = "<unknown>")
        {
            this.Id = id;
            this.State = state;
            this.Name = name;
        }

        public override string ToString() => Name.ToString();
    }
}
