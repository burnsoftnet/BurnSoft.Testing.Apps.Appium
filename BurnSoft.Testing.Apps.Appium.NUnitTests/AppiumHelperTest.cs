using BurnSoft.Testing.Apps.Appium.NUnitTests.Mappings;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace BurnSoft.Testing.Apps.Appium.NUnitTests
{
    /// <summary>
    /// Class AppiumHelperTest.
    /// </summary>
    public class AppiumHelperTest
    {
        /// <summary>
        /// The node executable
        /// </summary>
        private string _nodeEXE;
        /// <summary>
        /// The appium NPM
        /// </summary>
        private string _appiumNpm;
        /// <summary>
        /// The appium executable
        /// </summary>
        private string _appiumEXE;
        /// <summary>
        /// The aut
        /// </summary>
        private string _aut;
        /// <summary>
        /// The appium server
        /// </summary>
        private AppiumHelper appiumServer;
        /// <summary>
        /// Setups this instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _aut = Settings.Settings.ApplicationUnderTest;
        }
        /// <summary>
        /// Called when [time setup].
        /// </summary>
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _nodeEXE = Settings.Settings.NodeExe;
            _appiumNpm = Settings.Settings.AppiumNpm;
            _appiumEXE = Settings.Settings.AppiumServerExe;
            appiumServer = new AppiumHelper(nodeExecutable: _nodeEXE, appiumMainJs: _appiumNpm);
        }
        /// <summary>
        /// Closes this instance.
        /// </summary>
        [OneTimeTearDown]
        public void Close()
        {
            appiumServer.StopAppiumServer();
            KillAppByName(_aut);
        }
        /// <summary>
        /// Kills the name of the application by.
        /// </summary>
        /// <param name="appName">Name of the application.</param>
        public void KillAppByName(string appName)
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
                    Console.WriteLine($"Could not kill process {proc.Id}: {ex.Message}");
                }
            }
        }
        /// <summary>
        /// Defines the test method StartAppiumTest.
        /// </summary>
        [Test, Category("AppiumHelper Function Test"), Order(1)]
        public void StartAppiumTest()
        {
            appiumServer.Errors += (ss, ee) =>
            {
                Console.WriteLine($"ERROR: {ee}");
            };
            if (appiumServer.StartAppium())
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }
        /// <summary>
        /// Defines the test method StartApplicationUnderTest.
        /// </summary>
        [Test, Category("AppiumHelper Function Test"), Order(2)]
        public void StartApplicationUnderTest()
        {
            appiumServer.Errors += (ss, ee) =>
            {
                Console.WriteLine($"ERROR: {ee}");
            };
            var options = appiumServer.SetDesiredCapabilities(_aut);
            if (!appiumServer.StartDriverConnection(options)) 
                Assert.Fail();
        }
        /// <summary>
        /// Defines the test method ManualButtonClickTest.
        /// </summary>
        [Test, Category("AppiumHelper Function Test"), Order(3)]
        public void ManualButtonClickTest()
        {
            appiumServer.Errors += (ss, ee) =>
            {
                Console.WriteLine($"ERROR: {ee}");
            };
            var options = appiumServer.SetDesiredCapabilities(_aut);
            if (!appiumServer.StartDriverConnection(options))
                Assert.Fail();
            var driver = appiumServer.driver;

            var element = driver.FindElement(MobileBy.Name(TestAppMap.AutomationIds.ClickTestButton));
            element.Click();
            Thread.Sleep(60);
        }
    }
}
