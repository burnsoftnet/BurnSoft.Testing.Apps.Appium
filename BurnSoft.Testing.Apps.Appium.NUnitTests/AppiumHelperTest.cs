using NUnit.Framework;
using System.IO;

namespace BurnSoft.Testing.Apps.Appium.NUnitTests
{
    public class AppiumHelperTest
    {
        private string _nodeEXE;
        private string _appiumNpm;
        private string _appiumEXE;
        private string _aut;

        [SetUp]
        public void Setup()
        {
            _nodeEXE = @"C:\nvm4w\nodejs\node.exe";
            _appiumNpm = @"C:\Users\burnsoft\AppData\Roaming\npm\node_modules\appium\build\lib\main.js";
            _appiumEXE = @"C:\Users\burnsoft\AppData\Roaming\npm\node_modules\appium";
            _aut = "C:\\Source\\Repos\\BurnSoft.Testing.Apps.Appium\\SampleUITestApp\\bin\\Debug\\SampleUITestApp.exe";
        }

        [Test, Category("AppiumHelper Function Test")]
        public void StartAppiumTest()
        {

            AppiumHelper obj = new AppiumHelper(nodeExecutable: _nodeEXE, appiumMainJs: _appiumNpm);

            if (obj.StartAppium())
            {
                obj.StopAppiumServer();
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
            AppiumHelper obj = new AppiumHelper(nodeExecutable: _nodeEXE, appiumMainJs: _appiumNpm);
            var options = obj.SetDesiredCapabilities(_aut);
            if (!obj.StartDriverConnection(options)) 
                Assert.Fail();
        }
    }
}
