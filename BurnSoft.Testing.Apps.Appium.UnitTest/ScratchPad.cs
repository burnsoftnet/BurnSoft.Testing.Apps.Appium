using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using BurnSoft.Testing.Apps.Appium.Types;
// ReSharper disable UseObjectOrCollectionInitializer

namespace BurnSoft.Testing.Apps.Appium.UnitTest
{
    /// <summary>
    /// Defines test class ScratchPad. A general test to experiment with new possible functions
    /// </summary>
    [TestClass]
    public class ScratchPad
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
        [TestInitialize]
        public void Init()
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
        /// <summary>
        /// Cleans up.
        /// </summary>
        [TestCleanup]
        public void CleanUp()
        {
            _ga.Dispose();
        }
        [TestMethod, TestCategory("Indvidual Actions - Find Element")]
        public void FindElementsTest()
        {
            _ga.GetElements("ListBox1", out _errOut);
            Assert.IsTrue(_errOut.Length == 0);
        }
    }
}
