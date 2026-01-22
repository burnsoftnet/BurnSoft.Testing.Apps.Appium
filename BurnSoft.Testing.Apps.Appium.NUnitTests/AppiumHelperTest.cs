using NUnit.Framework;
using System;
using System.IO;
using BurnSoft.Testing.Apps.Appium.NUnitTests.Settings;

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
    }
}
