using BurnSoft.Testing.Apps.Appium.NUnitTests.Mappings;
using BurnSoft.Testing.Apps.Appium.NUnitTests.Settings;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace BurnSoft.Testing.Apps.Appium.NUnitTests
{
    public class AppiumHelperTest
    {
        private string _nodeEXE;
        private string _appiumNpm;
        private string _appiumEXE;
        private string _aut;
        private AppiumHelper appiumServer;

        [SetUp]
        public void Setup()
        {
            _aut = Settings.Settings.ApplicationUnderTest;
        }

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _nodeEXE = Settings.Settings.NodeExe;
            _appiumNpm = Settings.Settings.AppiumNpm;
            _appiumEXE = Settings.Settings.AppiumServerExe;
            appiumServer = new AppiumHelper(nodeExecutable: _nodeEXE, appiumMainJs: _appiumNpm);
        }

        [OneTimeTearDown]
        public void Close()
        {
            appiumServer.StopAppiumServer();
            KillAppByName(_aut);
        }

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

        [Test, Category("AppiumHelper Function Test")]
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

        [Test, Category("AppiumHelper Function Test")]
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

        [Test, Category("AppiumHelper Function Test")]
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
            var element = driver.FindElement((By.Id(TestAppMap.AutomationIds.ClickTestButton));
            //var element = driver.FindElement(By.Name(TestAppMap.DisplayName.ClickTestButton));
            //var element = driver.FindElement("Name", TestAppMap.AutomationIds.ClickTestButton);
            //element.Click();
            Thread.Sleep(60);
        }
    }
}
