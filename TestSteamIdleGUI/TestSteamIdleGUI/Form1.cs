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
using System.Diagnostics;


namespace TestSteamIdleGUI
{
    public partial class mainWindow : Form {
        const string AppIdsTxt = "apps.txt";
        const string IdleExeName = "TestSteamIdle";
        const string IdleExe = IdleExeName + ".exe";

        public mainWindow() {
            InitializeComponent();
        }

        private void stopExistingIdle() {
            if (Process.GetProcessesByName(IdleExeName).Length != 0) {
                StartIdleButton.Enabled = false;

                foreach (var process in Process.GetProcessesByName(IdleExeName))
                    process.Kill();

                printText("\nStopped existing idle process.");

                StartIdleButton.Enabled = true;
            }
        }

        private void printText(String input) {
            OutputCase.AppendText(input + "\n");
        }

        private void startIdle_Button(object sender, EventArgs e) {
            stopExistingIdle();

            string path = AppDomain.CurrentDomain.BaseDirectory;
            string pathTxt = Path.Combine(path, AppIdsTxt);
            string pathExe = Path.Combine(path, IdleExe);
            ProcessStartInfo startInfo = new ProcessStartInfo(pathExe);

            printText("\nStarting Idle...");

            foreach (string AppId in File.ReadAllText(pathTxt).Split(',')) {
                startInfo.Arguments = AppId;
                Process.Start(startInfo);

                printText("\t• Started idle process for app " + AppId);

                int index = OutputCase.Text.IndexOf(AppId);
                int lenght = AppId.Length;

                OutputCase.Select(index, lenght);
                OutputCase.SelectionColor = Color.FromArgb(255, 100, 200, 100);
            }

            printText("\nIdling a total of " + Process.GetProcessesByName(IdleExeName).Length + " game(s).");
        }

        private void killIdleButton_Click(object sender, EventArgs e) {
            stopExistingIdle();
        }

        private void MainWindow_Load(object sender, EventArgs e) {
            printText("Initialized...\n");
            stopExistingIdle();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OutputCase.Clear();
            printText("Cleared.");
        }
    }
}
