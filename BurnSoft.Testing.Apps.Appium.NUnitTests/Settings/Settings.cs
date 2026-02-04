using BurnSoft.Testing.Apps.Appium.helpers;
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
        public static bool Debug = true;
        /// <summary>
        /// The application under test
        /// </summary>
        public static string ApplicationUnderTest = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestApp\\SampleUITestApp.exe");
        /// <summary>
        /// The node executable
        /// </summary>
        public static string NodeExe = SystemHelpers.FindExePath("node.exe");
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
    }
}
