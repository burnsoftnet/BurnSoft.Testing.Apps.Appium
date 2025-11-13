using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using BurnSoft.Testing.Apps.Appium.Types;
namespace BurnSoft.Testing.Apps.Appium.NUnitTests
{
    public class Tests
    {
        /// <summary>
         /// Gets or sets the test context.
         /// </summary>
         /// <value>The test context.</value>
        public TestContext TestContext { get; set; }
        /// <summary>
        /// The error out
        /// </summary>
        private string _errOut;
        /// <summary>
        /// The ga
        /// </summary>
        private GeneralActions _ga;
        /// <summary>
        /// The automation identifier
        /// </summary>
        private string _automationId;
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            try
            {
                string SettingsScreenShotLocation = "ScreenShots";
                string fullExceptionPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SettingsScreenShotLocation);
                if (!Directory.Exists(fullExceptionPath)) Directory.CreateDirectory(fullExceptionPath);
                _errOut = "";
                _automationId = "AR-22";
                _ga = new GeneralActions();
                _ga.TestName = "UnitTest-Init";
                _ga.ApplicationPath = "c:\\Source\\Repos\\MyGunCollection\\BSMyGunCollection\\bin\\Debug\\BSMyGunCollection.exe";
                _ga.SettingsScreenShotLocation = fullExceptionPath;
                _ga.DoSleep = true;
                _ga.Initialize();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Assert.Fail(e.Message);
            }
        }
        [TearDown]
        public void Dispose()
        {
            _ga.Dispose();
        }

        [Test]
        public void PerformActionDoubleCLickElementTest()
        {
            bool value = false;
            try
            {
                value = _ga.PerformAction(_automationId, "", GeneralActions.MyAction.DoubleClick, out _errOut, GeneralActions.AppAction.FindElementByName);
                if (_errOut.Length > 0) throw new Exception(_errOut);
                Thread.Sleep(500);
            }
            catch (Exception e)
            {
                TestContext.WriteLine($"ERROR: {e.Message}");
            }
            //Assert.IsTrue(value);
        }
    }
}