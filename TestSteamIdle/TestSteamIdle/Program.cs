using System;
using System.Windows.Forms;
using Steamworks;

namespace TestSteamIdle
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Environment.SetEnvironmentVariable("SteamAppId", long.Parse(args[0]).ToString());
            if (!SteamAPI.Init()) return;
            Application.Run();
        }
    }
}
