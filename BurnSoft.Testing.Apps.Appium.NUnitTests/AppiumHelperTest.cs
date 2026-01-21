using NUnit.Framework;

namespace BurnSoft.Testing.Apps.Appium.NUnitTests
{
    public class AppiumHelperTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test, Category("AppiumHelper Function Test")]
        public void StartAppiumTest()
        {

            //AppiumHelper obj = new AppiumHelper(@"C:\Users\burnsoft\AppData\Roaming\npm\appium.cmd");
            AppiumHelper obj = new AppiumHelper(@"C:\Users\burnsoft\AppData\Roaming\npm\node_modules\appium");

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
