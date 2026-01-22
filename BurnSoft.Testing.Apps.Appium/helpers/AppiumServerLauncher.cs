using System;
using System.Diagnostics;
using System.Threading;

namespace BurnSoft.Testing.Apps.Appium.helpers
{

    public class AppiumServerLauncher
    {
        private Process appiumServerProcess;

        public bool Start()
        {
            bool bAns = false;
            try
            {
                Console.WriteLine("Starting Appium server...");

                // Define the process start info
                var startInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe", // Or "bash" if on Linux/macOS
                    Arguments = "/c appium", // The command to start Appium
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                appiumServerProcess = new Process
                {
                    StartInfo = startInfo
                };

                appiumServerProcess.OutputDataReceived += (sender, args) => Console.WriteLine(args.Data);
                appiumServerProcess.ErrorDataReceived += (sender, args) => Console.WriteLine(args.Data);

                appiumServerProcess.Start();
                appiumServerProcess.BeginOutputReadLine();
                appiumServerProcess.BeginErrorReadLine();

                // Give the server a moment to fully start
                Thread.Sleep(5000);
                Console.WriteLine("Appium server started.");
                bAns = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            return bAns;
        }

        public void Close()
        {
            if (appiumServerProcess != null && !appiumServerProcess.HasExited)
            {
                Console.WriteLine("Stopping Appium server...");
                appiumServerProcess.Kill();
                appiumServerProcess.WaitForExit(5000);
                Console.WriteLine("Appium server stopped.");
            }
        }
    }

}
