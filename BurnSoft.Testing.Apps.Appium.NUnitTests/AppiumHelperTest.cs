using NUnit.Framework;
using System.IO;

namespace BurnSoft.Testing.Apps.Appium.NUnitTests
{
    public class AppiumHelperTest
    {
        private string _nodeEXE;
        private string _appiumNpm;
        private string _appiumEXE;

        [SetUp]
        public void Setup()
        {
            _nodeEXE = @"C:\nvm4w\nodejs\node.exe";
            _appiumNpm = @"C:\Users\burnsoft\AppData\Roaming\npm\node_modules\appium\build\lib\main.js";
            _appiumEXE = @"C:\Users\burnsoft\AppData\Roaming\npm\node_modules\appium";
        }

        [Test, Category("AppiumHelper Function Test")]
        public void StartAppiumTest()
        {

            //AppiumHelper obj = new AppiumHelper(@"C:\Users\burnsoft\AppData\Roaming\npm\appium.cmd");
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
    }
}
