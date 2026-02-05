using OpenQA.Selenium.BiDi.Script;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurnSoft.Testing.Apps.Appium.helpers
{
    /// <summary>
    /// Class SystemHelpers is just that functions that are system related that help with 
    /// the testing you want to do and the unit tests
    /// </summary>
    public class SystemHelpers
    {
        /// <summary>
        /// Locates the full path of an executable file by searching the environment's PATH.
        /// </summary>
        /// <param name="exeName">The name of the executable file (e.g., "cmd.exe").</param>
        /// <returns>The fully qualified path to the file, or null if not found.</returns>
        public static string FindExePath(string exeName)
        {
            // Expand environment variables in the provided name
            exeName = Environment.ExpandEnvironmentVariables(exeName);

            // If the file exists at the current location, return its full path immediately
            if (File.Exists(exeName))
            {
                return Path.GetFullPath(exeName);
            }

            // Check if the provided name has a directory specified
            if (Path.GetDirectoryName(exeName) == String.Empty)
            {
                // If not, search the directories listed in the PATH environment variable
                var values = Environment.GetEnvironmentVariable("PATH");
                foreach (var path in values.Split(Path.PathSeparator))
                {
                    var fullPath = Path.Combine(path.Trim(), exeName);
                    if (File.Exists(fullPath))
                    {
                        return fullPath;
                    }
                }
            }

            // Executable not found in current directory or PATH
            return null;
        }

        /// <summary>
        /// Kills the name of the application by.
        /// </summary>
        /// <param name="appName">Name of the application.</param>
        public static bool KillAppByName(string appName, out string errOut)
        {
            bool bAns = false;
            errOut = "";
            try
            {
                // The process name is typically the executable name without the .exe extension
                // For "notepad.exe", the process name is "notepad"
                appName = Path.GetFileName(appName);
                string processName = Path.GetFileNameWithoutExtension(appName);

                Process[] processes = Process.GetProcessesByName(processName);

                foreach (Process proc in processes)
                {
                    try
                    {
                        proc.Kill();
                        // Optional: wait for the process to exit to ensure it's fully terminated
                        proc.WaitForExit();
                    }
                    catch (Exception ex)
                    {
                         errOut += $"Could not kill process {proc.Id}: {ex.Message}{Environment.NewLine}";
                    }
                }
                if (errOut.Length > 0) throw new Exception(errOut);
                bAns = true;
            }
            catch (Exception ex)
            {
                errOut = ex.Message;
            }
            return bAns;
        }
    }
}
