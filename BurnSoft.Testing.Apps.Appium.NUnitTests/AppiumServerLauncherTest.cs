using BurnSoft.Testing.Apps.Appium.helpers;
using NUnit.Framework;

namespace BurnSoft.Testing.Apps.Appium.NUnitTests
{
    public class AppiumServerLauncherTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test, Category("Appium Manuual Start Function Test")]
        public void StartTest()
        {
            AppiumServerLauncher obj = new AppiumServerLauncher();
            if (obj.Start())
            {
                obj.Close();
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }
    }
}
