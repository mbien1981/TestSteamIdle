using System;
using System.IO;
using System.Diagnostics;

namespace TestSteamIdleConsole
{
    class Program
    {
        const string AppIdsTxt = "apps.txt";
        const string IdleExeName = "TestSteamIdle";
        const string IdleExe = IdleExeName + ".exe";

        static void Main(string[] args)
        {
            foreach (var process in Process.GetProcessesByName(IdleExeName))
                process.Kill();

            if (Process.GetProcessesByName("steam").Length == 0)
            {
                Console.WriteLine("Steam not running");
            }
            else
            {
                Console.WriteLine("Steam running");

                string path = AppDomain.CurrentDomain.BaseDirectory;
                string pathTxt = Path.Combine(path, AppIdsTxt);
                string pathExe = Path.Combine(path, IdleExe);
                ProcessStartInfo startInfo = new ProcessStartInfo(pathExe);

                foreach (string AppId in File.ReadAllText(pathTxt).Split(','))
                {
                    startInfo.Arguments = AppId;
                    Process.Start(startInfo);
                    Console.WriteLine("Launched " + AppId);
                }
            }

            Console.WriteLine("Press 'Enter' to exit...");
            Console.ReadLine();

            foreach (var process in Process.GetProcessesByName(IdleExeName))
                process.Kill();
        }
    }
}
