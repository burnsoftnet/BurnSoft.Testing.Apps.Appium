using NUnit.Framework;
using System;
using System.IO;

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
            _aut = "C:\\Source\\Repos\\BurnSoft.Testing.Apps.Appium\\SampleUITestApp\\bin\\Debug\\SampleUITestApp.exe";
        }

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _nodeEXE = @"C:\nvm4w\nodejs\node.exe";
            _appiumNpm = @"C:\Users\burnsoft\AppData\Roaming\npm\node_modules\appium\build\lib\main.js";
            _appiumEXE = @"C:\Users\burnsoft\AppData\Roaming\npm\node_modules\appium";
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
