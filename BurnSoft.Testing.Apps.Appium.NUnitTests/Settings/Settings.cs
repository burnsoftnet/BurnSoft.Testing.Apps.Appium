using System;
using System.IO;


namespace BurnSoft.Testing.Apps.Appium.NUnitTests.Settings
{
    /// <summary>
    /// Class Settings.
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// The application path
        /// </summary>
        public static string appPath = AppDomain.CurrentDomain.BaseDirectory;
        /// <summary>
        /// The break on fail
        /// </summary>
        public static bool BreakOnFail = true;
        /// <summary>
        /// The debug
        /// </summary>
        public static bool Debug = false;
        /// <summary>
        /// The application under test
        /// </summary>
        public static string ApplicationUnderTest = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestApp\\SampleUITestApp.exe");
        /// <summary>
        /// The node executable
        /// </summary>
        public static string NodeExe = FindExePath("node.exe");
        /// <summary>
        /// The user data path
        /// </summary>
        public static string UserDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        /// <summary>
        /// The appium NPM
        /// </summary>
        public static string AppiumNpm = Path.Combine(UserDataPath, 
            @"npm\node_modules\appium\build\lib\main.js");
        /// <summary>
        /// The appium server executable
        /// </summary>
        public static string AppiumServerExe = Path.Combine(UserDataPath, 
            @"npm\node_modules\appium");
        /// <summary>
        /// The json save to
        /// </summary>
        public static string JsonSaveTo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data\\saved_test.json");
        /// <summary>
        /// The json load from
        /// </summary>
        public static string JsonLoadFrom = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data\\load_test.json");
        /// <summary>
        /// The deleted file
        /// </summary>
        public static string DeletedFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data\\deletefile.txt");
        /// <summary>
        /// The fail file
        /// </summary>
        public static string FailFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data\\fail.txt");
        /// <summary>
        /// The pass file
        /// </summary>
        public static string PassFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data\\pass.txt");

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
    }
}
