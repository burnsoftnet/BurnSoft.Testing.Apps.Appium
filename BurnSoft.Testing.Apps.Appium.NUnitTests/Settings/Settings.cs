using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurnSoft.Testing.Apps.Appium.NUnitTests.Settings
{
    public class Settings
    {
        public static string appPath = AppDomain.CurrentDomain.BaseDirectory;
        //public static string ApplicationUnderTest = "C:\\Source\\Repos\\BurnSoft.Testing.Apps.Appium\\SampleUITestApp\\bin\\Debug\\SampleUITestApp.exe";
        public static string ApplicationUnderTest = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestApp\\SampleUITestApp.exe");
        public static string NodeExe = @"C:\nvm4w\nodejs\node.exe";
        public static string AppiumNpm = @"C:\Users\burnsoft\AppData\Roaming\npm\node_modules\appium\build\lib\main.js";
        public static string AppiumServerExe = @"C:\Users\burnsoft\AppData\Roaming\npm\node_modules\appium";
    }
}
